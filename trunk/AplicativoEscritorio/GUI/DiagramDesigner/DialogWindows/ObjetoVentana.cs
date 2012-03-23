using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagramDesigner.DialogWindows
{
    public class ObjetoVentana
    {
        private string titulo;
        private List<System.Windows.Controls.Control> ctrls;

        public ObjetoVentana() { }

        public ObjetoVentana(string titulo, System.Windows.Controls.Control ctrl)
        {
            this.titulo = titulo;
            this.ctrls = new List<System.Windows.Controls.Control>();
            this.ctrls.Add(ctrl);
        }

        public ObjetoVentana(string titulo, List<System.Windows.Controls.Control> ctrls)
        {
            this.titulo = titulo;
            this.ctrls = ctrls;
        }
    }
}
