using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Entidades
{
    public class TestPrueba
    {
        #region Atributos
        private string propiedadA;
        #endregion

        #region Propiedades
        #endregion

        #region Constructores
        public TestPrueba() { }

        public TestPrueba(string propiedadA)
        {
            this.propiedadA = propiedadA;
        }
        #endregion

        #region Métodos
        public override string ToString()
        {
            /*Acá concatenar todas las propiedades para luego hacer el hash (por ahora es sólo 1 propiedad "propiedadA")*/
            return this.propiedadA;
        }
        #endregion
    }
}
