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
using CompiladorGargar.Resultado.Auxiliares;


namespace CompiladorGargar.Sintactico
{
    class AnalizadorSintactico
    {
        
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

      

        private short cantErroresSintacticos;
        private short cantParentesisAbiertos;

        public string ArchEntrada { get; set; }

        public AnalizadorSintactico(string path)
        {
            gramatica = new Gramatica.Gramatica(path);

            tabla = this.gramatica.ArmarTablaAnalisis();

            //this.ArchEntrada = archEntrada;

            //this.CargarAnalizadorLexicografico();

            cantElementosCadenaEntrada = Convert.ToInt32(CompiladorGargar.Properties.Resources.CantElementosCadenaEntrada);
            finArch = false;

            cantErroresSintacticos = 0;
            cantParentesisAbiertos = 0;
           

            //this.RellenarCadenaEntrada();

        }

        public void ReiniciarAnalizadorSintactico()
        {
            EstadoSintactico.Reiniciar();

            this.pila = new PilaGramatica(this.gramatica.SimboloInicial);
            this.cadenaEntrada = new CadenaEntrada();

            this.arbolSemantico = new ArbolSemantico(this.gramatica.SimboloInicial);
            this.pila.ArbolSemantico = this.arbolSemantico;

            this.finArch = false;
            cantErroresSintacticos = 0;
            cantParentesisAbiertos = 0;

            habilitarSemantico = true;
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
            return (this.cadenaEntrada.EsFinDeCadena() && this.pila.esFinDePila());
        }      

        public List<PasoAnalizadorSintactico> AnalizarSintacticamenteUnPaso()
        {
            List<PasoAnalizadorSintactico> retorno = new List<PasoAnalizadorSintactico>();
            try
            {
                retorno = this.AnalizarUnSoloPaso();
            }
            catch (ErrorLexicoException ex)
            {
                cantErroresSintacticos++;
                retorno.Add(new PasoAnalizadorSintactico(ex.Descripcion, GlobalesCompilador.TipoError.Sintactico, ex.Fila, ex.Columna, false));
                //this.MostrarError(new ErrorCompiladorEventArgs(ex.Tipo, ex.Descripcion, ex.Fila, ex.Columna, false));
            }
            //catch (ErrorLexicoException ex)
            //{
              
            //    string mensajeAMostrar = ex.Descripcion;
            //    int filaAMostrar = ex.Fila;
            //    int colAMostrar = ex.Columna;

            //    try
            //    {
            //        ErroresManager.AnalizadorErroresSintacticos analizador = new ErroresManager.AnalizadorErroresSintacticos(
            //                                                                       EstadoSintactico.ListaLineaActual,
            //                                                                       EstadoSintactico.ContextoGlobal,
            //                                                                       EstadoSintactico.ContextoLinea,
            //                                                                       this.CadenaEntrada.CadenaEntera);
            //        try
            //        {
            //            analizador.Validar();
            //        }
            //        catch (CompiladorGargar.Sintactico.ErroresManager.ValidacionException excepVal)
            //        {
            //            mensajeAMostrar = excepVal.Message;
            //            filaAMostrar = excepVal.Fila != -1 ? excepVal.Fila : filaAMostrar;
            //            colAMostrar = excepVal.Columna != -1 ? excepVal.Columna : colAMostrar;
            //            this.habilitarSemantico = false;

            //            retorno.Add(new PasoAnalizadorSintactico(mensajeAMostrar, GlobalesCompilador.TipoError.Sintactico, filaAMostrar, colAMostrar, true, excepVal.MensjError));
            //        }
            //    }
            //    catch (ErroresManager.AnalizadorErroresException exAnaliz)
            //    {
            //        mensajeAMostrar = exAnaliz.Message;
            //        filaAMostrar = exAnaliz.Fila != -1 ? exAnaliz.Fila : filaAMostrar;
            //        colAMostrar = exAnaliz.Columna != -1 ? exAnaliz.Columna : colAMostrar;

            //        CompiladorGargar.Sintactico.ErroresManager.Errores.MensajeError mensErr = new CompiladorGargar.Sintactico.ErroresManager.Errores.ErrorVacio();
            //        mensErr.MensajeModoTexto = exAnaliz.Message;
            //        mensErr.MensajeModoGrafico = exAnaliz.MensajeModoGrafico;
            //        mensErr.EsErrorBienDefinido = false;

            //        retorno.Add(new PasoAnalizadorSintactico(mensajeAMostrar, GlobalesCompilador.TipoError.Sintactico, filaAMostrar, colAMostrar, true, mensErr)); //siempre paro la compilacion al primer error
            //    }

          
            //    if (cantErroresSintacticos >= GlobalesCompilador.CANT_MAX_ERRORES_SINTACTICOS)
            //    {
            //        retorno.Add(new PasoAnalizadorSintactico("Se paró la compilacion por la cantidad de errores.", GlobalesCompilador.TipoError.Sintactico, 0, 0, true));
            //    }
            //}
            catch (ErrorSintacticoException ex)
            {
                if (ex.Mostrar)
                {
                    cantErroresSintacticos++;
                }

                string mensajeAMostrar = ex.Descripcion;
                int filaAMostrar = ex.Fila;
                int colAMostrar = ex.Columna;

                bool pararCompilacion = ex.PararAnalisis;

                try
                {
                    ErroresManager.AnalizadorErroresSintacticos analizador = new ErroresManager.AnalizadorErroresSintacticos(
                                                                                   EstadoSintactico.ListaLineaActual,
                                                                                   EstadoSintactico.ContextoGlobal,
                                                                                   EstadoSintactico.ContextoLinea,
                                                                                   this.CadenaEntrada.CadenaEntera);

                    try
                    {
                        analizador.Validar();
                    }
                    catch (CompiladorGargar.Sintactico.ErroresManager.ValidacionException excepVal)
                    {
                        mensajeAMostrar = excepVal.Message;
                        filaAMostrar = excepVal.Fila != -1 ? excepVal.Fila : filaAMostrar;
                        colAMostrar = excepVal.Columna != -1 ? excepVal.Columna : colAMostrar;
                        this.habilitarSemantico = false;
                        pararCompilacion = true; //siempre paro la compilacion al primer error

                        retorno.Add(new PasoAnalizadorSintactico(mensajeAMostrar, GlobalesCompilador.TipoError.Sintactico, filaAMostrar, colAMostrar, pararCompilacion, excepVal.MensjError));
                    }
                }
                catch (ErroresManager.AnalizadorErroresException exAnaliz)
                {
                    mensajeAMostrar = exAnaliz.Message;
                    filaAMostrar = exAnaliz.Fila != -1 ? exAnaliz.Fila : filaAMostrar;
                    colAMostrar = exAnaliz.Columna != -1 ? exAnaliz.Columna : colAMostrar;
                    pararCompilacion = exAnaliz.Parar;

                    CompiladorGargar.Sintactico.ErroresManager.Errores.MensajeError mensErr = new CompiladorGargar.Sintactico.ErroresManager.Errores.ErrorVacio();
                    mensErr.MensajeModoTexto = exAnaliz.Message;
                    mensErr.MensajeModoGrafico = exAnaliz.MensajeModoGrafico;
                    mensErr.EsErrorBienDefinido = false;

                    retorno.Add(new PasoAnalizadorSintactico(mensajeAMostrar, GlobalesCompilador.TipoError.Sintactico, filaAMostrar, colAMostrar, pararCompilacion, mensErr)); //siempre paro la compilacion al primer error
                }

                #region Parte Vieja maneja excepciones sintacticas

                //if (ex.DescartarTopeCadena)
                //{
                //    if (!this.CadenaEntrada.EsFinDeCadena())
                //    {
                //        this.CadenaEntrada.EliminarPrimerTerminal();
                //    }
                //    try
                //    {
                //        this.RellenarCadenaEntrada();
                //    }
                //    catch (ErrorLexicoException exx)
                //    {
                //        retorno.Add(new PasoAnalizadorSintactico(exx.Descripcion, GlobalesCompilador.TipoError.Sintactico, exx.Fila, exx.Columna, false));
                //        //this.MostrarError(new ErrorCompiladorEventArgs(exx.Tipo, exx.Descripcion, exx.Fila, exx.Columna, false));
                //    }
                //    if (ex.Mostrar)
                //    {
                //        retorno.Add(new PasoAnalizadorSintactico(mensajeAMostrar, GlobalesCompilador.TipoError.Sintactico, filaAMostrar, colAMostrar, pararCompilacion));
                //    }
                //    //this.MostrarError(new ErrorCompiladorEventArgs(ex.Tipo, ex.Descripcion, ex.Fila, ex.Columna, ex.pararAnalisis));
                //}

                //if (ex.DescartarTopePila)
                //{
                //    if (ex.TerminalHastaDondeDescartarPila != null)
                //    {
                //        bool parar = false;
                //        while (!this.Pila.esFinDePila() && !parar)
                //        {
                //            if (this.Pila.ObtenerTope().GetType() != typeof(Terminal))
                //            {
                //                this.Pila.DescartarTope();
                //            }
                //            else if (!(((Terminal)this.Pila.ObtenerTope()).Equals(Terminal.ElementoFinSentencia())))
                //            {
                //                this.Pila.DescartarTope();
                //            }
                //            else
                //            {
                //                this.Pila.DescartarTope();
                //                parar = true;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        if (!this.Pila.esFinDePila())
                //        {
                //            this.Pila.DescartarTope();
                //        }
                //    }

                //    if (ex.Mostrar)
                //    {
                //        retorno.Add(new PasoAnalizadorSintactico(mensajeAMostrar, GlobalesCompilador.TipoError.Sintactico, filaAMostrar, colAMostrar, pararCompilacion));
                //    }
                //    //this.MostrarError(new ErrorCompiladorEventArgs(ex.Tipo, ex.Descripcion, ex.Fila, ex.Columna, ex.pararAnalisis));
                //}

                #endregion

                if (cantErroresSintacticos >= GlobalesCompilador.CANT_MAX_ERRORES_SINTACTICOS)
                {
                    retorno.Add(new PasoAnalizadorSintactico("Se paró la compilacion por la cantidad de errores.", GlobalesCompilador.TipoError.Sintactico, 0, 0, true));
                }
            }
            return retorno;
        }


        internal List<PasoAnalizadorSintactico> AnalizarUnSoloPaso()
        {
            List<PasoAnalizadorSintactico> retorno = new List<PasoAnalizadorSintactico>();

            if (!(this.cadenaEntrada.EsFinDeCadena() && this.pila.esFinDePila()))
            {

                this.RellenarCadenaEntrada();

                if (this.pila.ObtenerTope().GetType() == typeof(Terminal))
                {
                    Terminal term = (Terminal)this.pila.ObtenerTope();

                    if (this.cadenaEntrada.ObtenerPrimerTerminal().Equals(this.pila.ObtenerTope()))
                    {                       

                        if (term.Componente.Token == ComponenteLexico.TokenType.ParentesisApertura)
                        {
                            cantParentesisAbiertos++;
                        }
                        else if (term.Componente.Token == ComponenteLexico.TokenType.ParentesisClausura)
                        {
                            cantParentesisAbiertos--;
                        }

                        //flanzani 8/1/2012
                        //tokens repetidos
                        //Antes de pasar por el semantico, lo que hago es fijarme si el terminal justo no esta repetido, 
                        //pq eso me caga todo el parseo de errores del sintactico
                        //Esto puede arrojar una excepcion sintactica
                        ChequearTokensRepetidosEnCadena(term);
                        
                        if (this.habilitarSemantico)
                        {
                            retorno = this.arbolSemantico.CalcularAtributos(this.cadenaEntrada.ObtenerPrimerTerminal());
                        }

                        this.pila.DescartarTope();

                        GlobalesCompilador.UltFila = this.cadenaEntrada.ObtenerPrimerTerminal().Componente.Fila;
                        GlobalesCompilador.UltCol = this.cadenaEntrada.ObtenerPrimerTerminal().Componente.Columna;

                        EstadoSintactico.AgregarTerminal(term);

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
                                //Descarto el ; de la pila pq asumo que simplemente se le olvido
                                strbldr.Append(" Se asume fin de sentencia para continuar con analisis.");
                                
                                
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

        private void ChequearTokensRepetidosEnCadena(Terminal term)
        {
           if (this.cadenaEntrada.TieneTerminalRepetidoEnPrimerLugarErroneo( this.cantParentesisAbiertos))
            {

                if (term.Equals(Terminal.ElementoParentesisClausura()))
                {
                    //Le sumo uno asi restauro el equilibrio
                    cantParentesisAbiertos++;
                }
                StringBuilder strbldr = new StringBuilder("Se encontro ");
                strbldr.Append(EnumUtils.stringValueOf(term.Componente.Token));
                strbldr.Append(" repetido.");

                throw new ErrorSintacticoException(strbldr.ToString(),
                        this.AnalizadorLexico.FilaActual(),
                        this.AnalizadorLexico.ColumnaActual(),
                        false,
                        true,
                        false);
            }
        }


        private List<PasoAnalizadorSintactico> AnalizarPila()
        {
            List<PasoAnalizadorSintactico> retorno = new List<PasoAnalizadorSintactico>();

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

                //Buscar en la tabla arroja excepciones sintacticas si encuentra errores.
                Produccion prod = this.tabla.BuscarEnTablaProduccion(nt, t, true, generaProdVacia);

                if (prod != null)
                {

                    // flanzani 8/1/2012
                    // Esto es para ver que no se este operando la pila para llegar a un error sintactico, descartando cosas
                    // Si encuentra un problema, devuelve true, y se crea un error sintactico para descartar el tope de la cadena.
                    bool dejarDeOperarPilaYTirarError = ChequearQueNoSeEsteOperandoLaPilaParaUnErrorSintactico(prod);


                    if (dejarDeOperarPilaYTirarError) 
                    {
                        // flanzani 8/1/2012
                        // Este metodo se fija si el estado de la pila es pq falta un token solo, y se fija basandose en que es 
                        // el tope de la pila, para ver que terminal tengo que buscar y decir que falta ese para descartar ese
                        AnalizarLugarDeLaPilaYDescartarHastaTerminalQueCorresponda(nt, t, generaProdVacia);

                        
                    }

                    this.pila.TransformarProduccion(prod);
                }
               

            }
            else
            {
                if (!((Terminal)this.pila.ObtenerTope()).NoEsLambda())
                {                    
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

        private void AnalizarLugarDeLaPilaYDescartarHastaTerminalQueCorresponda(NoTerminal nt, Terminal t, bool generaProdVacia)
        {
            Terminal termBuscar = BuscarTerminalApropiadoBasadoEnTopePila(nt);

            //string error = CaptarMensajeErrorApropiado(nt, t);

            Produccion prod = this.tabla.BuscarEnTablaProduccion(nt, termBuscar, false, generaProdVacia);

            if (prod != null)
            {
                StringBuilder strbldr = new StringBuilder(string.Empty);
                strbldr.Append("Se esperaba ");
                strbldr.Append(termBuscar.Componente.Lexema);

                throw new ErrorSintacticoException(strbldr.ToString(),
                        this.AnalizadorLexico.FilaActual(),
                        this.AnalizadorLexico.ColumnaActual(),
                        true,
                        false,
                        true,
                        true,
                        termBuscar
                        );

            }
            else
            {

                StringBuilder strbldr = new StringBuilder(string.Empty);
                strbldr.Append(EnumUtils.stringValueOf(t.Componente.Token));
                strbldr.Append(" no tiene lugar en la sentencia.");

                throw new ErrorSintacticoException(strbldr.ToString(),
                        this.AnalizadorLexico.FilaActual(),
                        this.AnalizadorLexico.ColumnaActual(),
                        false,
                        true,
                        false);
            }
        }

        private string CaptarMensajeErrorApropiado(NoTerminal nt, Terminal t)
        {
            string x;

            return string.Empty;
        }

        private Terminal BuscarTerminalApropiadoBasadoEnTopePila(NoTerminal nt)
        {
            Terminal retorno;
            switch (nt.Nombre)
            {
                case "MULT":
                case "MULTS":
                case "EXP":
                case "EXPR":
                    if (this.cantParentesisAbiertos > 0)
                    {
                        retorno = Terminal.ElementoParentesisClausura();
                    }
                    else
                    {
                        retorno = Terminal.ElementoFinSentencia();
                    }
                    break;

                case "IDASIGN":
                    retorno = Terminal.ElementoFinSentencia();
                    break;

                case "EXPRPRPROC":
                case "EXPRPRPROCED":
                case "EXPRBOOLEANAS":
                case "EXPRBOOLEXTRA":
                   
                    retorno = Terminal.ElementoParentesisClausura();                   
                    break;
                default:
                    retorno = Terminal.ElementoFinSentencia();
                    break;
            }

            return retorno;
        }

        private bool ChequearQueNoSeEsteOperandoLaPilaParaUnErrorSintactico(Produccion prod)
        {
            bool retorno =false;
            bool pararChequeo = false;
            if (prod.ProduceElementoVacio())
            {
                int posPila = 1;
                while (!(this.cadenaEntrada.EsFinDeCadena() && this.pila.esFinDePila()) && (this.pila.Count > posPila) && !pararChequeo)
                {
                    
                    if (this.pila.ObtenerPosicion(posPila).GetType() == typeof(Terminal))
                    {
                        Terminal term = (Terminal)this.pila.ObtenerPosicion(posPila);

                        if (this.cadenaEntrada.ObtenerPrimerTerminal().Equals(this.pila.ObtenerPosicion(posPila)))
                        {
                            //No hay error pq coincide el terminal, y se va a poder descartar en el proximo paso.
                            retorno = false;
                            pararChequeo = true;
                        }
                        else
                        {
                            if (term.NoEsLambda())
                            {
                                //Hay error pq el terminal no coindiria con el de la cadena de entrada.
                                retorno = true;
                                pararChequeo = true;
                            }
                            
                        }
                    }
                    else
                    {
                        Terminal t = this.cadenaEntrada.ObtenerPrimerTerminal();
                        NoTerminal nt = (NoTerminal)this.pila.ObtenerPosicion(posPila);

                        bool generaProdVacia = false;

                        //Que es esto??
                        if (!this.PerteneceNoTerminalesNoEscapeables(nt))
                        {
                            generaProdVacia = this.gramatica.NoTerminalGeneraProduccionVacia(nt);
                        }

                        Produccion prodAux = this.tabla.BuscarEnTablaProduccion(nt, t, false, generaProdVacia);

                        if (prodAux != null)
                        {
                            if (prodAux.ProduceElementoVacio())
                            {
                                posPila++;
                            }
                            else
                            {
                                //Significa que llegue a algo concreto con el terminal que tengo en el tope, y dejo seguir.
                                retorno = false;
                                pararChequeo = true;
                            }
                        }
                        else
                        {
                            //Significa que en la tabla ni figura, o sea que es un error
                            retorno = true;
                            pararChequeo = true;
                        }
                        
                    }
                }

                if (posPila > this.pila.Count)
                {
                    //Hubo error pq el terminal tope no servia para nada de la pila
                    retorno = true;
                }
            }

            return retorno;
        }

        private bool PerteneceNoTerminalesNoEscapeables(NoTerminal nt)
        {
            return (nt.Nombre.Equals("EXPR") || nt.Nombre.Equals("BLQ") || nt.Nombre.Equals("PROCEDIMIENTO") || nt.Nombre.Equals("PROCED"));

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
                        //throw new ErrorLexicoException(string.Format("El caracter {0} no es reconocido por el lenguaje GarGar", t.Componente.Lexema), t.Componente.Fila, t.Componente.Columna);
                        string lexemaError;
                        if (string.IsNullOrEmpty(t.Componente.CaracterErroneo))
                        {
                            lexemaError = t.Componente.Lexema.Split(new char[] { ' ' })[0];
                        }
                        else
                        {
                            lexemaError = t.Componente.CaracterErroneo;
                        }

                        string errorMensaje = string.Format("El caracter {0} es invalido en este contexto", lexemaError);

                        if (t.Componente.Descripcion != null)
                        {
                            errorMensaje = t.Componente.Descripcion;
                        }                            

                        throw new ErrorLexicoException(errorMensaje, t.Componente.Fila, t.Componente.Columna);
                    }

                    if (t.Componente.Token == ComponenteLexico.TokenType.Numero)
                    {
                        if (t.Componente.Lexema.TrimStart()[0] == '-')
                        {

                            Terminal ultimoTerminal = CadenaEntrada.UltimoTerminalInsertado;

                            if (ultimoTerminal != null && TerminalesHelpers.EsTerminalConValor(ultimoTerminal))
                            {
                                //Para que no de error Sintactico, creo este otro token para que parezca que es una operacion negativa -

                                ComponenteLexico comp = new ComponenteLexico();
                                comp.Fila = t.Componente.Fila;
                                comp.Columna = t.Componente.Columna;
                                comp.Lexema = "-";
                                comp.Token = ComponenteLexico.TokenType.RestaEntero;

                                this.cadenaEntrada.InsertarTerminal(new Terminal() { Componente = comp });

                                //Le sumo uno pq el menos ya no pertenece mas.
                                t.Componente.Columna++;

                                //Le saco el - del lexema
                                t.Componente.Lexema = t.Componente.Lexema.Remove(0, 1);
                            }
                        }
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
