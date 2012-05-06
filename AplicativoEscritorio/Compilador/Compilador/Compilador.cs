using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico;
using System.IO;
using System.Diagnostics;
using Utilidades;
using CompiladorGargar.Resultado.Auxiliares;
using CompiladorGargar.Resultado;
using CompiladorGargar.Lexicografico;
using EJEKOR;

namespace CompiladorGargar
{
    public class Compilador
    {
        private AnalizadorSintactico analizadorSintactico;

        public string ArchivoGramatica { get; set; }

        public string DirectorioTemporales { get; set; }
        public string DirectorioEjecutables { get; set; }

        public string NombreEjecutable { get; set; }

        private bool modoDebug { get; set; }

        public bool MarcarEntrada { get; set; }
        public int LineaEntrada { get; set; }

        public bool ReemplazarSalida { get; set; }
        public bool ReemplazarEntrada { get; set; }

        public string CodigoReemplazoEntrada { get; set; }
        public string CodigoReemplazoSalida { get; set; }
        public int LineaComienzoReemplazoSalida { get; set; }
        public int LineaFinReemplazoSalida { get; set; }

        public Compilador(bool modo, string dirTemp, string dirEjec, string nombre)
        {
            MarcarEntrada = false;
            LineaEntrada = -1;
            this.modoDebug = modo;
            //this.ArchivoGramatica = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, System.Configuration.ConfigurationManager.AppSettings["archGramatica"].ToString());
            this.ArchivoGramatica = "Gramatica.xml";
            this.DirectorioTemporales = dirTemp;
            this.DirectorioEjecutables = dirEjec;
            this.NombreEjecutable = nombre;

            GeneracionCodigoHelpers.DirectorioTemporales = dirTemp;

            CargarAnalizadorSintactico();
        }

        public Compilador(string gramatica, bool modo, string dirTemp, string dirEjec, string nombre)
        {
            this.modoDebug = modo;
            this.ArchivoGramatica = gramatica;
            this.DirectorioTemporales = dirTemp;
            this.DirectorioEjecutables = dirEjec;
            this.NombreEjecutable = nombre;

            CargarAnalizadorSintactico();
        }

        private void CargarAnalizadorSintactico()
        {
            try
            {
                analizadorSintactico = new AnalizadorSintactico(ArchivoGramatica);
                analizadorSintactico.HabilitarSemantico = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fatal al iniciar el analizador sintactico:" + "\r\n" + ex.Message);

            }
        }

        private void CargarAnalizadorLexico(string texto)
        {
            this.analizadorSintactico.CargarAnalizadorLexico(texto);
        }

        public ResultadoCompilacion Compilar(string texto)
        {
            long timeStamp = Stopwatch.GetTimestamp();
            long timeStampPaso;

            

            GeneracionCodigoHelpers.ReiniciarValoresVariablesAleatorias();

            this.analizadorSintactico.ReiniciarAnalizadorSintactico();
            float tiempoCargarSint = ((float)(Stopwatch.GetTimestamp() - timeStamp)) / ((float)Stopwatch.Frequency);

            List<PasoDebugTiempos> tiempos = new List<PasoDebugTiempos>();

            ResultadoCompilacion res = new ResultadoCompilacion();
            res.CompilacionGarGarCorrecta = false;

            if (this.ReemplazarEntrada)
            {
                

                texto = StringUtils.InsertarLineaEnTexto(texto,
                        this.CodigoReemplazoEntrada,
                        this.LineaEntrada);
            }

            if (this.ReemplazarSalida)
            {


                texto = StringUtils.InsertarLineaEnTextoEntreLineas(texto,
                        this.CodigoReemplazoSalida,
                        this.LineaComienzoReemplazoSalida,
                        this.LineaFinReemplazoSalida);
            }

            int i = 1;

            try
            {
                long timeStampLex = Stopwatch.GetTimestamp();
                CargarAnalizadorLexico(texto);
                float tiempoCargarLexico = ((float)(Stopwatch.GetTimestamp() - timeStampLex)) / ((float)Stopwatch.Frequency);



                try
                {
                    bool pararComp = false;
                    GlobalesCompilador.TipoError tipoError = GlobalesCompilador.TipoError.Ninguno;

                    while (!this.analizadorSintactico.esFinAnalisisSintactico() && !pararComp)
                    {

                        timeStampPaso = Stopwatch.GetTimestamp();
                        List<PasoAnalizadorSintactico> retorno = this.analizadorSintactico.AnalizarSintacticamenteUnPaso();
                        float tiempoAnalizSint = ((float)(Stopwatch.GetTimestamp() - timeStampPaso)) / ((float)Stopwatch.Frequency);


                        if (retorno.Count > 0)
                        {
                            foreach (var item in retorno)
                            {
                                switch (item.TipoError)
                                {
                                    case GlobalesCompilador.TipoError.Sintactico:
                                        tipoError = item.TipoError;
                                        res.ListaErrores.Add(item);
                                        pararComp = pararComp || item.PararCompilacion;
                                        break;
                                    case GlobalesCompilador.TipoError.Semantico:
                                        tipoError = item.TipoError;
                                        res.ListaErrores.Add(item);
                                        pararComp = pararComp || item.PararCompilacion;
                                        break;
                                    case GlobalesCompilador.TipoError.Ninguno:
                                        tipoError = item.TipoError;
                                        break;

                                }
                            }
                        }

                        if (modoDebug)
                        {

                            PasoCompilacion paso = new PasoCompilacion(this.analizadorSintactico.Pila.ToString(),
                                this.analizadorSintactico.CadenaEntrada.ToString(),
                                tipoError);

                            res.ListaDebugSintactico.Add(paso);
                        }

                        float numPaso = ((float)(Stopwatch.GetTimestamp() - timeStampPaso)) / ((float)Stopwatch.Frequency);

                        tiempos.Add(new PasoDebugTiempos() { NumPaso = i, TiempoAnalizadorSint = tiempoAnalizSint, TiempoAnalizadorTot = numPaso }); ;
                        i++;
                    }

                    if (this.analizadorSintactico.esFinAnalisisSintactico() && res.ListaErrores.Count == 0)
                    {
                        res.CompilacionGarGarCorrecta = true;
                    }
                }
                catch (Exception ex)
                {
                    res.CompilacionGarGarCorrecta = false;

                    if (this.modoDebug)
                    {
                        res.Error = string.Format("{0}: \r\n {1}", ex.Message, ex.StackTrace);
                    }
                    else
                    {
                        res.Error = "Ha habido un error en la compilacion. Por favor reporte el problema";
                    }
                }




                if (res.CompilacionGarGarCorrecta)
                {
                    res.ArbolSemanticoResultado = this.analizadorSintactico.ArbolSemantico;
                    res.TablaSimbolos = res.ArbolSemanticoResultado.TablaDeSimbolos;

                    res.ListaLineasValidas = EstadoSintactico.ListaLineasValidasParaInsertarCodigo;
                    res.ListaLineasContenidoProcSalida = EstadoSintactico.ListaLineasContenidoProcSalida;

                    long timeStampCod = Stopwatch.GetTimestamp();
                    res.ArbolSemanticoResultado.CalcularExpresiones();
                    res.CodigoPascal = res.ArbolSemanticoResultado.CalcularCodigo();

                    Dictionary<int, int> bindeoLineasEntrePascalYGarGar = BindearLineas(res.CodigoPascal.Split(new string[] { "\r\n" }, StringSplitOptions.None));

                    //Esto se usa en la parte de los test de ejecucion. Le agrego que marque el valor de las var de entrada en la linea.
                    
                    
                    res.TiempoGeneracionCodigo = ((float)(Stopwatch.GetTimestamp() - timeStampCod)) / ((float)Stopwatch.Frequency);

                    timeStampCod = Stopwatch.GetTimestamp();
                    res.ArchTemporalCodigoPascal = CrearArchivoTemporal(res.CodigoPascal);
                    res.ArchTemporalCodigoPascalConRuta = Path.Combine(DirectorioTemporales, res.ArchTemporalCodigoPascal);
                    res.TiempoGeneracionTemporalCodigo = ((float)(Stopwatch.GetTimestamp() - timeStampCod)) / ((float)Stopwatch.Frequency);

                    try
                    {
                        timeStampCod = Stopwatch.GetTimestamp();

                        // Aca van las operaciones especiales con el compilador
                        if (this.MarcarEntrada)
                        {
                            int lineaEnPascal = bindeoLineasEntrePascalYGarGar.First(x => x.Value == this.LineaEntrada).Key;
                            

                            StringUtils.InsertarLineaEnArchivo(res.ArchTemporalCodigoPascalConRuta,
                                GeneracionCodigoHelpers.LlamarCrearEntradaEnArchResultado,
                                lineaEnPascal);

                        }

                      

                        
                        ResultadoCompilacionPascal resPas = CompilarPascal(res.ArchTemporalCodigoPascalConRuta, bindeoLineasEntrePascalYGarGar);

                        

                        res.ResultadoCompPascal = resPas;
                        res.ArchEjecutable = resPas.NombreEjecutable;
                        res.ArchEjecutableConRuta = Path.Combine(DirectorioEjecutables, res.ArchEjecutable);
                        res.ArchTemporalResultadosEjecucionConRuta = GeneracionCodigoHelpers.ArchivoTemporalEstaEjecucion;
                        res.TiempoGeneracionEjecutable = ((float)(Stopwatch.GetTimestamp() - timeStampCod)) / ((float)Stopwatch.Frequency);

                        res.GeneracionEjectuableCorrecto = resPas.CompilacionPascalCorrecta;                        

                    }
                    catch
                    {
                        res.GeneracionEjectuableCorrecto = false;
                    }

                    BorrarTemporales();

                }

                res.TiempoGeneracionAnalizadorLexico = tiempoCargarLexico;
            }
            catch (ErrorLexicoException ex)
            {
                res.ListaErrores.Add(new PasoAnalizadorSintactico(
                    ex.Descripcion,
                    GlobalesCompilador.TipoError.Sintactico,
                    ex.Fila,
                    ex.Columna));
            }
            catch (Exception ex)
            {
                res.CompilacionGarGarCorrecta = false;

                if (this.modoDebug)
                {
                    res.Error = ex.Message;
                }
                else
                {
                    res.Error = "Ha habido un error en la compilacion. Por favor reporte el problema";
                }
            }
            
            res.TiempoGeneracionAnalizadorSintactico = tiempoCargarSint;
            res.TiempoCompilacionTotal = ((float)(Stopwatch.GetTimestamp() - timeStamp)) / ((float)Stopwatch.Frequency);
            
            return res;
        }

        private Dictionary<int, int> BindearLineas(string[] lineas)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();

            int numLineaGargar = 0;

            for (int i = 0; i < lineas.Length; i++)
            {
                int numLineaPascal = i + 1;

                string linea = lineas[i].Trim('\t');

                dict.Add(numLineaPascal, numLineaGargar);

                if (linea.Contains(GeneracionCodigoHelpers.VariableContadoraLineas))
                {
                    string[] res = linea.Split(new string[] { ":=" }, StringSplitOptions.None);

                    if (res.Length > 1)
                    {
                        string num = res[1].TrimStart().TrimEnd().TrimEnd(';');
                        numLineaGargar = Convert.ToInt32(num);
                    }
                }
                         
            }

            return dict;
        }

        private void BorrarTemporales()
        {
            if (!string.IsNullOrWhiteSpace(DirectorioTemporales))
            {
                DirectoriosManager.BorrarArchivosDelDirPorExtension(DirectorioTemporales, "*.pas");
                DirectoriosManager.BorrarArchivosDelDirPorExtension(DirectorioTemporales, "*.o");
            }
        }



        private ResultadoCompilacionPascal CompilarPascal(string archTemporalPascal, Dictionary<int, int> bindeoLineas)
        {
            ResultadoCompilacionPascal res;

            try
            {
                archTemporalPascal = string.Format("{0}{1}{0}",'"',archTemporalPascal);

                string exe = Path.Combine(DirectorioEjecutables, NombreEjecutable);

                if (!exe.Contains(".exe"))
                {
                    exe = string.Concat(exe, ".exe");
                }

                string auxExe = string.Format("{0}{1}{0}", '"', exe);

                string pathIncludes = Path.Combine(Globales.ConstantesGlobales.PathEjecucionAplicacion, Globales.ConstantesGlobales.NOMBRE_DIR_UNITS_PASCAL);
                pathIncludes = string.Format("{0}{1}{0}", '"', pathIncludes);


                string argumentoInclude = string.Format("-Fu{0}", pathIncludes);
                string argumentoModoCompilacion = string.Format("-Mobjfpc");
                string argumentoUseAnsiStrings = string.Format("-Sh");
                string argumentoChequearIndicesDeArreglos = string.Format("-Cr");
                string argumentoNombreExe = string.Format("-o{0}", auxExe);

                string resultado = EjecucionManager.EjecutarSinVentana(Globales.ConstantesGlobales.NOMBRE_ARCH_COMPILADOR_PASCAL, new List<string>() { argumentoInclude, argumentoModoCompilacion, argumentoUseAnsiStrings, argumentoChequearIndicesDeArreglos, argumentoNombreExe, archTemporalPascal });

                res = new ResultadoCompilacionPascal(resultado, bindeoLineas);
                res.NombreEjecutable = exe;
            }
            catch (Exception)
            {
                res = new ResultadoCompilacionPascal();
                res.CompilacionPascalCorrecta = false;
                //res.ListaErrores.Add("Error fatal al intentar ejecutar el compilador de codigo intermedio.");
            }

            return res;
        }

        private string CrearArchivoTemporal(string cod)
        {
            string nombreArch = string.Format("{0}{1}",RandomManager.RandomStringConPrefijo("tempPas",20,true),".pas");

            using (StreamWriter strWriter = new StreamWriter(Path.Combine(this.DirectorioTemporales, nombreArch), false))
            {
                strWriter.WriteLine(cod);

                strWriter.Flush();

                strWriter.Close();
            }

            return nombreArch;
        }







        
    }
}
