using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico;
using System.IO;
using System.Diagnostics;

namespace CompiladorGargar
{
    public class Compilador
    {
        public static string directorioActual;
        public delegate void ErrorCompiladorDelegate(string tipo, string desc, int fila, int col, bool parar);

        private bool errorSemantico = false;

        private AnalizadorSintactico analizadorSintactico;

        private bool errores = false;

        //public string ArchivoEntrada { get; set; }
        public string ArchivoGramatica { get; set; }

        private bool modoDebug { get; set; }


        public Compilador(string gramatica, bool modo)
        {
            this.modoDebug = modo;
            this.ArchivoGramatica = gramatica;
            CargarAnalizadorSintactico();

            //analizadorSintactico.errorCompilacion += new ErrorCompiladorDelegate(Compilador_errorCompilacion);
            //analizadorSintactico.ArbolSemantico.errorCompilacion += new ErrorCompiladorDelegate(Compilador_errorCompilacion);
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
            res.CompilacionCorrecta = false;

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
                    res.CompilacionCorrecta = true;                    
                }
            }
            catch (Exception ex)
            {
                res.CompilacionCorrecta = false;

                if (this.modoDebug)
                {
                    res.Error = ex.Message;
                }
                else
                {
                    res.Error = "Ha habido un error en la compilacion. Por favor reporte el problema";
                }                
            }

            

            if (res.CompilacionCorrecta)
            {
                res.ArbolSemanticoResultado = this.analizadorSintactico.ArbolSemantico;

                long timeStampCod = Stopwatch.GetTimestamp();
                res.ArbolSemanticoResultado.CalcularExpresiones();
                res.CodigoPascal = res.ArbolSemanticoResultado.CalcularCodigo();
                res.TiempoGeneracionCodigo = ((float)(Stopwatch.GetTimestamp() - timeStampCod)) / ((float)Stopwatch.Frequency);
            }


            res.TiempoGeneracionAnalizadorLexico = tiempoCargarLexico;
            res.TiempoGeneracionAnalizadorSintactico = tiempoCargarSint;
            res.TiempoCompilacionTotal = ((float)(Stopwatch.GetTimestamp() - timeStamp)) / ((float)Stopwatch.Frequency);
            
            return res;
        }

        private void CargarAnalizadorLexico(string texto)
        {
            this.analizadorSintactico.CargarAnalizadorLexico(texto);
        }
    }
}
