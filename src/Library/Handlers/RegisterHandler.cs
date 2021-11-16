
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Final
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "registro".
    /// </summary>
    public class RegisterHandler : BaseHandler
    {
        private string[] allowedStatus;

        /// <summary>
        /// Otorga un array con los status validos.
        /// </summary>
        /// <value>Array de status</value>
        public string[] AllowedStatus { get; set;}
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="RegisterHandler"/>. Esta clase procesa el mensaje "registro".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public RegisterHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"registro"};
            this.AllowedStatus = new string[] {
                                               "STATUS_REGISTER_RESPONSE",
                                               "STATUS_REGISTER_EMPRENDEDOR",
                                               "STATUS_REGISTER_EMPRENDEDOR_NAME",
                                               "STATUS_REGISTER_EMPRENDEDOR_UBICACION",
                                               "STATUS_REGISTER_EMPRENDEDOR_RUBRO",
                                               "STATUS_REGISTER_EMPRENDEDOR_HABILITACION",
                                               "STATUS_REGISTER_EMPRESA",
                                               "STATUS_REGISTER_EMPRESA_NAME",
                                               "STATUS_REGISTER_EMPRESA_RUBRO",
                                               "STATUS_REGISTER_EMPRESA_UBICACION"
                                              };  
        }

        /// <summary>
        /// Procesa el mensaje "registro" y retorna true; retorna false en caso contrario.
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
                    if (Singleton<Datos>.Instance.IsRegistered(message.UserId))
                    {
                        response = "Ya estas registrado!.";
                        return true;
                    }
                    response = "¿Tienes un token de registro? Y/N";
                    Singleton<StatusManager>.Instance.AgregarEstadoUsuario(message.UserId, "STATUS_REGISTER_RESPONSE");
                    return true;
                }
                else if (check == "STATUS_REGISTER_RESPONSE")
                {
                    if (message.Text.ToUpper() == "Y")
                    {
                        response = "Ingrese su token: ";
                        Singleton<StatusManager>.Instance.AgregarEstadoUsuario(message.UserId, "STATUS_REGISTER_EMPRESA");
                        return true;
                    }
                    else
                    {
                        response = "¿Deseas registrarte como Emprendedor? Y/N";
                        Singleton<StatusManager>.Instance.AgregarEstadoUsuario(message.UserId, "STATUS_REGISTER_EMPRENDEDOR");
                        return true;
                    }
                }
                else if (check == "STATUS_REGISTER_EMPRESA")
                {
                    if (Singleton<Datos>.Instance.IsTokenValid(message.Text))
                    {
                        response = $"Token valido.\n\nIngrese el nombre de su empresa: ";
                        Singleton<StatusManager>.Instance.AgregarEstadoUsuario(message.UserId, "STATUS_REGISTER_EMPRESA_NAME");
                        return true;
                    }
                    else
                    {
                        response = "Token invalido.\n¿Quieres intentarlo de nuevo? Y/N";
                        Singleton<StatusManager>.Instance.AgregarEstadoUsuario(message.UserId, "STATUS_REGISTER_RESPONSE");
                        return true;
                    }
                }
                else if (check == "STATUS_REGISTER_EMPRESA_NAME")
                {
                    response = $"Su nombre es: {message.Text}.\n\nRubros validos:\n";
                    StringBuilder str = new StringBuilder();
                    foreach (string rubro in Singleton<Datos>.Instance.ListaRubros())
                    {
                        str.Append($"- {rubro}\n");
                    }
                    response += str;
                    response += $"\n\nIngrese su rubro:";

                    UserEmpresa userEmpresa = new UserEmpresa(message.UserId, message.Text);
                    Empresa empresa = new Empresa(message.Text, "", new Rubro(""));
                    userEmpresa.Empresa = empresa;
                    Singleton<Datos>.Instance.ListaUsuariosRegistrados().Add(userEmpresa);

                    Singleton<StatusManager>.Instance.AgregarEstadoUsuario(message.UserId, "STATUS_REGISTER_EMPRESA_RUBRO");
                    return true;
                }
                else if (check == "STATUS_REGISTER_EMPRESA_RUBRO")
                {
                    if (Singleton<Datos>.Instance.CheckRubros(message.Text))
                    {
                        response = $"Su rubro es: {message.Text}.\n\nIngrese su ubicacion: ";

                        foreach (UserEmpresa userEmpresa in Singleton<Datos>.Instance.ListaUsuariosRegistrados())
                        {
                            if (userEmpresa.Id == message.UserId)
                            {
                                Rubro newRubro = new Rubro(message.Text);
                                userEmpresa.Empresa.Rubro = newRubro;
                            }
                        }
                        Singleton<StatusManager>.Instance.AgregarEstadoUsuario(message.UserId, "STATUS_REGISTER_EMPRESA_UBICACION");
                        return true;
                    }
                    else 
                    {
                        response = $"Rubro invalido.\nRubros validos: ";
                        StringBuilder str = new StringBuilder();
                        foreach (string rubro in Singleton<Datos>.Instance.ListaRubros())
                        {
                            str.Append($"- {rubro}\n");
                        }
                        response += str;
                        response += "\n\nIngrese su rubro nuevamente:";
                        Singleton<StatusManager>.Instance.AgregarEstadoUsuario(message.UserId, "STATUS_REGISTER_EMPRESA_RUBRO");
                        return true;
                    }
                }
                else if (check == "STATUS_REGISTER_EMPRESA_UBICACION")
                {
                    response = $"Su ubicacion es: {message.Text}.\n\nREGISTRO COMPLETO!!!.\n\nAhora estas registrado como empresa. ";

                    foreach (UserEmpresa user in Singleton<Datos>.Instance.ListaUsuariosRegistrados())
                    {
                        if (user.Id == message.UserId)
                        {
                            user.Empresa.Ubicacion = message.Text;
                        }
                    }

                    Singleton<StatusManager>.Instance.AgregarEstadoUsuario(message.UserId, "STATUS_IDLE");

                    return true;
                }
                else if (check == "STATUS_REGISTER_EMPRENDEDOR")
                {
                    if (message.Text.ToUpper() == "Y")
                    {
                        response = "Ingrese su nombre: ";
                        Singleton<StatusManager>.Instance.AgregarEstadoUsuario(message.UserId, "STATUS_REGISTER_EMPRENDEDOR_NAME");
                        return true;
                    }
                    else
                    {
                        response = "Registro anulado.";
                        Singleton<StatusManager>.Instance.AgregarEstadoUsuario(message.UserId, "STATUS_IDLE");
                        return true;
                    }
                }
                else if (check == "STATUS_REGISTER_EMPRENDEDOR_NAME")
                {
                    response = $"Su nombre es: {message.Text}.\n\nIngrese su ubicacion: ";

                    UserEmprendedor userEmprendedor = new UserEmprendedor(message.UserId, message.Text);
                    Emprendedor emprendedor = new Emprendedor("", new Rubro(""), new Habilitaciones(""));
                    userEmprendedor.Emprendedor = emprendedor;
                    Singleton<Datos>.Instance.ListaUsuariosRegistrados().Add(userEmprendedor);

                    Singleton<StatusManager>.Instance.AgregarEstadoUsuario(message.UserId, "STATUS_REGISTER_EMPRENDEDOR_UBICACION");

                    return true;
                }
                else if (check == "STATUS_REGISTER_EMPRENDEDOR_UBICACION")
                {
                    response = $"Su ubicacion es: {message.Text}.\n\nRubros validos:\n";

                    StringBuilder str = new StringBuilder();
                    foreach (string rubro in Singleton<Datos>.Instance.ListaRubros())
                    {
                        str.Append($"- {rubro}\n");
                    }
                    response += str;
                    response += $"\n\nIngrese su rubro:";

                    
                    foreach (UserEmprendedor user in Singleton<Datos>.Instance.ListaUsuariosRegistrados())
                    {
                        if (user.Id == message.UserId)
                        {
                            user.Emprendedor.Ubicacion = message.Text;
                        }
                    }

                    Singleton<StatusManager>.Instance.AgregarEstadoUsuario(message.UserId, "STATUS_REGISTER_EMPRENDEDOR_RUBRO");

                    return true;
                }
                else if (check == "STATUS_REGISTER_EMPRENDEDOR_RUBRO")
                {
                    if (Singleton<Datos>.Instance.CheckRubros(message.UserId))
                    {
                        response = $"Su rubro es: {message.Text}.\n\nHabilitaciones validas:\n";
                        StringBuilder str = new StringBuilder();
                        foreach (string habilitacion in Singleton<Datos>.Instance.ListaHabilitaciones())
                        {
                            str.Append($"- {habilitacion}\n");
                        }
                        response += str;
                        response += $"\n\nIngrese su habilitacion:";

                        
                        foreach (UserEmprendedor user in Singleton<Datos>.Instance.ListaUsuariosRegistrados())
                        {
                            if (user.Id == message.UserId)
                            {
                                Rubro newRubro = new Rubro(message.Text);
                                user.Emprendedor.Rubro = newRubro;
                            }
                        }

                        Singleton<StatusManager>.Instance.AgregarEstadoUsuario(message.UserId, "STATUS_REGISTER_EMPRENDEDOR_HABILITACIONES");

                        return true;
                    }
                    else
                    {
                        response = $"Rubro invalido.\nRubros validos: ";
                        StringBuilder str = new StringBuilder();
                        foreach (string rubro in Singleton<Datos>.Instance.ListaRubros())
                        {
                            str.Append($"- {rubro}\n");
                        }
                        response += str;
                        response += "\n\nIngrese su rubro nuevamente:";
                        Singleton<StatusManager>.Instance.AgregarEstadoUsuario(message.UserId, "STATUS_REGISTER_EMPRENDEDOR_RUBRO");
                        return true;
                    }
                    
                }
                else if (check == "STATUS_REGISTER_EMPRENDEDOR_HABILITACIONES")
                {
                    if (Singleton<Datos>.Instance.CheckHabilitaciones(message.Text))
                    {
                        response = $"Su habilitacion es: {message.Text}.\n\nREGISTRO COMPLETO!!!.\n\nAhora eres un Emprendedor.";
    
                        foreach (UserEmprendedor user in Singleton<Datos>.Instance.ListaUsuariosRegistrados())
                        {
                            if (user.Id == message.UserId)
                            {
                                Habilitaciones newHabilitacion = new Habilitaciones(message.Text);
                                user.Emprendedor.Habilitacion = newHabilitacion;
                            }
                            
                        }

                        Singleton<StatusManager>.Instance.AgregarEstadoUsuario(message.UserId, "STATUS_IDLE");
                        
                        return true;
                    }
                    else
                    {
                        response = $"Habilitacion invalida.\nHabilitaciones validas: ";
                        StringBuilder str = new StringBuilder();
                        foreach (string habilitacion in Singleton<Datos>.Instance.ListaHabilitaciones())
                        {
                            str.Append($"- {habilitacion}\n");
                        }
                        response += str;
                        response += "\n\nIngrese su habilitacion nuevamente:";
                        Singleton<StatusManager>.Instance.AgregarEstadoUsuario(message.UserId, "STATUS_REGISTER_EMPRENDEDOR_HABILITACIONES");
                        return true;
                    }   
                }
            }
            response = string.Empty;
            return false;
        }
    }
}






