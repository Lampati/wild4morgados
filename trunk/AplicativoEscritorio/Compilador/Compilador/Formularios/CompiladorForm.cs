using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CompiladorGargar.Lexicografico;
using System.Configuration;
using System.IO;
using CompiladorGargar.Sintactico;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Auxiliares;
using CompiladorGargar.Sintactico.TablaGramatica;
using Utilidades;
using CompiladorGargar.Resultado.Auxiliares;
using CompiladorGargar.Resultado;

namespace CompiladorGargar
{
    internal partial class CompiladorForm : Form
    {
        public static string directorioActual;
        public delegate void ErrorCompiladorDelegate(string tipo, string desc, int fila, int col, bool parar);

 

        private bool errores = false;

        public string ArchivoEntrada { get; set; }

        public bool ModoDebug { get; set; }

        private Compilador compilador;
   

        public CompiladorForm( bool modo)
        {
            InitializeComponent();

            this.ModoDebug = modo;
            directorioActual = Application.StartupPath;

            

            string pathArchGramatica = Path.Combine(directorioActual, System.Configuration.ConfigurationManager.AppSettings["archGramatica"].ToString());

            this.compilador = new Compilador(pathArchGramatica, modo, directorioActual, directorioActual, "prueba");
            
        }

        private void Compilador_Load(object sender, EventArgs e)
        {
            try
            {
               
                try
                {

                    Utils.Log.path = directorioActual;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al iniciar el log: " + ex.Message);
                }

                this.buttonAnalizadorSintactico.Enabled = false;
                this.button3.Enabled = false;


                if (ModoDebug)
                {
                    this.CargarArchivoEnInterfaz(ArchivoEntrada);
                    //this.CargarTablaEnInterfaz();
                    
                    
                    //this.dataGridViewSintactico.Rows.Add(this.analizadorSintactico.Pila.ToString(), this.analizadorSintactico.CadenaEntrada.ToString());
                    //this.dataGridViewSintactico.CurrentCell = this.dataGridViewSintactico[0, this.dataGridViewSintactico.Rows.Count - 1];

                    this.buttonAnalizadorSintactico.Enabled = true;
                    this.button3.Enabled = true;
                }

                this.txtBxIDE.Text = ObtenerTodoElTexto();

                this.tabControl1.SelectedTab = tabPageIDE;
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
            //errores = true;

            //this.dataGridViewErrores.Rows.Add(fila, col, tipo, desc);



            //switch (tipo)
            //{
            //    case "Semantico":
            //        this.errorSemantico = true;
            //        this.dataGridViewErrores.Rows[this.dataGridViewErrores.Rows.Count - 2].DefaultCellStyle.BackColor = Color.OrangeRed;
            //        break;
            //    case "Sintactico":
            //        this.dataGridViewErrores.Rows[this.dataGridViewErrores.Rows.Count - 2].DefaultCellStyle.BackColor = Color.Red;
            //        break;
            //}

            //this.dataGridViewErrores.CurrentCell = this.dataGridViewErrores[0, this.dataGridViewErrores.Rows.Count - 1];

            //if (parar)
            //{
            //    this.buttonAnalizadorSintactico.Enabled = false;
            //    this.buttonAnalizadorSintacticoTODO.Enabled = false;
            //}
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

        //private void CargarTablaEnInterfaz()
        //{
        //    try
        //    {
        //        foreach (Terminal t in this.analizadorSintactico.Gramatica.Terminales)
        //        {
        //            this.dataGridViewTablaAnalisis.Columns.Add(EnumUtils.stringValueOf(t.Componente.Token), EnumUtils.stringValueOf(t.Componente.Token));
        //        }

        //        foreach (NoTerminal nt in this.analizadorSintactico.Gramatica.NoTerminales)
        //        {
        //            DataGridViewRow dr = new DataGridViewRow();
        //            dr.HeaderCell.Value = nt.ToString();


        //            foreach (Terminal t in this.analizadorSintactico.Gramatica.Terminales)
        //            {
        //                //Produccion prod = this.analizadorSintactico.Tabla.BuscarEnTablaProduccion(nt, t, false);

        //                NodoTablaAnalisisGramatica nodo = this.analizadorSintactico.Tabla.BuscarNodo(nt, t);


        //                DataGridViewCell col = new DataGridViewTextBoxCell();

        //                if (nodo != null)
        //                {

        //                    if (nodo.EsSinc)
        //                    {
        //                        col.Value = "Sinc";
        //                    }
        //                    else
        //                    {
        //                        if (nodo.Produccion != null)
        //                        {
        //                            col.Value = nodo.Produccion.ToString();

        //                        }
        //                        else
        //                        {
        //                            col.Value = string.Empty;
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    col.Value = string.Empty;
        //                }

        //                dr.Cells.Add(col);

        //            }

        //            this.dataGridViewTablaAnalisis.Rows.Add(dr);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al crear la tabla de analisis");
        //    }
        //}

        private void button1_Click(object sender, EventArgs e)
        {
        //    try
        //    {                

        //        ComponenteLexico com = this.analizadorSintactico.AnalizadorLexico.ObtenerProximoToken();

        //        this.labelLexema.Text = com.Lexema;
        //        this.labelToken.Text = com.Token.ToString();
        //        this.labelFila.Text = com.Fila.ToString();
        //        this.labelColumna.Text = com.Columna.ToString();
        //        //analizadorLex.AnalizarLexicograficamente();

                
        //        this.dataGridView1.Rows.Add(com.Lexema, com.Token, com.AntecedidoPorSeparador.ToString(), com.Fila, com.Columna);
        //        this.dataGridView1.CurrentCell = this.dataGridView1[0, this.dataGridView1.Rows.Count - 1];

        //        if (com.Token == ComponenteLexico.TokenType.EOF)
        //        {
        //            this.button2.Enabled = false;
        //            this.button1.Enabled = false;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Utils.Log.AddError(ex.Message);
        //        MessageBox.Show(ex.Message);
        //    }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        //    ComponenteLexico com = this.analizadorSintactico.AnalizadorLexico.ObtenerProximoToken();


        //    while (com.Token != ComponenteLexico.TokenType.EOF)
        //    {
        //        this.dataGridView1.Rows.Add(com.Lexema, com.Token, com.AntecedidoPorSeparador.ToString(), com.Fila, com.Columna);

        //        com = this.analizadorSintactico.AnalizadorLexico.ObtenerProximoToken();
        //    }

        //    this.dataGridView1.Rows.Add(com.Lexema, com.Token, com.AntecedidoPorSeparador.ToString(), com.Fila, com.Columna);
        //    this.dataGridView1.CurrentCell = this.dataGridView1[0, this.dataGridView1.Rows.Count - 1];
            
        //    this.button2.Enabled = false;
        //    this.button1.Enabled = false;

        //    this.labelLexema.Text = string.Empty;
        //    this.labelToken.Text = string.Empty;
        //    this.labelFila.Text = string.Empty;
        //    this.labelColumna.Text = string.Empty;

            //com = this.analizadorLex.ObtenerProximoToken();
        }

        private void buttonResetear_Click(object sender, EventArgs e)
        {
            //this.dataGridView1.Rows.Clear();
            //this.analizadorSintactico.ResetearAnalizadorLexicografico();

            //this.button2.Enabled = true;
            //this.button1.Enabled = true;

            //this.labelLexema.Text = string.Empty;
            //this.labelToken.Text = string.Empty;
            //this.labelFila.Text = string.Empty;
            //this.labelColumna.Text = string.Empty;
        }

        private void buttonAnalizadorSintactico_Click(object sender, EventArgs e)
        {
            //bool error = this.analizadorSintactico.AnalizarSintacticamenteUnPaso();

            //if (ModoDebug)
            //{
            //    this.AgregarFilaSintactico(error);            
            //    this.dataGridViewSintactico.CurrentCell = this.dataGridViewSintactico[0, this.dataGridViewSintactico.Rows.Count - 1];
            //}

            //if (this.analizadorSintactico.esFinAnalisisSintactico() && !errores)
            //{
            //    this.buttonGenerarCodigo.Enabled = true;
            //}
        
        }

        private void buttonAnalizadorSintacticoTODO_Click(object sender, EventArgs e)
        {
            if (ModoDebug)
            {
                this.dataGridViewSintactico.Rows.Clear();
            }

            string programa = ObtenerTodoElTexto();

            ResultadoCompilacion res = this.compilador.Compilar(programa);


            foreach (var item in res.ListaDebugSintactico)
            {
                this.dataGridViewSintactico.Rows.Add(item.ContenidoPila, item.EstadoCadenaEntrada);

                switch (item.TipoError)
	            {
		            case GlobalesCompilador.TipoError.Sintactico:
                        this.dataGridViewSintactico.Rows[this.dataGridViewSintactico.Rows.Count - 1].DefaultCellStyle.BackColor = Color.OrangeRed;
                        break;
                    case GlobalesCompilador.TipoError.Semantico:
                        this.dataGridViewSintactico.Rows[this.dataGridViewSintactico.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
                        break;
                    case GlobalesCompilador.TipoError.Ninguno:
                        break;
                    default:
                        break;
	            }             

                this.dataGridViewSintactico.CurrentCell = this.dataGridViewSintactico[0, this.dataGridViewSintactico.Rows.Count - 1];
            }

            
            if (res.CompilacionGarGarCorrecta)
            {
                this.buttonGenerarCodigo.Enabled = false;
                this.textBoxCodigo.Text = res.CodigoPascal;
            }
            else
            {
                foreach (var item in res.ListaErrores)
                {

                    this.dataGridViewErrores.Rows.Add(item.Fila, item.Columna, item.TipoError.ToString(), item.Descripcion);


                    switch (item.TipoError)
                    {
                        case GlobalesCompilador.TipoError.Sintactico:
                            this.dataGridViewErrores.Rows[this.dataGridViewErrores.Rows.Count - 2].DefaultCellStyle.BackColor = Color.Red;
                            break;
                        case GlobalesCompilador.TipoError.Semantico:
                            this.dataGridViewErrores.Rows[this.dataGridViewErrores.Rows.Count - 2].DefaultCellStyle.BackColor = Color.OrangeRed;
                            break;
                        case GlobalesCompilador.TipoError.Ninguno:
                            break;

                    }
                }

                this.dataGridViewErrores.CurrentCell = this.dataGridViewErrores[0, this.dataGridViewErrores.Rows.Count - 1];
            }


            if (!string.IsNullOrEmpty(res.Error))
            {
                MessageBox.Show(res.Error);
            }

            MessageBox.Show("Finalizado");

        }

        private string ObtenerTodoElTexto()
        {
            string texto = string.Empty;
            try
            {
                StreamReader strReader = new StreamReader(Path.Combine(directorioActual, ArchivoEntrada));

                texto = strReader.ReadToEnd();

                strReader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("No se encontro el archivo de entrada. Error Fatal");
            }

            return texto;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //int i = 0;
            //while (i < 50 && !this.analizadorSintactico.esFinAnalisisSintactico())
            //{
            //    bool error = this.analizadorSintactico.AnalizarSintacticamenteUnPaso();

            //    if (ModoDebug)
            //    {
            //        this.AgregarFilaSintactico(error);
            //    }

            //    i++;
            //}

            //if (this.analizadorSintactico.esFinAnalisisSintactico() && !errores)
            //{
            //    this.buttonGenerarCodigo.Enabled = true;
            //}
        }

        //private void AgregarFilaSintactico(bool error)
        //{

        //    this.dataGridViewSintactico.Rows.Add(this.analizadorSintactico.Pila.ToString(), this.analizadorSintactico.CadenaEntrada.ToString());

        //    if (this.errorSemantico)
        //    {
        //        this.dataGridViewSintactico.Rows[this.dataGridViewSintactico.Rows.Count - 1].DefaultCellStyle.BackColor = Color.OrangeRed;
        //        this.errorSemantico = false;
        //    }

        //    if (error)
        //    {
        //        this.dataGridViewSintactico.Rows[this.dataGridViewSintactico.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
        //    }

        //    this.dataGridViewSintactico.CurrentCell = this.dataGridViewSintactico[0, this.dataGridViewSintactico.Rows.Count - 1];

        //}


    

        private void buttonGenerarCodigo_Click(object sender, EventArgs e)
        {
        //    textBoxCodigo.Text = string.Empty;

        //    this.analizadorSintactico.ArbolSemantico.CalcularExpresiones();

        //    textBoxCodigo.Text += this.analizadorSintactico.ArbolSemantico.CalcularCodigo();
        }

        private void buttonCompilarIde_Click(object sender, EventArgs e)
        {
            ReiniciarIDEParaCompilacion();           

            string programa = this.txtBxIDE.Text;
            ResultadoCompilacion res = this.compilador.Compilar(programa);

            MostrarResultadosCompilacion(res);

            if (!string.IsNullOrEmpty(res.Error))
            {
                MessageBox.Show(res.Error);
            }

            MessageBox.Show("Finalizado");
        }

        private void buttonEjecutar_Click(object sender, EventArgs e)
        {
            ReiniciarIDEParaCompilacion();

            string programa = this.txtBxIDE.Text;
            ResultadoCompilacion res = this.compilador.Compilar(programa);

            MostrarResultadosCompilacion(res);

            if (!string.IsNullOrEmpty(res.Error))
            {
                MessageBox.Show(res.Error);
            }

            if (res.CompilacionGarGarCorrecta && res.GeneracionEjectuableCorrecto)
            {
                EjecucionManager.EjecutarConVentana(res.ArchEjecutableConRuta);
            }
            
        }

        private void MostrarResultadosCompilacion(ResultadoCompilacion res)
        {
            if (ModoDebug)
            {
                AgregarListaDebug(res.ListaDebugSintactico);
            }

            if (res.CompilacionGarGarCorrecta)
            {
                this.buttonGenerarCodigo.Enabled = false;
                this.textBoxCodigo.Text = res.CodigoPascal;
            }
            else
            {
                AgregarErrores(res.ListaErrores);
            }
        }

        private void ReiniciarIDEParaCompilacion()
        {
            if (ModoDebug)
            {
                this.dataGridViewSintactico.Rows.Clear();
            }

            this.dataGridViewErroresIDE.Rows.Clear();
        }

        private void AgregarListaDebug(List<PasoCompilacion> list)
        {
 	        foreach (var item in list)
                    {
                        this.dataGridViewSintactico.Rows.Add(item.ContenidoPila, item.EstadoCadenaEntrada);

                        switch (item.TipoError)
                        {
                            case GlobalesCompilador.TipoError.Sintactico:
                                this.dataGridViewSintactico.Rows[this.dataGridViewSintactico.Rows.Count - 1].DefaultCellStyle.BackColor = Color.OrangeRed;
                                break;
                            case GlobalesCompilador.TipoError.Semantico:
                                this.dataGridViewSintactico.Rows[this.dataGridViewSintactico.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
                                break;
                            case GlobalesCompilador.TipoError.Ninguno:
                                break;
                            default:
                                break;
                        }

                        this.dataGridViewSintactico.CurrentCell = this.dataGridViewSintactico[0, this.dataGridViewSintactico.Rows.Count - 1];
                    }
        }

        private void AgregarErrores(List<PasoAnalizadorSintactico> list)
        {
            foreach (var item in list)
            {

                this.dataGridViewErroresIDE.Rows.Add(item.Fila, item.Columna, item.TipoError.ToString(), item.Descripcion);


                switch (item.TipoError)
                {
                    case GlobalesCompilador.TipoError.Sintactico:
                        this.dataGridViewErroresIDE.Rows[this.dataGridViewErroresIDE.Rows.Count - 2].DefaultCellStyle.BackColor = Color.Red;
                        break;
                    case GlobalesCompilador.TipoError.Semantico:
                        this.dataGridViewErroresIDE.Rows[this.dataGridViewErroresIDE.Rows.Count - 2].DefaultCellStyle.BackColor = Color.OrangeRed;
                        break;
                    case GlobalesCompilador.TipoError.Ninguno:
                        break;

                }
            }

            this.dataGridViewErroresIDE.CurrentCell = this.dataGridViewErroresIDE[0, this.dataGridViewErroresIDE.Rows.Count - 1];
        }

        


    }
}
