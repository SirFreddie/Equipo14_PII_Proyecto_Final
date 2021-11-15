using System;

namespace Proyecto_Final
{
    /// <summary>
    /// Esta clase representa a los administradores del programa.
    /// </summary>
    public class UserAdmin : IUser
    {
        /// <summary>
        /// Otorga el nombre de usuario del administrador.
        /// </summary>
        /// <value>Nombre de usuario del administrador</value>
        
        public string Nombre { get; }

        /// <summary>
        /// String que indica qué clase es para el atributo "Es".
        /// </summary>
        private string es  = "Admin";

        /// <summary>
        /// Identificador del tipo de clase.
        /// </summary>
        /// <value>String "Admin".</value>
        public string Es
        {
            get
            {
                return this.Es;
            }
        
            set
            {
                this.Es = es;
            }
        }
        /// <summary>
        /// Inicializa la clase UserAdmin.
        /// </summary>
        /// <param name="nombre"></param>
        public UserAdmin(string nombre)
        {
            this.Nombre = nombre;
        }

        /// <summary>
        /// Invita a una empresa desde cualquier IUserInterface siempre y cuando esta empresa no haya sido invitada.
        /// </summary>
        /// <param name="nombreUserEmpresa"></param>
        public void InvitarEmpresa(string nombreUserEmpresa)
        {
            foreach (UserEmpresa user in Singleton<Datos>.Instance.ListaUsuarioEmpresa())
            {
                if (user.Nombre == nombreUserEmpresa)
                {
                    user.Invitacion = new Invitacion();
                }
            }
        }
    }
}