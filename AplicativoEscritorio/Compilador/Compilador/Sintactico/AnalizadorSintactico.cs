using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Sintactico.TablaGramatica;
using System.Configuration;
using Compilador.Lexicografico;
using System.Diagnostics;
using System.Windows.Forms;
using Compilador.Auxiliares;
using Compilador.Semantico;
using Compilador.Semantico.Arbol;


namespace Compilador.Sintactico
{
    class AnalizadorSintactico
    {
        public event Compilador.ErrorCompiladorDelegate errorCompilacion;

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

        public AnalizadorSintactico(string path)
        {
            gramatica = new Gramatica.Gramatica(path);

            tabla = this.gramatica.ArmarTablaAnalisis();

            

            this.CargarAnalizadorLexicografico();
            
            cantElementosCadenaEntrada = Convert.ToInt32(ConfigurationSettings.AppSettings["cantElementosCadenaEntrada"].ToString());
            finArch = false;

            this.pila = new PilaGramatica(this.gramatica.SimboloInicial);
            this.cadenaEntrada = new CadenaEntrada();

            this.arbolSemantico = new ArbolSemantico(this.gramatica.SimboloInicial);
            this.pila.ArbolSemantico = this.arbolSemantico;

            this.RellenarCadenaEntrada();

        }

        
        private void CargarAnalizadorLexicografico()
        {
            string pathArchEntrada = ConfigurationSettings.AppSettings["archEntrada"].ToString();

            analizadorLexico = new AnalizadorLexicografico(pathArchEntrada);
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

        public bool AnalizarSintacticamenteUnPaso()
        {
            bool error = false;
            try
            {
                this.AnalizarUnSoloPaso();
            }
            catch (ErrorLexicoException ex)
            {
                this.MostrarError(new ErrorCompiladorEventArgs(ex.Tipo, ex.Descripcion, ex.Fila, ex.Columna, false));
            }
            catch (ErrorSintacticoException ex)
            {
                error = true;
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
                        this.MostrarError(new ErrorCompiladorEventArgs(exx.Tipo, exx.Descripcion, exx.Fila, exx.Columna, false));
                    }
                    this.MostrarError(new ErrorCompiladorEventArgs(ex.Tipo, ex.Descripcion, ex.Fila, ex.Columna, ex.pararAnalisis));
                }

                if (ex.descartarTopePila)
                {
                    if (!this.Pila.esFinDePila())
                    {
                        this.Pila.DescartarTope();
                    }
                    this.MostrarError(new ErrorCompiladorEventArgs(ex.Tipo, ex.Descripcion, ex.Fila, ex.Columna, ex.pararAnalisis));
                }

                #region Manejo viejo de errores
                /*
                if (ex.descartarTopeCadena)
                {
                    StringBuilder strBldr = new StringBuilder(ex.Descripcion);
                    strBldr.Append(" Se descartaron los siguientes tokens para seguir con el analisis: ");

                    while (!this.CadenaEntrada.esFinDeCadena() && !this.CadenaEntrada.ObtenerPrimerTerminal().Equals(Terminal.ElementoFinSentencia()))
                    {
                        strBldr.Append(this.CadenaEntrada.ObtenerPrimerTerminal().ToString());
                        strBldr.Append(" ");
                        this.CadenaEntrada.EliminarPrimerTerminal();

                        try
                        {
                            this.RellenarCadenaEntrada();
                        }
                        catch (ErrorLexicoException exx)
                        {
                            this.MostrarError(new ErrorCompiladorEventArgs(exx.Tipo, exx.Descripcion, exx.Fila, exx.Columna, false));
                        }
                    }
                    strBldr.Append(this.CadenaEntrada.ObtenerPrimerTerminal().ToString());

                    if (!this.CadenaEntrada.esFinDeCadena())
                    {
                        this.CadenaEntrada.EliminarPrimerTerminal();
                    }

                    //Siempre descarto el tope pq me puede haber quedado mal de antes
                    if (this.Pila.ObtenerTope().GetType() == typeof(NoTerminal))
                    {
                        this.Pila.DescartarTope();
                    }

                    bool produccionesDummyEncontradas = false;

                    while (!this.Pila.esFinDePila() && !this.Pila.ObtenerTope().Equals(Terminal.ElementoFinSentencia()))
                    {
                        if (this.Pila.ObtenerTope().GetType() == typeof(Terminal))
                        {
                            this.Pila.DescartarTope();
                        }
                        else
                        {
                       
                            //if (ultimoNoTerminalErroneo == (NoTerminal)this.Pila.ObtenerTope())
                            //{
                            //    ultimoNoTerminalErroneo = null;
                            //    this.Pila.DescartarTope();
                            //}
                            //else                       
                            //{
                                if (!produccionesDummyEncontradas)
                                {
                                    ultimoNoTerminalErroneo = (NoTerminal)this.Pila.ObtenerTope();

                                    if (this.gramatica.ContieneElementoFinSentencia((NoTerminal)this.pila.ObtenerTope()))
                                    {
                                        List<Produccion> produccionesDummy = this.gramatica.ObtenerProduccionesParaSalvarError(
                                                                            (NoTerminal)this.pila.ObtenerTope());

                                        foreach (Produccion p in produccionesDummy)
                                        {
                                            this.pila.TransformarProduccion(p);
                                        }

                                        produccionesDummyEncontradas = true;
                                    }
                                    else
                                    {
                                        this.Pila.DescartarTope();
                                    }
                                }
                                else
                                {
                                    this.Pila.DescartarTope();
                                }
                            //}

                            //Produccion prod = this.tabla.BuscarEnTablaProduccion(nt, t, false);
                            //this.Pila.TransformarProduccion(null);
                        }
                    }
                    if (!this.Pila.esFinDePila())
                    {
                        this.Pila.DescartarTope();
                    }

                    this.MostrarError(new ErrorCompiladorEventArgs(ex.Tipo, strBldr.ToString(), ex.Fila, ex.Columna, ex.pararAnalisis));
                }
                if (ex.descartarTopePila)
                {
                    this.MostrarError(new ErrorCompiladorEventArgs(ex.Tipo, ex.Descripcion, ex.Fila, ex.Columna, ex.pararAnalisis));
                    this.pila.DescartarTope();
                }
                 */
                #endregion
            }
            return error;
        }

        public void AnalizarUnSoloPaso()
        {
            if (this.cadenaEntrada.esFinDeCadena() && this.pila.esFinDePila())
            {
                MessageBox.Show("Analisis sintactico termino");
            }
            else
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
                            this.arbolSemantico.CalcularAtributos(this.cadenaEntrada.ObtenerPrimerTerminal());
                        }

                        this.pila.DescartarTope();
                        
                        this.cadenaEntrada.EliminarPrimerTerminal();
                    }
                    else
                    {
                        if (term.NoEsLambda())
                        {
                            StringBuilder strbldr = new StringBuilder(string.Empty);
                            strbldr.Append("Se esperaba el token ");
                            strbldr.Append(EnumUtils.stringValueOf(term.Componente.Token));
                            strbldr.Append(" pero se encontro el token ");
                            strbldr.Append(EnumUtils.stringValueOf(this.cadenaEntrada.ObtenerPrimerTerminal().Componente.Token));
                            strbldr.Append(".");

                            if (term.Equals(Terminal.ElementoFinSentencia()))
                            {
                                strbldr.Append(" Se asume fin de sentencia para continuar con analisis.");
                                //Descarto el ; de la pila pq asumo que simplemente se le olvido
                                throw new ErrorSintacticoException(strbldr.ToString(),
                                    this.AnalizadorLexico.FilaActual(),
                                    this.AnalizadorLexico.ColumnaActual(),
                                    true,
                                    false,
                                    false);
                            }
                            else
                            {
                                
                                throw new ErrorSintacticoException(strbldr.ToString(),
                                    this.AnalizadorLexico.FilaActual(),
                                    this.AnalizadorLexico.ColumnaActual(),
                                    true,
                                    false,
                                    false);
                            }
                        }
                        else
                        {
                            this.AnalizarPila();
                        }
                    }
                }
                else
                {
                    
                    this.AnalizarPila();
                    
 
                }
            }
        }

        private void AnalizarPila()
        {
            if (this.pila.ObtenerTope().GetType() == typeof(NoTerminal))
            {
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
                        this.arbolSemantico.CalcularAtributos(t);
                    }
                    this.pila.DescartarTope();
                }
            }
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
                        throw new ErrorLexicoException(t.Componente.Descripcion, t.Componente.Fila, t.Componente.Columna);
                    }

                    this.cadenaEntrada.InsertarTerminal(t);                       
                    
                }
                if (t.Equals(Terminal.ElementoEOF()))
                {
                    finArch = true;
                }
            }
        }

    }
}
