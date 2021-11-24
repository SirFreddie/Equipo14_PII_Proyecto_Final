using System.Linq;
using System;
using Ucu.Poo.Locations.Client;

namespace Proyecto_Final
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "Publicar".
    /// </summary>
    public class ShowInterestInOfferHandler : BaseHandler
    {
        private string[] allowedStatus;
        /// <summary>
        /// Otorga un array con los status validos.
        /// </summary>
        /// <value>Array de status</value>
        public string[] AllowedStatus { get; set;}
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PublishHandler"/>. Esta clase procesa el mensaje "Publicar".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public ShowInterestInOfferHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/interes_oferta"};
            this.AllowedStatus = new string[] {"STATUS_END_OFFER_RESPONSE",
                                                "STATUS_END_OFFER_OFFER_SELECTED",

                                               };  
        }

        /// <summary>
        /// Procesa el mensaje "Publicar" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override bool InternalHandle(IMessage message, out string response)
        {
            string check = Singleton<StatusManager>.Instance.CheckStatus(message.UserId);
            if (this.CanHandle(message) || (this.AllowedStatus.Contains(check)))
            {
                if (check == "STATUS_IDLE")
                {
                    response = $"";
                    Singleton<StatusManager>.Instance.AgregarEstadoUsuario(message.UserId,"STATUS_END_OFFER_RESPONSE");
                    return true;
                }
                else if (check == "STATUS_END_OFFER_RESPONSE")
                {
                    if (message.Text == "Y")
                    {
                        response = $"Procederé a mostrarle todas sus ofertas. Responda con el ID de la oferta que quiere concretar.";
                        UserEmpresa user = (UserEmpresa) Singleton<Datos>.Instance.GetUserById(message.UserId);
                        foreach (Oferta oferta in user.Empresa.Ofertas)
                        {
                            response += $"\nID: {oferta.Id} \nNombre: {oferta.Product.Nombre} \nDescripción: {oferta.Product.Descripcion} \nTipo: {oferta.Product.Tipo.Nombre} \nUbicación: {oferta.Product.Ubicacion} \nValor: {oferta.Product.MonetaryValue()}{oferta.Product.Valor} \nCantidad: {oferta.Product.Cantidad} \nHabilitaciones requeridas: {oferta.HabilitacionesOferta.Habilitacion} \n";
                        }
                        Singleton<StatusManager>.Instance.AgregarEstadoUsuario(message.UserId,"STATUS_END_OFFER_OFFER_SELECTED");
                        return true;
                    }
                    else if (message.Text == "N")
                    {
                        response = "Operacion abortada correctamente.";
                        Singleton<StatusManager>.Instance.AgregarEstadoUsuario(message.UserId,"STATUS_IDLE");
                        return true;
                    }
                    else
                    {
                        response = $"No entendí, por favor, responda \"Y\" para concretar una oferta o escriba \"N\" para cancelar la operación.";
                        return true;
                    }
                }
                else if(check == "STATUS_END_OFFER_OFFER_SELECTED")
                {
                   UserEmpresa user = (UserEmpresa) Singleton<Datos>.Instance.GetUserById(message.UserId);
                   foreach (Oferta oferta in user.Empresa.Ofertas)
                   {
                       if (oferta.Id == message.Text)
                       {
                           
                       }
                   }
                }
            }
            response = String.Empty;
            return false;
        }
    }
}