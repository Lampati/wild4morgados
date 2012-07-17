using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ModoGrafico.ViewModels;
using InterfazTextoGrafico;

namespace ModoGrafico.Tabs
{
    public class TabItemFuncion : Tab
    {
        private int orden;

        public string retorno;
        public string Retorno
        {
            get
            {
                return retorno;
            }
            set
            {
                retorno = value;
                NotifyPropertyChanged("Retorno");
            }
        }

        public eTipoVariable tipoRetorno;
        public eTipoVariable TipoRetorno
        {
            get
            {
                return tipoRetorno;
            }
            set
            {
                tipoRetorno = value;
                NotifyPropertyChanged("TipoRetorno");
            }
        }


        public TabItemFuncion() : base()
        {
            this.orden = LibreriaActividades.Extension.AsignarOrdenTab();
            Tipo = Enums.TipoTab.TabItemFuncion;
        }

        public TabItemFuncion(ProcedimientoViewModel proc) : base()
        {
            actividadViewModel = proc;
            this.orden = LibreriaActividades.Extension.AsignarOrdenTab();
            Tipo = Enums.TipoTab.TabItemFuncion;

            header = proc.Nombre.ToUpper().Trim();
        }

        public override void ActualizarPropiedadesTab(Interfaces.IPropiedadesContexto props)
        {

        }

        public override Interfaces.IPropiedadesContexto ObtenerPropiedadesTab()
        {
            return null;
        }

        

        public override int Orden
        {
            get { return this.orden; }
        }
    }
}
