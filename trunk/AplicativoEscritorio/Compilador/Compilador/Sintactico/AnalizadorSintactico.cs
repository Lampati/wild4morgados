using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Sintactico.TablaGramatica;
using System.Configuration;
using CompiladorGargar.Lexicografico;
using System.Diagnostics;
using System.Windows.Forms;
using CompiladorGargar.Auxiliares;
using CompiladorGargar.Semantico;
using CompiladorGargar.Semantico.Arbol;


namespace CompiladorGargar.Sintactico
{
    class AnalizadorSintactico
    {
        public event CompiladorForm.ErrorCompiladorDelegate errorCompilacion;

        private Gramatica.Gramatica gramatica;
        public Gramatica.Gramatica Gramatica
        {
            get { return gramatica; }
        }

        private AnalizadorLexicografico analizadorLexico;
        public AnalizadorLexicografico AnalizadorLexico
        {
            get { return analizadorLexico; }
            set { analizadorLexico = value; }
        }

        private PilaGramatica pila;
        public PilaGramatica Pila
        {
            get { return pila; }
        }

        private CadenaEntrada cadenaEntrada;
        public CadenaEntrada CadenaEntrada
        {
            get { return cadenaEntrada; }
        }

        private TablaAnalisisGramatica tabla;
        public TablaAnalisisGramatica Tabla
        {
            get { return tabla; }
        }
        
        private int cantElementosCadenaEntrada;
        private bool finArch;


        private ArbolSemantico arbolSemantico;
        public ArbolSemantico ArbolSemantico
        {
            get { return arbolSemantico; }
        }

        private bool habilitarSemantico;
        public bool HabilitarSemantico
        {
            get { return habilitarSemantico; }
            set { habilitarSemantico = value; }
        }

        public string ArchEntrada { get; set; }

        public AnalizadorSintactico(string path)
        {
            gramatica = new Gramatica.Gramatica(path);

            tabla = this.gramatica.ArmarTablaAnalisis();

            //this.ArchEntrada = archEntrada;

            //this.CargarAnalizadorLexicografico();
            
            cantElementosCadenaEntrada = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["cantElementosCadenaEntrada"].ToString());
            finArch = false;            

           

            //this.RellenarCadenaEntrada();

        }

        public void ReiniciarAnalizadorSintactico()
        {
            this.pila = new PilaGramatica(this.gramatica.SimboloInicial);
            this.cadenaEntrada = new CadenaEntrada();

            this.arbolSemantico = new ArbolSemantico(this.gramatica.SimboloInicial);
            this.pila.ArbolSemantico = this.arbolSemantico;

            this.finArch = false;
        }


        private void CargarAnalizadorLexicografico()
        {
            analizadorLexico = new AnalizadorLexicograficoConArchEnMemoria(ArchEntrada);
        }

        private void CargarAnalizadorLexicografico(string texto)
        {
            this.analizadorLexico = new AnalizadorLexicograficoConArchEnMemoria(texto);
            this.RellenarCadenaEntrada();
        }

        internal void ResetearAnalizadorLexicografico()
        {
            this.CargarAnalizadorLexicografico();
        }

        public bool esFinAnalisisSintactico()
        {
            //return !(!this.cadenaEntrada.esFinDeCadena() && !this.pila.esFinDePila());
            return (this.cadenaEntrada.esFinDeCadena() && this.pila.esFinDePila());
        }      

        //public bool AnalizarSintacticamenteUnPaso()
        //{
        //    bool error = false;
        //    try
        //    {
        //        this.AnalizarUnSoloPaso();
        //    }
        //    catch (ErrorLexicoException ex)
        //    {
        //        this.MostrarError(new ErrorCompiladorEventArgs(ex.Tipo, ex.Descripcion, ex.Fila, ex.Columna, false));
        //    }
        //    catch (ErrorSintacticoException ex)
        //    {
        //        error = true;
        //        if (ex.descartarTopeCadena)
        //        {
        //            if (!this.CadenaEntrada.esFinDeCadena())
        //            {
        //                this.CadenaEntrada.EliminarPrimerTerminal();
        //            }
        //            try
        //            {
        //                this.RellenarCadenaEntrada();
        //            }
        //            catch (ErrorLexicoException exx)
        //            {
        //                this.MostrarError(new ErrorCompiladorEventArgs(exx.Tipo, exx.Descripcion, exx.Fila, exx.Columna, false));
        //            }
        //            this.MostrarError(new ErrorCompiladorEventArgs(ex.Tipo, ex.Descripcion, ex.Fila, ex.Columna, ex.pararAnalisis));
        //        }

        //        if (ex.descartarTopePila)
        //        {
        //            if (!this.Pila.esFinDePila())
        //            {
        //                this.Pila.DescartarTope();
        //            }
        //            this.MostrarError(new ErrorCompiladorEventArgs(ex.Tipo, ex.Descripcion, ex.Fila, ex.Columna, ex.pararAnalisis));
        //        }

               
        //    }
        //    return error;
        //}

        public List<PasoAnalizadorSintactico> AnalizarSintacticamenteUnPaso()
        {
            List<PasoAnalizadorSintactico> retorno = new List<PasoAnalizadorSintactico>();
            try
            {
                retorno = this.AnalizarUnSoloPaso();
            }
            catch (ErrorLexicoException ex)
            {

                retorno.Add(new PasoAnalizadorSintactico(ex.Descripcion, Global.TipoError.Sintactico, ex.Fila, ex.Columna, false));
                //this.MostrarError(new ErrorCompiladorEventArgs(ex.Tipo, ex.Descripcion, ex.Fila, ex.Columna, false));
            }
            catch (ErrorSintacticoException ex)
            {
                
                if (ex.descartarTopeCadena)
                {
                    if (!this.CadenaEntrada.esFinDeCadena())
                    {
                        this.CadenaEntrada.EliminarPrimerTerminal();
                    }
                    try
                    {
                        this.RellenarCadenaEntrada();
                    }
                    catch (ErrorLexicoException exx)
                    {
                        retorno.Add(new PasoAnalizadorSintactico(exx.Descripcion, Global.TipoError.Sintactico, exx.Fila, exx.Columna, false));
                        //this.MostrarError(new ErrorCompiladorEventArgs(exx.Tipo, exx.Descripcion, exx.Fila, exx.Columna, false));
                    }
                    retorno.Add(new PasoAnalizadorSintactico(ex.Descripcion, Global.TipoError.Sintactico, ex.Fila, ex.Columna, ex.pararAnalisis));
                    //this.MostrarError(new ErrorCompiladorEventArgs(ex.Tipo, ex.Descripcion, ex.Fila, ex.Columna, ex.pararAnalisis));
                }

                if (ex.descartarTopePila)
                {
                    if (!this.Pila.esFinDePila())
                    {
                        this.Pila.DescartarTope();
                    }
                    retorno.Add(new PasoAnalizadorSintactico(ex.Descripcion, Global.TipoError.Sintactico, ex.Fila, ex.Columna, ex.pararAnalisis));
                    //this.MostrarError(new ErrorCompiladorEventArgs(ex.Tipo, ex.Descripcion, ex.Fila, ex.Columna, ex.pararAnalisis));
                }


            }
            return retorno;
        }


        internal List<PasoAnalizadorSintactico> AnalizarUnSoloPaso()
        {
            List<PasoAnalizadorSintactico> retorno = new List<PasoAnalizadorSintactico>();

            if (!(this.cadenaEntrada.esFinDeCadena() && this.pila.esFinDePila()))
            {

                this.RellenarCadenaEntrada();

                if (this.pila.ObtenerTope().GetType() == typeof(Terminal))
                {
                    Terminal term = (Terminal)this.pila.ObtenerTope();

                    if (this.cadenaEntrada.ObtenerPrimerTerminal().Equals(this.pila.ObtenerTope()))
                    {
                        //Comentado hasta la parte semantica
                        //this.pilaAtributos.CalcularAtributos();
                        if (this.habilitarSemantico)
                        {
                            retorno = this.arbolSemantico.CalcularAtributos(this.cadenaEntrada.ObtenerPrimerTerminal());
                        }

                        this.pila.DescartarTope();

                        Global.UltFila = this.cadenaEntrada.ObtenerPrimerTerminal().Componente.Fila;
                        Global.UltCol = this.cadenaEntrada.ObtenerPrimerTerminal().Componente.Columna;

                        this.cadenaEntrada.EliminarPrimerTerminal();
                    }
                    else
                    {
                        if (term.NoEsLambda())
                        {
                            StringBuilder strbldr = new StringBuilder(string.Empty);
                            strbldr.Append("Se esperaba ");
                            strbldr.Append(EnumUtils.stringValueOf(term.Componente.Token));
                            strbldr.Append(" pero se encontro ");
                            strbldr.Append(EnumUtils.stringValueOf(this.cadenaEntrada.ObtenerPrimerTerminal().Componente.Token));
                            strbldr.Append(".");

                            if (term.Equals(Terminal.ElementoFinSentencia()))
                            {
                                strbldr.Append(" Se asume fin de sentencia para continuar con analisis.");
                                //Descarto el ; de la pila pq asumo que simplemente se le olvido
                                
                            }
              
                            throw new ErrorSintacticoException(strbldr.ToString(),
                                    this.AnalizadorLexico.FilaActual(),
                                    this.AnalizadorLexico.ColumnaActual(),
                                    true,
                                    false,
                                    false);
                        }
                        else
                        {
                            retorno= this.AnalizarPila();
                        }
                    }
                }
                else
                {
                    retorno= this.AnalizarPila();

                }
            }

            return retorno;
        }

        //public void AnalizarUnSoloPaso()
        //{
        //    if (this.cadenaEntrada.esFinDeCadena() && this.pila.esFinDePila())
        //    {
        //        MessageBox.Show("Analisis sintactico termino");
        //    }
        //    else
        //    {
        //        this.RellenarCadenaEntrada();                

        //        if (this.pila.ObtenerTope().GetType() == typeof(Terminal))
        //        {
        //            Terminal term = (Terminal)this.pila.ObtenerTope();

        //            if (this.cadenaEntrada.ObtenerPrimerTerminal().Equals(this.pila.ObtenerTope()))
        //            {
        //                //Comentado hasta la parte semantica
        //                //this.pilaAtributos.CalcularAtributos();
        //                if (this.habilitarSemantico)
        //                {
        //                    this.arbolSemantico.CalcularAtributos(this.cadenaEntrada.ObtenerPrimerTerminal());
        //                }

        //                this.pila.DescartarTope();

        //                Global.UltFila = this.cadenaEntrada.ObtenerPrimerTerminal().Componente.Fila;
        //                Global.UltCol = this.cadenaEntrada.ObtenerPrimerTerminal().Componente.Columna;
                        
        //                this.cadenaEntrada.EliminarPrimerTerminal();
        //            }
        //            else
        //            {
        //                if (term.NoEsLambda())
        //                {
        //                    StringBuilder strbldr = new StringBuilder(string.Empty);
        //                    strbldr.Append("Se esperaba el token ");
        //                    strbldr.Append(EnumUtils.stringValueOf(term.Componente.Token));
        //                    strbldr.Append(" pero se encontro el token ");
        //                    strbldr.Append(EnumUtils.stringValueOf(this.cadenaEntrada.ObtenerPrimerTerminal().Componente.Token));
        //                    strbldr.Append(".");

        //                    if (term.Equals(Terminal.ElementoFinSentencia()))
        //                    {
        //                        strbldr.Append(" Se asume fin de sentencia para continuar con analisis.");
        //                        //Descarto el ; de la pila pq asumo que simplemente se le olvido
        //                        throw new ErrorSintacticoException(strbldr.ToString(),
        //                            this.AnalizadorLexico.FilaActual(),
        //                            this.AnalizadorLexico.ColumnaActual(),
        //                            true,
        //                            false,
        //                            false);
        //                    }
        //                    else
        //                    {
                                
        //                        throw new ErrorSintacticoException(strbldr.ToString(),
        //                            this.AnalizadorLexico.FilaActual(),
        //                            this.AnalizadorLexico.ColumnaActual(),
        //                            true,
        //                            false,
        //                            false);
        //                    }
        //                }
        //                else
        //                {
        //                    this.AnalizarPila();
        //                }
        //            }
        //        }
        //        else
        //        {
                    
        //            this.AnalizarPila();
                    
 
        //        }
        //    }
        //}

        //private void AnalizarPila()
        //{
        //    if (this.pila.ObtenerTope().GetType() == typeof(NoTerminal))
        //    {
        //        Terminal t = this.cadenaEntrada.ObtenerPrimerTerminal();
        //        NoTerminal nt = (NoTerminal)this.pila.ObtenerTope();

        //        bool generaProdVacia = false;

        //        //Que es esto??
        //        if (!this.PerteneceNoTerminalesNoEscapeables(nt))
        //        {
        //            generaProdVacia = this.gramatica.NoTerminalGeneraProduccionVacia(nt);
        //        }

        //        Produccion prod = this.tabla.BuscarEnTablaProduccion(nt, t, true, generaProdVacia);
                
        //        this.pila.TransformarProduccion(prod);
        //    }
        //    else
        //    {
        //        if (!((Terminal)this.pila.ObtenerTope()).NoEsLambda())
        //        {
        //            //Comentado hasta la parte semantica
        //            //this.pilaAtributos.CalcularAtributos();
                    
        //            Terminal t = Terminal.ElementoVacio();
        //            t.Componente.Fila = this.AnalizadorLexico.FilaActual();
        //            t.Componente.Columna = this.AnalizadorLexico.ColumnaActual();

        //            if (habilitarSemantico)
        //            {
        //                this.arbolSemantico.CalcularAtributos(t);
        //            }
        //            this.pila.DescartarTope();
        //        }
        //    }
        //}

        private List<PasoAnalizadorSintactico> AnalizarPila()
        {
            List<PasoAnalizadorSintactico> retorno = new List<PasoAnalizadorSintactico>();

            if (this.pila.ObtenerTope().GetType() == typeof(NoTerminal))
            {
                long timeStamp2 = Stopwatch.GetTimestamp();
                Terminal t = this.cadenaEntrada.ObtenerPrimerTerminal();
                NoTerminal nt = (NoTerminal)this.pila.ObtenerTope();

                bool generaProdVacia = false;

                //Que es esto??
                if (!this.PerteneceNoTerminalesNoEscapeables(nt))
                {
                    generaProdVacia = this.gramatica.NoTerminalGeneraProduccionVacia(nt);
                }

                Produccion prod = this.tabla.BuscarEnTablaProduccion(nt, t, true, generaProdVacia);

                this.pila.TransformarProduccion(prod);
            }
            else
            {
                if (!((Terminal)this.pila.ObtenerTope()).NoEsLambda())
                {
                    //Comentado hasta la parte semantica
                    //this.pilaAtributos.CalcularAtributos();

                    Terminal t = Terminal.ElementoVacio();
                    t.Componente.Fila = this.AnalizadorLexico.FilaActual();
                    t.Componente.Columna = this.AnalizadorLexico.ColumnaActual();

                    if (habilitarSemantico)
                    {
                        retorno = this.arbolSemantico.CalcularAtributos(t);
                    }

                   
                    this.pila.DescartarTope();

                }
            }

            return retorno;
        }

        private bool PerteneceNoTerminalesNoEscapeables(NoTerminal nt)
        {
            return (nt.Nombre.Equals("EXPR") || nt.Nombre.Equals("BLQ") || nt.Nombre.Equals("PROCEDIMIENTO") || nt.Nombre.Equals("PROCED"));

        }

        
        public void MostrarError(ErrorCompiladorEventArgs e)
        {
            if (errorCompilacion != null)
            {
                errorCompilacion(e.Tipo,e.Descripcion,e.Fila,e.Columna,e.PararAnalisis);
            }            
        }

        private void RellenarCadenaEntrada()
        {
            if (!finArch)
            {
                Terminal t = Terminal.ElementoVacio();

                while ((this.cadenaEntrada.Count < cantElementosCadenaEntrada) && (!t.Equals(Terminal.ElementoEOF())))
                {
                    
                    t = new Terminal();
                    t.Componente = this.AnalizadorLexico.ObtenerProximoToken();

                    //Controlo que no haya un error lexico en el token que acabo de traer.
                    //Si hay error directamente me salteo el paso, no se inserta en la cadena, y no toco la pila.
                    if (t.Equals(Terminal.ElementoError()))
                    {
                        throw new ErrorLexicoException(string.Format("Error al intentar leer: {0}", t.Componente.Descripcion), t.Componente.Fila, t.Componente.Columna);
                    }

                    this.cadenaEntrada.InsertarTerminal(t);                       
                    
                }
                if (t.Equals(Terminal.ElementoEOF()))
                {
                    finArch = true;
                }
            }
        }




        internal void CargarAnalizadorLexico(string texto)
        {
            this.CargarAnalizadorLexicografico(texto);
            
        }
    }
}
