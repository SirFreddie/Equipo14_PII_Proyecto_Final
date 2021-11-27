
using Ucu.Poo.Locations.Client;
using System.Text;
using System.Collections.Generic;

namespace Proyecto_Final
{
    /// <summary>
    /// 
    /// </summary>
    public class Buscador
    {
        /// <summary>
        /// La función de esta clase es en base a las ofertas guardadas, buscarlas en base a ciertos criterios.
        /// Debido a que la única responsabilidad de esta clase es buscar ofertas según distintos criterios, esta sigue el SRP.
        /// </summary>
        /// <value></value>
        public string Content
        {
            get
            {
                return this.ContentBuilder.ToString();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private StringBuilder ContentBuilder { get; } = new StringBuilder();

        
        /// <summary>
        /// /// En base a la ubicación del Emprendedor, retorna una lista con todas las ofertas que se encuentren a una distancia de 10km o menos; utilizando el LocationApi <see cref="LocationApiClient"/>.
        /// </summary>
        /// <param name="direccion"></param>
        public void VerOfertasUbicacion(string direccion) //(SRP)
        {
            ContentBuilder.Clear();
            
            LocationApiClient client = new LocationApiClient();
            Location ubicacionEmprendedor = client.GetLocation(direccion);

            foreach(Oferta oferta in Singleton<Datos>.Instance.ListaOfertas())
            {
                Location ubicacionOferta = client.GetLocation(oferta.Product.Ubicacion);
                Distance distance = client.GetDistance(ubicacionEmprendedor,ubicacionOferta);
                if (distance.TravelDistance <= 10.0)
                {                    
                    if (distance.TravelDistance <= 10.0)
                    {
                        ContentBuilder.Append($"Esta oferta está a {distance.TravelDistance}km de su ubicación: \nID: {oferta.Id} \nNombre: {oferta.Nombre} \nProducto:{oferta.Product.Nombre} \nDescripción: {oferta.Product.Descripcion} \nTipo: {oferta.Product.Tipo.Nombre} \nUbicación: {oferta.Product.Ubicacion} \nValor: {oferta.Product.MonetaryValue()}{oferta.Product.Valor} \nCantidad: {oferta.Product.Cantidad} \nHabilitaciones requeridas: {oferta.HabilitacionesOferta.Habilitacion} \n");
                    }  
                } 
            }
            if(ContentBuilder.ToString() == "")
            {
                ContentBuilder.Append("No se encontraron ofertas que estén en su cercanía.");
            }
        }
        

        /// <summary>
        /// En base a una palabra clave recibida, otorga todas las ofertas que tengan la misma palabra clave
        /// </summary>
        /// <param name="palabraClave"></param>
        /// <returns></returns>
        public void VerOfertasPalabraClave(string palabraClave) //(SRP)
        {
            ContentBuilder.Clear();
            foreach(Oferta oferta in Singleton<Datos>.Instance.ListaOfertas())
            {
                foreach(string palabrasClave in oferta.PalabrasClave)
                {
                    if(palabraClave.ToLower() == palabrasClave.ToLower())
                    {
                        ContentBuilder.Append($"Esta oferta concuerda con la palabra clave que colocó: \nID: {oferta.Id} \nNombre: {oferta.Nombre} \nProducto:{oferta.Product.Nombre} \nDescripción: {oferta.Product.Descripcion} \nTipo: {oferta.Product.Tipo.Nombre} \nUbicación: {oferta.Product.Ubicacion} \nValor: {oferta.Product.MonetaryValue()}{oferta.Product.Valor} \nCantidad: {oferta.Product.Cantidad} \nHabilitaciones requeridas: {oferta.HabilitacionesOferta.Habilitacion} \n");
                    }
                }
            }
            if(ContentBuilder.ToString() == "")
            {
                ContentBuilder.Append("No se encontraron ofertas que concuerdan con esa palabra clave.");
            }   
        }

        /// <summary>
        /// En base a un tipo de producto recibido, otorga todas las ofertas que tengan el mismo tipo
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public void VerOfertasTipo(string tipo) //(SRP)
        {
            ContentBuilder.Clear();
            foreach(Oferta oferta in Singleton<Datos>.Instance.ListaOfertas())
            {
                if(tipo == oferta.Product.Tipo.Nombre)
                {
                    ContentBuilder.Append($"Esta oferta concuerda con el tipo que describió: \nID: {oferta.Id} \nNombre: {oferta.Nombre} \nProducto:{oferta.Product.Nombre} \nDescripción: {oferta.Product.Descripcion} \nTipo: {oferta.Product.Tipo.Nombre} \nUbicación: {oferta.Product.Ubicacion} \nValor: {oferta.Product.MonetaryValue()}{oferta.Product.Valor} \nCantidad: {oferta.Product.Cantidad} \nHabilitaciones requeridas: {oferta.HabilitacionesOferta.Habilitacion} \n");
                }
            }
            if(ContentBuilder.ToString() == "")
            {
                ContentBuilder.Append("No se encontraron ofertas que concuerden con el tipo de producto deseado.");
            }
        } 

        /// <summary>
        /// Otorga todas las ofertas que sean recurrentes.
        /// </summary>
        /// <returns></returns>
        public void VerOfertasRecurrentes()
        {
            ContentBuilder.Clear();
            foreach(Oferta oferta in Singleton<Datos>.Instance.ListaOfertas())
            {
                if(oferta.IsRecurrente)
                {
                    ContentBuilder.Append($"Esta oferta es recurrente: \nID: {oferta.Id} \nNombre: {oferta.Nombre} \nProducto:{oferta.Product.Nombre} \nDescripción: {oferta.Product.Descripcion} \nTipo: {oferta.Product.Tipo.Nombre} \nUbicación: {oferta.Product.Ubicacion} \nValor: {oferta.Product.MonetaryValue()}{oferta.Product.Valor} \nCantidad: {oferta.Product.Cantidad} \nHabilitaciones requeridas: {oferta.HabilitacionesOferta.Habilitacion} \n");
                }
            }
            if(ContentBuilder.ToString() == "")
            {
                ContentBuilder.Append("No se encontraron ofertas recurrentes.");
            }
        } 

        /// <summary>
        /// Otorga todas las ofertas que sean puntuales.
        /// </summary>
        /// <returns></returns>
        public void VerOfertasPuntuales()
        {
            ContentBuilder.Clear();
            foreach(Oferta oferta in Singleton<Datos>.Instance.ListaOfertas())
            {
                if(!oferta.IsRecurrente)
                {
                    ContentBuilder.Append($"Esta oferta es puntual: \nID: {oferta.Id} \nNombre: {oferta.Nombre} \nProducto:{oferta.Product.Nombre} \nDescripción: {oferta.Product.Descripcion} \nTipo: {oferta.Product.Tipo.Nombre} \nUbicación: {oferta.Product.Ubicacion} \nValor: {oferta.Product.MonetaryValue()}{oferta.Product.Valor} \nCantidad: {oferta.Product.Cantidad} \nHabilitaciones requeridas: {oferta.HabilitacionesOferta.Habilitacion} \n");
                }
            }
            if(ContentBuilder.ToString() == "")
            {
                ContentBuilder.Append("No se encontraron ofertas puntuales.");
            }
        }    
    }
}