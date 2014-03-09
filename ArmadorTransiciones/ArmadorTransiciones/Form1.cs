using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ArmadorTransiciones
{
    public partial class ArmadorTransiciones : Form
    {
        public ArmadorTransiciones()
        {
            InitializeComponent();
        }

        private void ArmadorTransiciones_Load(object sender, EventArgs e)
        {
            this.checkBoxEspecial.Enabled = true;
            this.checkBoxEspecial.Checked = false;
            this.checkBoxLetras.Enabled = false;
            this.checkBoxNumeros.Enabled = false;
            this.textBoxMenosLetras.Enabled = false;
            this.textBoxMenosNumeros.Enabled = false;
            this.textBoxMenosLetras.Text = String.Empty;
            this.textBoxMenosLetras.Text = String.Empty;
        }


        private void checkBoxEspecial_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxEspecial.Checked)
            {
                this.checkBoxLetras.Enabled = true;
                this.checkBoxNumeros.Enabled = true;
                this.textBoxMenosLetras.Enabled = true;
                this.textBoxMenosNumeros.Enabled = true;

                this.textBoxChar.Enabled = false;
            }
            else
            {
                this.checkBoxLetras.Enabled = false;
                this.checkBoxNumeros.Enabled = false;
                this.textBoxMenosLetras.Enabled = false;
                this.textBoxMenosNumeros.Enabled = false;

                this.textBoxChar.Enabled = true;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBoxResultado.Text = String.Empty;

            if (!this.checkBoxEspecial.Checked)
            {
                this.textBoxResultado.Text = this.textBoxIni.Text + "," + this.textBoxChar.Text + "," + this.textBoxFinal.Text;

            }
            else
            {
                String aux = String.Empty;
                String ini = this.textBoxIni.Text;
                String fin = this.textBoxFinal.Text;

                if (this.checkBoxLetras.Checked)
                {
                    /*
                    for (int i = 65; i <= 90; i++)
                    {
                        if (!this.textBoxMenosLetras.Text.Contains(Convert.ToChar(i).ToString()))
                        {
                            aux += ini + "," + Convert.ToChar(i).ToString() + "," + fin + ";";
                        }
                    }
                    */
                    for (int i = 97; i <= 122; i++)
                    {
                        if (!this.textBoxMenosLetras.Text.Contains(Convert.ToChar(i).ToString()))
                        {
                            aux += ini + "," + Convert.ToChar(i).ToString() + "," + fin + ";";
                        }
                    }
                     
                }

                if (this.checkBoxNumeros.Checked)
                {
                    for (int i = 48; i <= 57; i++)
                    {
                        if (!this.textBoxMenosNumeros.Text.Contains(Convert.ToChar(i).ToString()))
                        {
                            aux += ini + "," + Convert.ToChar(i).ToString() + "," + fin + ";";
                        }
                    }

                }

                this.textBoxResultado.Text = aux.Substring(0, aux.Length - 1);
            }

            this.AgregarAArchivo();
        }

        private void AgregarAArchivo()
        {
            StreamWriter sr = new StreamWriter(@"C:\tempData.txt",true);

            sr.WriteLine(this.textBoxResultado.Text);

            sr.Close();
            
        }







    }
}
