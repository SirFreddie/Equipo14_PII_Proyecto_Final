using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto_Final
{   
    /// <summary>
    /// Clase encargada de interactuar con el usuario a travez de la consola.
    /// </summary>
    public class ConsoleInteraction : IUserInterface
    {
        /// <summary>
        /// Inicializa la clase ConsoleInteraction.
        /// </summary>
        public ConsoleInteraction()
        {
            // Constructor.
        }

        /// <summary>
        /// Interactua con el usuario para crear una oferta.
        /// </summary>
        /// <returns>Datos para crear la oferta/publicacion.</returns>
        public string CrearOferta() // (SRP)
        {
            Console.WriteLine("Ingrese el nombre de su publicacion: ");
            string nombre = Console.ReadLine();
            return nombre;
        }

        /// <summary>
        /// Interactua con el usuario para aceptar la invitacion.
        /// </summary>
        /// <returns>Si se acepta la invitacion devuelve <c>true</c>, de lo contrario <c>false</c>.</returns>
        public bool AceptarInvitacion() // (SRP)
        {
            Console.WriteLine("Aceptar invitacion? Y/N: ");
            string input = Console.ReadLine().ToUpper();   
            if (input == "Y")
            {
                Console.WriteLine("Invitacion aceptada!");
                return true;
            }
            else
            {
                Console.WriteLine("Invitacion rechazada...");
                return false;
            }
        }

        /// <summary>
        /// Interactua con el usuario para crear los datos respectivos de la empresa.
        /// </summary>
        /// <returns>Tupla de strings con los datos de la empresa.</returns>
        public (string, string, string) CrearDatosEmpresa() // (SRP)
        {
            Console.WriteLine("Ingrese el nombre de su empresa: ");
            string nombre = Console.ReadLine(); 
            Console.WriteLine("Ingrese la direccion de su empresa: ");
            string ubicacion = Console.ReadLine();
            Console.WriteLine("Ingrese el rubro de su empresa: ");
            string rubro = Console.ReadLine();
            while (Singleton<Datos>.Instance.CheckRubros(rubro) == false)
            {
                // TODO: Imprimir los rubros disponibles.
                Console.WriteLine("Ingrese el rubro de su empresa nuevamente: ");
                rubro = Console.ReadLine();
            }

            (string, string, string) auxTuple = (nombre, ubicacion, rubro);
            return auxTuple;
        }

        /// <summary>
        /// Interactua con el usuario para poder crear los datos de la habilitacion.
        /// </summary>
        /// <returns>Habilitacion.</returns>
        public string CrearDatosHabilitacion() // (SRP)
        {
            Console.WriteLine("Ingrese la habilitacion necesaria para el retiro de su producto: ");
            string habilitacion = Console.ReadLine();
            while (Singleton<Datos>.Instance.CheckHabilitaciones(habilitacion) == false)
            {
                // TODO: Imprimir las habilitaciones disponibles.
                Console.WriteLine("Ingrese la habilitacion necesaria para el retiro de su producto nuevamente: ");
                habilitacion = Console.ReadLine();
            }

            return habilitacion;
        }

        /// <summary>
        /// Interactua con el usuario para poder crear los datos del producto.
        /// </summary>
        /// <returns>Nombre, descripcion, ubicacion, valor y cantidad del producto.</returns>
        public (string, string, string, int, int) CrearDatosProducto() // (SRP)
        {
            Console.WriteLine("Ingrese el nombre del producto: ");
            string nombre = Console.ReadLine(); 
            Console.WriteLine("Ingrese la descripcion de su producto: ");
            string descripcion = Console.ReadLine();
            Console.WriteLine("Ingrese la ubicacion de su producto: ");
            string ubicacion = Console.ReadLine();
            Console.WriteLine("Ingrese el valor unitario de su producto: ");
            string valor = Console.ReadLine();
            Console.WriteLine("Ingrese la cantidad de su producto: ");
            string cantidad = Console.ReadLine();

            (string, string, string, int, int) auxTuple = (nombre, descripcion, ubicacion, Convert.ToInt32(valor), Convert.ToInt32(cantidad));
            return auxTuple;
        }

        /// <summary>
        /// Interactua con el usuario para crear los datos de la publicacion.
        /// </summary>
        /// <returns>Nombre de la publicacion</returns>
        public string CrearDatosOferta()
        {
            Console.WriteLine("Ingrese el nombre de su publicacion: ");
            string nombre = Console.ReadLine();
            return nombre;
        }

        /// <summary>
        /// Interactua con el usuario para crear los datos del tipo de producto.
        /// </summary>
        /// <returns>Tipo de producto.</returns>
        public string CrearDatosTipoProducto() // (SRP)
        {
            Console.WriteLine("Ingrese el tipo de su producto: ");
            string tipo = Console.ReadLine();
            while (Singleton<Datos>.Instance.CheckTipos(tipo) == false)
            {
                // TODO: Imprimir los tipos disponibles.
                Console.WriteLine("Ingrese el tipo de su producto nuevamente: ");
                tipo = Console.ReadLine();
            }

            return tipo;
        }

        /// <summary>
        /// Interactua con el usuario Empresa para agregar una especializacion.
        /// </summary>
        /// <param name="empresa"></param>
        public void AgregarEspecializacion(Empresa empresa) // (SRP)
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("Ingrese la especializacion a agregar: ");
                string esp = Console.ReadLine();
                empresa.AgregarEspecializacion(esp);
                Console.WriteLine("Quiere agregar otra especializacion? Y/N: ");
                string input = Console.ReadLine().ToUpper();
                if (input != "Y")
                {
                    loop = false;
                }
            }   
        }

        /// <summary>
        /// Interactua con el usuario y le envia un mensaje de aviso que la empresa ya fue invitada y aceptada.
        /// </summary>
        public void AlreadyInvitedMsg()
        {
            Console.WriteLine("Esta empresa ya fue invitada y aceptada.");
        }

        /// <summary>
        /// Interactua con el usuario para crear un mensaje clave en una oferta especifica.
        /// </summary>
        /// <param name="oferta"></param>
        public void AgregarMsjClave(Oferta oferta)
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("Ingrese la palabra clave a agregar: ");
                string palabra = Console.ReadLine();
                oferta.AgregarMsjClave(palabra);
                Console.WriteLine("Quiere agregar otra palabra? Y/N: ");
                string input = Console.ReadLine().ToUpper();
                if (input != "Y")
                {
                    loop = false;
                }
            }   
        }

        /// <summary>
        /// Interactua con el usuario para concretar una oferta.
        /// </summary>
        /// <returns>Retorna <c>true</c> si se concreta la oferta, de lo contrario retorna <c>false</c>.</returns>
        public bool ConcretarOferta()
        {
            Console.WriteLine("Quieres concretar esta oferta? Y/N: ");
            string input = Console.ReadLine().ToUpper();
            
            return (input == "Y") ? true : false;
        }

        /// <summary>
        /// Imprime en consola los materiales y la cantidad vendida a lo largo de la historia.
        /// </summary>
        /// <param name="item"></param>
        public void ImprimirVendidos(KeyValuePair<string, int> item)
        {
            Console.WriteLine("Material = {0} || Cantidad = {1}", item.Key, item.Value);
        }
    }
}