using System;
using System.Collections;
using System.Collections.Generic;

namespace Proyecto_Final
{
    /// <summary>
    /// Esta clase representa a los usuarios emprendedores en el sistema.
    /// </summary>
    public class UserEmprendedor
    {
        /// <summary>
        /// Otorga el nombre del Emprendedor.
        /// </summary>
        /// <value>Nombre del Emprendedor.</value>
        public string Nombre { get; }

        /// <summary>
        /// Otorga los datos existentes en el objeto Emprendedor <see cref="Emprendedor"/>.
        /// </summary>
        /// <value></value>
        public Emprendedor Emprendedor { get; private set; }

        /// <summary>
        /// Inicializa la clase UserEmprendedor.
        /// </summary>
        /// <param name="nombre"></param>
        public UserEmprendedor(string nombre)
        {
            this.Nombre = nombre;
        }

        /// <summary>
        /// Registra el Emprendedor.
        /// </summary>
        public void RegistrarEmprendedor(IUserInterface userInterface)
        {

        }

        /// <summary>
        /// Agrega a la lista de especializaciones que contiene la clase "Emprendedor" una especialización.
        /// </summary>
        public void AgregarEspecializacion()
        {
            this.Emprendedor.AgregarEspecializacion();
        }
        
        /// <summary>
        /// Elimina de la lista de especializaciones que contiene la clase "Emprendedor una especialización.
        /// </summary>
        public void EliminarEspecializacion()
        {
            this.Emprendedor.EliminarEspecializacion();
        }
        /// <summary>
        /// 
        /// </summary>
        public void EnviarMsj()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public void VerOfertasPalabraClave()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public void VerOfertasUbicacion()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public void CargarInfo()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public void GuardarInfo()
        {

        }
        
    }
}