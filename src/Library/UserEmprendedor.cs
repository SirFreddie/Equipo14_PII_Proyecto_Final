using System.Collections;
using Ucu.Poo.Locations.Client;
using System.Threading.Tasks;

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
        public string Nombre { get; set;}

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
        /// DEBUG: Setea un emprendedor al usuario
        /// </summary>
        /// <param name="emprendedor"></param>
        public void SetEmprendedor(Emprendedor emprendedor)
        {
            this.Emprendedor = emprendedor;
        }
        /// <summary>
        /// Como emprendedor, quiero saber cuántos materiales o residuos consumí en un período de tiempo, para de esa forma tener un control de mis insumos.
        /// </summary>
        public void ConsumoXTiempo()
        {
            this.Emprendedor.ConsumoXTiempo();
        }
        
        /// <summary>
        /// En base a la ubicación del Emprendedor, retorna una lista con todas las ofertas que se encuentren a una distancia de 10km o menos; utilizando el LocationApi <see cref="LocationApiClient"/>.
        /// </summary>
        public string VerOfertasPalabraClave(string palabraClave)
        {
            Buscador buscador = new Buscador();
            buscador.VerOfertasPalabraClave(palabraClave);
            return buscador.Content;
        }
        /// <summary>
        /// En base a un tipo de producto recibido, otorga todas las ofertas que tengan el mismo tipo
        /// </summary>
        public string VerOfertasUbicacion()
        {
            Buscador buscador = new Buscador();
            buscador.VerOfertasUbicacion(this.Emprendedor.Ubicacion);
            return buscador.Content;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipo"></param>
        public void VerOfertasTipo(string tipo)
        {
            Buscador buscador = new Buscador();
            buscador.VerOfertasTipo(tipo);
        }
    }
}