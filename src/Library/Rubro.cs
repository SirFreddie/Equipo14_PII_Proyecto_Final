using System;
using System.Collections;

namespace Proyecto_Final
{
    /// <summary>
    /// Esta clase representa el rubro de una empresa.
    /// </summary>
    public class Rubro
    {
        /// <summary>
        /// Retorna el nombre del rubro de una empresa.
        /// </summary>
        /// <value>Nombre del rubro de la empresa.</value>
        public string Rubros { get; }
        /// <summary>
        /// Inicializa la clase rubro.
        /// </summary>
        /// <param name="rubro"></param>
        public Rubro(string rubro)
        {
            this.Rubros = rubro;
        }
    }
}