using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Compilador.Lexicografico;
using System.Configuration;
using System.IO;
using Compilador.Sintactico;
using Compilador.Sintactico.Gramatica;
using Compilador.Auxiliares;
using Compilador.Sintactico.TablaGramatica;

namespace Compilador
{
    public partial class Compilador : Form
    {
        public static string directorioActual;
        public delegate void ErrorCompiladorDelegate(string tipo, string desc, int fila, int col, bool parar);

        private bool errorSemantico = false;

     

        private AnalizadorSintactico analizadorSintactico;

        private bool errores = false;

        public string ArchivoEntrada { get; set; }
   

        public Compilador()
        {
            InitializeComponent(); 
        
            
        }

        private void Compilador_Load(object sender, EventArgs e)
        {
            try
            {
                directorioActual = Application.StartupPath;
                try
                {

                    Utils.Log.path = directorioActual;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al iniciar el log: " + ex.Message);
                }

                

                this.CargarArchivoEnInterfaz(ArchivoEntrada);

                this.CargarAnalizadorSintactico();

                this.CargarTablaEnInterfaz();

                this.analizadorSintactico.errorCompilacion += new ErrorCompiladorDelegate(Compilador_errorCompilacion);

                this.analizadorSintactico.ArbolSemantico.errorCompilacion += new ErrorCompiladorDelegate(Compilador_errorCompilacion);

                this.dataGridViewSintactico.Rows.Add(this.analizadorSintactico.Pila.ToString(), this.analizadorSintactico.CadenaEntrada.ToString());

                this.dataGridViewSintactico.CurrentCell = this.dataGridViewSintactico[0, this.dataGridViewSintactico.Rows.Count - 1];

                this.tabControl1.SelectedTab = tabPageSintactico;
                //this.analizadorSintactico.AnalizarSintacticamente();

                this.buttonGenerarCodigo.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }

        void Compilador_errorCompilacion(string tipo, string desc, int fila, int col, bool parar)
        {
            errores = true;

            this.dataGridViewErrores.Rows.Add(fila, col, tipo, desc );            

            

            switch (tipo)
            {
                case "Semantico":
                    this.errorSemantico = true;
                    this.dataGridViewErrores.Rows[this.dataGridViewErrores.Rows.Count - 2].DefaultCellStyle.BackColor = Color.OrangeRed;
                    break;
                case "Sintactico":                    
                    this.dataGridViewErrores.Rows[this.dataGridViewErrores.Rows.Count - 2].DefaultCellStyle.BackColor = Color.Red;
                    break;
            }

            this.dataGridViewErrores.CurrentCell = this.dataGridViewErrores[0, this.dataGridViewErrores.Rows.Count - 1];

            if (parar)
            {
                this.buttonAnalizadorSintactico.Enabled = false;
                this.buttonAnalizadorSintacticoTODO.Enabled = false;
            }
        }



        private void CargarAnalizadorSintactico()
        {
            try
            {
               string pathArchGramatica = Path.Combine(directorioActual,System.Configuration.ConfigurationManager.AppSettings["archGramatica"].ToString());
               analizadorSintactico = new AnalizadorSintactico(pathArchGramatica, ArchivoEntrada);
               analizadorSintactico.HabilitarSemantico = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fatal al iniciar el analizador sintactico:" + "\r\n" + ex.Message);

            }
        }

        private void CargarArchivoEnInterfaz(string arch)
        {
            try
            {
                StreamReader strReader = new StreamReader(Path.Combine(directorioActual,arch));

                this.textBoxArchivoFuente.Text = strReader.ReadToEnd();

                strReader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("No se encontro el archivo de entrada. Error Fatal");
            }
        }

        private void CargarTablaEnInterfaz()
        {
            try
            {
                foreach (Terminal t in this.analizadorSintactico.Gramatica.Terminales)
                {
                    this.dataGridViewTablaAnalisis.Columns.Add(EnumUtils.stringValueOf(t.Componente.Token), EnumUtils.stringValueOf(t.Componente.Token));
                }

                foreach (NoTerminal nt in this.analizadorSintactico.Gramatica.NoTerminales)
                {
                    DataGridViewRow dr = new DataGridViewRow();
                    dr.HeaderCell.Value = nt.ToString();


                    foreach (Terminal t in this.analizadorSintactico.Gramatica.Terminales)
                    {
                        //Produccion prod = this.analizadorSintactico.Tabla.BuscarEnTablaProduccion(nt, t, false);

                        NodoTablaAnalisisGramatica nodo = this.analizadorSintactico.Tabla.BuscarNodo(nt, t);


                        DataGridViewCell col = new DataGridViewTextBoxCell();

                        if (nodo != null)
                        {

                            if (nodo.EsSinc)
                            {
                                col.Value = "Sinc";
                            }
                            else
                            {
                                if (nodo.Produccion != null)
                                {
                                    col.Value = nodo.Produccion.ToString();

                                }
                                else
                                {
                                    col.Value = string.Empty;
                                }
                            }
                        }
                        else
                        {
                            col.Value = string.Empty;
                        }

                        dr.Cells.Add(col);

                    }

                    this.dataGridViewTablaAnalisis.Rows.Add(dr);

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la tabla de analisis");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {                

                ComponenteLexico com = this.analizadorSintactico.AnalizadorLexico.ObtenerProximoToken();

                this.labelLexema.Text = com.Lexema;
                this.labelToken.Text = com.Token.ToString();
                this.labelFila.Text = com.Fila.ToString();
                this.labelColumna.Text = com.Columna.ToString();
                //analizadorLex.AnalizarLexicograficamente();

                
                this.dataGridView1.Rows.Add(com.Lexema, com.Token, com.AntecedidoPorSeparador.ToString(), com.Fila, com.Columna);
                this.dataGridView1.CurrentCell = this.dataGridView1[0, this.dataGridView1.Rows.Count - 1];

                if (com.Token == ComponenteLexico.TokenType.EOF)
                {
                    this.button2.Enabled = false;
                    this.button1.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                Utils.Log.AddError(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ComponenteLexico com = this.analizadorSintactico.AnalizadorLexico.ObtenerProximoToken();


            while (com.Token != ComponenteLexico.TokenType.EOF)
            {
                this.dataGridView1.Rows.Add(com.Lexema, com.Token, com.AntecedidoPorSeparador.ToString(), com.Fila, com.Columna);

                com = this.analizadorSintactico.AnalizadorLexico.ObtenerProximoToken();
            }

            this.dataGridView1.Rows.Add(com.Lexema, com.Token, com.AntecedidoPorSeparador.ToString(), com.Fila, com.Columna);
            this.dataGridView1.CurrentCell = this.dataGridView1[0, this.dataGridView1.Rows.Count - 1];
            
            this.button2.Enabled = false;
            this.button1.Enabled = false;

            this.labelLexema.Text = string.Empty;
            this.labelToken.Text = string.Empty;
            this.labelFila.Text = string.Empty;
            this.labelColumna.Text = string.Empty;

            //com = this.analizadorLex.ObtenerProximoToken();
        }

        private void buttonResetear_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Clear();
            this.analizadorSintactico.ResetearAnalizadorLexicografico();

            this.button2.Enabled = true;
            this.button1.Enabled = true;

            this.labelLexema.Text = string.Empty;
            this.labelToken.Text = string.Empty;
            this.labelFila.Text = string.Empty;
            this.labelColumna.Text = string.Empty;
        }

        private void buttonAnalizadorSintactico_Click(object sender, EventArgs e)
        {
            bool error = this.analizadorSintactico.AnalizarSintacticamenteUnPaso();

            this.AgregarFilaSintactico(error);

            this.dataGridViewSintactico.CurrentCell = this.dataGridViewSintactico[0, this.dataGridViewSintactico.Rows.Count - 1];

            if (this.analizadorSintactico.esFinAnalisisSintactico() && !errores)
            {
                this.buttonGenerarCodigo.Enabled = true;
            }
        
        }

        private void buttonAnalizadorSintacticoTODO_Click(object sender, EventArgs e)
        {
            while (!this.analizadorSintactico.esFinAnalisisSintactico())
            {
                bool error= this.analizadorSintactico.AnalizarSintacticamenteUnPaso();

                this.AgregarFilaSintactico(error);

                this.dataGridViewSintactico.CurrentCell = this.dataGridViewSintactico[0, this.dataGridViewSintactico.Rows.Count - 1];
            }

            if (this.analizadorSintactico.esFinAnalisisSintactico()  &&  !errores)
            {
                this.buttonGenerarCodigo.Enabled = true;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i = 0;
            while (i < 50 && !this.analizadorSintactico.esFinAnalisisSintactico())
            {
                bool error = this.analizadorSintactico.AnalizarSintacticamenteUnPaso();

                this.AgregarFilaSintactico(error);

                i++;
            }

            if (this.analizadorSintactico.esFinAnalisisSintactico() && !errores)
            {
                this.buttonGenerarCodigo.Enabled = true;
            }
        }

        private void AgregarFilaSintactico(bool error)
        {

            this.dataGridViewSintactico.Rows.Add(this.analizadorSintactico.Pila.ToString(), this.analizadorSintactico.CadenaEntrada.ToString());

            if (this.errorSemantico)
            {
                this.dataGridViewSintactico.Rows[this.dataGridViewSintactico.Rows.Count - 1].DefaultCellStyle.BackColor = Color.OrangeRed;
                this.errorSemantico = false;
            }

            if (error)
            {
                this.dataGridViewSintactico.Rows[this.dataGridViewSintactico.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
            }

            this.dataGridViewSintactico.CurrentCell = this.dataGridViewSintactico[0, this.dataGridViewSintactico.Rows.Count - 1];

        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == this.tabPageArbolSemantico)
            {
                treeView1.Nodes.Clear();

                treeView1.Nodes.Add(this.analizadorSintactico.ArbolSemantico.DibujarArbol());                
                
            }
        }

        private void buttonGenerarCodigo_Click(object sender, EventArgs e)
        {
            textBoxCodigo.Text = string.Empty;

            this.analizadorSintactico.ArbolSemantico.CalcularExpresiones();

            this.analizadorSintactico.ArbolSemantico.CalcularMemoriaGlobal();
            //this.analizadorSintactico.ArbolSemantico.CalcularLugar();
            textBoxCodigo.Text += this.analizadorSintactico.ArbolSemantico.CalcularCodigo();
        }


    }
}
