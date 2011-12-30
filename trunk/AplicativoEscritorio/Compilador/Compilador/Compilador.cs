using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico;
using System.IO;
using System.Diagnostics;
using Utilidades;

namespace CompiladorGargar
{
    public class Compilador
    {
        private AnalizadorSintactico analizadorSintactico;

        public string ArchivoGramatica { get; set; }

        public string DirectorioTemporales { get; set; }
        public string DirectorioEjecutables { get; set; }

        private bool modoDebug { get; set; }

        public Compilador(string gramatica, bool modo, string dirTemp, string dirEjec)
        {
            this.modoDebug = modo;
            this.ArchivoGramatica = gramatica;
            this.DirectorioTemporales = dirTemp;
            this.DirectorioEjecutables = dirEjec;

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

            this.analizadorSintactico.ReiniciarAnalizadorSintactico();
            float tiempoCargarSint = ((float)(Stopwatch.GetTimestamp() - timeStamp)) / ((float)Stopwatch.Frequency);

            long timeStampLex = Stopwatch.GetTimestamp();
            CargarAnalizadorLexico(texto);
            float tiempoCargarLexico = ((float)(Stopwatch.GetTimestamp() - timeStampLex)) / ((float)Stopwatch.Frequency);

            List<PasoDebugTiempos> tiempos = new List<PasoDebugTiempos>();

            ResultadoCompilacion res = new ResultadoCompilacion();
            res.CompilacionGarGarCorrecta = false;

            int i = 1;

            try
            {
                bool pararComp = false;
                Global.TipoError tipoError = Global.TipoError.Ninguno;

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
                                case Global.TipoError.Sintactico:
                                    tipoError = item.TipoError;
                                    res.ListaErrores.Add(item);
                                    pararComp = pararComp || item.PararCompilacion;
                                    break;
                                case Global.TipoError.Semantico:
                                    tipoError = item.TipoError;
                                    res.ListaErrores.Add(item);
                                    pararComp = pararComp || item.PararCompilacion;
                                    break;
                                case Global.TipoError.Ninguno:
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
                    res.Error = ex.Message;
                }
                else
                {
                    res.Error = "Ha habido un error en la compilacion. Por favor reporte el problema";
                }                
            }

            

            if (res.CompilacionGarGarCorrecta)
            {
                res.ArbolSemanticoResultado = this.analizadorSintactico.ArbolSemantico;

                long timeStampCod = Stopwatch.GetTimestamp();
                res.ArbolSemanticoResultado.CalcularExpresiones();
                res.CodigoPascal = res.ArbolSemanticoResultado.CalcularCodigo();
                res.TiempoGeneracionCodigo = ((float)(Stopwatch.GetTimestamp() - timeStampCod)) / ((float)Stopwatch.Frequency);

                timeStampCod = Stopwatch.GetTimestamp();
                res.ArchTemporalCodigoPascal =  CrearArchivoTemporal(res.CodigoPascal);
                res.ArchTemporalCodigoPascalConRuta = Path.Combine(DirectorioTemporales, res.ArchTemporalCodigoPascal);
                res.TiempoGeneracionTemporalCodigo = ((float)(Stopwatch.GetTimestamp() - timeStampCod)) / ((float)Stopwatch.Frequency);

                try
                {
                    timeStampCod = Stopwatch.GetTimestamp();
                    res.ArchEjecutable = CompilarPascal(res.ArchTemporalCodigoPascalConRuta);
                    res.ArchEjecutableConRuta = Path.Combine(DirectorioEjecutables, res.ArchEjecutable);
                    res.TiempoGeneracionEjecutable = ((float)(Stopwatch.GetTimestamp() - timeStampCod)) / ((float)Stopwatch.Frequency);

                    res.GeneracionEjectuableCorrecto = true;
                }
                catch
                {
                    res.GeneracionEjectuableCorrecto = false;
                }

                BorrarTemporales();

            }

            res.TiempoGeneracionAnalizadorLexico = tiempoCargarLexico;
            res.TiempoGeneracionAnalizadorSintactico = tiempoCargarSint;
            res.TiempoCompilacionTotal = ((float)(Stopwatch.GetTimestamp() - timeStamp)) / ((float)Stopwatch.Frequency);
            
            return res;
        }

        private void BorrarTemporales()
        {
            DirectoriosManager.BorrarArchivosDelDirPorExtension(DirectorioTemporales, "*.pas");
            DirectoriosManager.BorrarArchivosDelDirPorExtension(DirectorioTemporales, "*.o");
        }

       

        private string CompilarPascal(string archTemporalPascal)
        {
            string exe = Path.Combine(DirectorioEjecutables,"blabla.exe");

            string argumento = string.Format("-o{0}", exe);

            EjecucionManager.EjecutarSinVentana(Globales.Global.NOMBRE_ARCH_COMPILADOR_PASCAL, new List<string>() { argumento, archTemporalPascal });

            return exe;
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
