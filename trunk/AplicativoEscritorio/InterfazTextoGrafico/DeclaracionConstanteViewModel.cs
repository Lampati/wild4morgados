using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace InterfazTextoGrafico
{
    public class DeclaracionConstanteViewModel : ActividadViewModelBase
    {
        public string Nombre {get; set;}
        public InterfazTextoGrafico.Enums.TipoDato Tipo {get; set;}
        public string Valor {get; set;}

        public override string Gargar
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();

                switch (Tipo)
                {
                    case InterfazTextoGrafico.Enums.TipoDato.Numero:
                        strBldr.AppendFormat("const {0} : {1} = {2};", Nombre, Tipo.ToString(), Valor);
                        break;
                    case InterfazTextoGrafico.Enums.TipoDato.Texto:
                        strBldr.AppendFormat("const {0} : {1} = '{2}';", Nombre, Tipo.ToString(), Valor);
                        break;
                    case InterfazTextoGrafico.Enums.TipoDato.Booleano:
                        strBldr.AppendFormat("const {0} : {1} = {2};", Nombre, Tipo.ToString(), Valor);
                        break;
                }

                return strBldr.ToString();
            }
        }
    }
}
