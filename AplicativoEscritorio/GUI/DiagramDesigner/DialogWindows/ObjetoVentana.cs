using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagramDesigner.DialogWindows
{
    public class ObjetoVentana
    {
        private System.Windows.Controls.TextBlock texto;
        private List<System.Windows.Controls.Control> ctrls;

        public ObjetoVentana() { }

        public ObjetoVentana(System.Windows.Controls.TextBlock texto)
        {
            this.texto = texto;
        }

        public ObjetoVentana(System.Windows.Controls.TextBlock texto, System.Windows.Controls.Control ctrl)
        {
            this.texto = texto;
            this.ctrls = new List<System.Windows.Controls.Control>();
            this.ctrls.Add(ctrl);
        }

        public ObjetoVentana(System.Windows.Controls.TextBlock texto, List<System.Windows.Controls.Control> ctrls)
        {
            this.texto = texto;
            this.ctrls = ctrls;
        }

        public System.Windows.Controls.TextBlock Texto
        {
            get { return this.texto; }
        }

        public List<System.Windows.Controls.Control> Controls
        {
            get
            {
                if (this.ctrls != null && this.ctrls.Count > 0)
                    return this.ctrls;

                return null;
            }
        }

        public bool SeCompleto
        {
            get { return this.texto != null || this.ctrls != null; }
        }
    }
}
