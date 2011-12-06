using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text;

namespace Compilador
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Compilador comp = new Compilador();
                
                Application.Run(comp);
            }
            catch (Exception ex)
            {
                if (!Debugger.IsAttached)
                {
                    MessageBox.Show("Error fatal en el compilador.");
                }
                else
                {                    
                    MessageBox.Show(new StringBuilder(ex.Message).AppendLine().AppendLine(ex.Source).AppendLine(ex.StackTrace).ToString());
                }

                Application.Exit();
            }
        }
    }
}
