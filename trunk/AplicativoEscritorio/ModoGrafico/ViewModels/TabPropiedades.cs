using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModoGrafico.Interfaces;
using InterfazTextoGrafico.Enums;
using InterfazTextoGrafico;

namespace ModoGrafico.ViewModels
{
    public class TabPropiedades : IPropiedadesContexto
    {
        private string nombre;
        private TipoDato tipoRetorno;
        private string retorno;
        private List<ParametroViewModel> parametros = new List<ParametroViewModel>();
        

        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;
            }
        }

        public TipoDato TipoRetorno
        {
            get
            {
                return tipoRetorno;
            }
            set
            {
                tipoRetorno = value;
            }
        }

        public string Retorno
        {
            get
            {
                return retorno;
            }
            set
            {
                retorno = value;
            }
        }

        public List<ParametroViewModel> Parametros
        {
            get
            {
                return parametros;
            }
            set
            {
                parametros = value;
            }
        }

      
    }
}
