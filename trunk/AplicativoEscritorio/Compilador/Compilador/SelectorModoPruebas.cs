using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Compilador
{
    public partial class SelectorModoPruebas : Form
    {
        public SelectorModoPruebas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Compilador comp = new Compilador();
            comp.ArchivoEntrada = System.Configuration.ConfigurationManager.AppSettings["archEntrada"].ToString();
            comp.Text = "Test";
            comp.WindowState = System.Windows.Forms.FormWindowState.Normal;
            comp.Size = new Size(300, 600);
            comp.ModoDebug = false;
            comp.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Compilador comp = new Compilador();
            comp.ArchivoEntrada = System.Configuration.ConfigurationManager.AppSettings["archValidacionesIncorrectas"].ToString();
            comp.Text = "Validaciones Incorrectas";
            comp.WindowState = System.Windows.Forms.FormWindowState.Normal;
            comp.Size = new Size(300, 600);
            comp.ModoDebug = false;
            comp.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Compilador comp = new Compilador();
            comp.ArchivoEntrada = System.Configuration.ConfigurationManager.AppSettings["archValidacionesCorrectas"].ToString();
            comp.Text = "Validaciones Correctas";
            comp.WindowState = System.Windows.Forms.FormWindowState.Normal;
            comp.Size = new Size(300, 600);
            comp.ModoDebug = false;
            comp.Show();
        }
    }
}
