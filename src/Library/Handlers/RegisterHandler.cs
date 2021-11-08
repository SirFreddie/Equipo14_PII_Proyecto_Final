using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Nito.AsyncEx;
using Telegram.Bot.Types.ReplyMarkups;
using System;

namespace Proyecto_Final
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "registro".
    /// </summary>
    public class RegisterHandler : BaseHandler
    {
        private string[] allowedStatus;

        public string[] AllowedStatus { get; set;}
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="RegisterHandler"/>. Esta clase procesa el mensaje "registro".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public RegisterHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"registro"};
            this.AllowedStatus = new string[] {"STATUS_REGISTER_RESPONSE", "STATUS_REGISTER_NAME", "STATUS_REGISTER_UBICACION"};  
        }

        /// <summary>
        /// Procesa el mensaje "registro" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override bool InternalHandle(Message message, out string response)
        {
            string check = Singleton<StatusManager>.Instance.CheckStatus(message.From.Id);
            if (this.CanHandle(message) || (this.AllowedStatus.Contains(check)))
            {
                if (check == "STATUS_IDLE")
                {   
                    response = "¿Tienes un token de registro? Y/N";
                    //response = "¿Deseas registrarte como Emprendedor? Y/N";
                    Singleton<StatusManager>.Instance.AgregarEstadoUsuario(message.From.Id, "STATUS_REGISTER_RESPONSE");
                    return true;
                }
                else if (check == "STATUS_REGISTER_RESPONSE")
                {
                    if (message.Text.ToUpper() == "Y")
                    {
                        response = "Ingrese su token: ";
                        // TODO: Checkear el token.
                        Singleton<StatusManager>.Instance.AgregarEstadoUsuario(message.From.Id, "STATUS_REGISTER_EMPRESA");
                        return true;
                        /*
                        response = "Ingrese su nombre: ";
                        Singleton<StatusManager>.Instance.AgregarEstadoUsuario(message.From.Id, "STATUS_REGISTER_NAME");
                        return true;*/
                    }
                    else
                    {
                        response = "Registro anulado.";
                        Singleton<StatusManager>.Instance.AgregarEstadoUsuario(message.From.Id, "STATUS_IDLE");
                        return true;
                    }
                }
                else if (check == "STATUS_REGISTER_NAME")
                {
                    response = $"Su nombre es: {message.Text}.\nIngrese su ubicacion: ";
                    UserEmprendedor userEmprendedor = new UserEmprendedor(message.Text);
                    Emprendedor emprendedor = new Emprendedor("", new Rubro(""), new Habilitaciones(""));
                    userEmprendedor.Emprendedor = emprendedor;
                    Singleton<StatusManager>.Instance.AgregarEstadoUsuario(message.From.Id, "STATUS_REGISTER_UBICACION");
                    return true;
                }
                else if (check == "STATUS_REGISTER_UBICACION")
                {
                    response = $"Su ubicacion es: {message.Text}.\nIngrese su rubro: ";
                    return true;
                }
            }

            response = string.Empty;
            return false;
        }
    }
}






