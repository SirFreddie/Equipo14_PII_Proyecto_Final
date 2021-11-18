using System;
using System.Collections;
using System.Collections.Generic;

namespace Proyecto_Final
{
    /// <summary>
    /// Esta clase representa al usuario de la Empresa.
    /// </summary>
    public class UserEmpresa : IUser
    {
        private bool isInvited = false;
        
        /// <summary>
        /// Otorga el id del usuario.
        /// </summary>
        /// <value>Id del usuario.</value>
        public string Id { get; set; }

        /// <summary>
        /// Obtiene un valor del nombre del usuario empresa.
        /// </summary>
        /// <value>Nombre de la empresa</value>
        public string Nombre { get; }
        
        /// <summary>
        /// Obtiene un valor del objeto Empresa.
        /// </summary>
        /// <value>Objeto del tipo Empresa</value>
        public Empresa Empresa { get; set; }

        /// <summary>
        /// Obtiene un valor booleano dependiendo de si la empresa fue invitada o no.
        /// </summary>
        /// <value><c>true/false</c></value>
        public bool IsInvited { get { return isInvited; } private set { this.isInvited = value;} }

        /// <summary>
        /// Inicializa la clase UserEmpresa.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        public UserEmpresa(string id, string nombre)
        {
            this.Id = id;
            this.Nombre = nombre;
        }

        /// <summary>
        /// Agrega un rubro.
        /// </summary>
        /// <param name="rubro"></param>
        public void AgregarRubro(string rubro)
        {
            this.Empresa.AgregarRubro(rubro);
        }

        /// <summary>
        /// El usuario puede crear la empresa.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="ubicacion"></param>
        /// <param name="rubro"></param>
        public void CrearEmpresa(string nombre, string ubicacion, string rubro) // (Creator)
        {
            Rubro newRubro = new Rubro(rubro);
            Empresa newEmpresa = new Empresa(nombre, ubicacion, newRubro);

            this.Empresa = newEmpresa;
            Singleton<Datos>.Instance.AgregarEmpresa(newEmpresa);
        }

        /// <summary>
        /// Como empresa, quiero indicar un conjunto de palabras claves asociadas a la publicación de los materiales, para que de esa forma sea más fácil de encontrarlos en las búsquedas que hacen los emprendedores.
        /// </summary>
        /// <param name="datosMensaje"></param>
        public void CrearMsjClave((string, string) datosMensaje) 
        {
            this.Empresa.AgregarMsjClave((datosMensaje.Item1, datosMensaje.Item2)); // (Delegacion)
        }

        /// <summary>
        /// Como empresa, quiero publicar una oferta de materiales reciclables o residuos, para que de esa forma los emprendedores que lo necesiten puedan reutilizarlos.
        /// </summary>
        /// <param name="datosOferta"></param>
        /// <param name="datosHabilitacion"></param>
        /// <param name="nombreProducto"></param>
        /// <param name="descripcionProducto"></param>
        /// <param name="ubicacionProducto"></param>
        /// <param name="valorProducto"></param>
        /// <param name="cantidadProducto"></param>
        /// <param name="datosTipoProducto"></param>
        public void CrearOferta(string datosOferta, string datosHabilitacion, string nombreProducto, string descripcionProducto, string ubicacionProducto, int valorProducto, int cantidadProducto, string datosTipoProducto) // (Creator)
        {
            Producto producto = this.CrearProducto(nombreProducto, descripcionProducto, ubicacionProducto, valorProducto, cantidadProducto, datosTipoProducto);
            Habilitaciones habilitacion = new Habilitaciones(datosHabilitacion);
            Oferta newOferta = new Oferta(datosOferta, producto, habilitacion);

            this.Empresa.Ofertas.Add(newOferta);
            Singleton<Datos>.Instance.AgregarOferta(newOferta);
        }

        /// <summary>
        /// Como empresa, quiero clasificar los materiales o residuos, indicar su cantidad y unidad, el valor (en $ o U$S) de los mismos y el lugar donde se ubican, para que de esa forma los emprendedores tengan información de materiales o residuos disponibles.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="descripcion"></param>
        /// <param name="ubicacion"></param>
        /// <param name="valor"></param>
        /// <param name="cantidad"></param>
        /// <param name="datosTipoProducto"></param>
        /// <returns></returns>
        public Producto CrearProducto(string nombre, string descripcion, string ubicacion, int valor, int cantidad, string datosTipoProducto) // (Creator)
        {
            TipoProducto newTipoProducto = new TipoProducto(datosTipoProducto);
            Producto newProducto = new Producto(nombre, descripcion, ubicacion, valor, cantidad, newTipoProducto);

            return newProducto;
        }

        /// <summary>
        /// Cambia el estado de la oferta especifica a vendido.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="nombreOferta"></param>
        /// <param name="nombreEmprendedor"></param>
        public void ConcretarOferta(string input, string nombreOferta, string nombreEmprendedor)
        {
            if (input == "Y")
            {
                foreach (Oferta oferta in this.Empresa.Ofertas)
                {
                    if (oferta.Nombre == nombreOferta)
                    {
                        foreach (UserEmprendedor emprendedor in Singleton<Datos>.Instance.ListaUsuarioEmprendedor())
                        {
                            if (emprendedor.Nombre == nombreEmprendedor)
                            {
                                oferta.IsVendido = true;
                                oferta.Comprador = emprendedor;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Como empresa, quiero saber todos los materiales o residuos entregados en un período de tiempo, para de esa forma tener un seguimiento de su reutilización.
        /// </summary>
        /// <returns>Retorna un diccionario con los datos de las ventas</returns>
        public Dictionary<string, int> VerificarVentas()
        {
            return this.Empresa.VerificarVentas(); // (Delegacion)
        }
    }
}