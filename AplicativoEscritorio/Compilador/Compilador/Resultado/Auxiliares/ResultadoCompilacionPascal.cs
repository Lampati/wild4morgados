using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Semantico.Arbol;
using CompiladorGargar.Resultado.Auxiliares;

namespace CompiladorGargar.Resultado.Auxiliares
{
    public class ResultadoCompilacionPascal
    {
        public bool CompilacionPascalCorrecta { get; set; }

        public string NombreEjecutable { get; set; }    

        public List<string> ListaErrores { get; set; }
        public List<string> ListaWarnings { get; set; }
        public List<string> ListaNotes { get; set; }
        public List<string> ListaHints { get; set; }

        public ResultadoCompilacionPascal()
        {
            ListaErrores = new List<string>();
            ListaWarnings = new List<string>();
            ListaNotes = new List<string>();
            ListaHints = new List<string>();
         
        }

        public ResultadoCompilacionPascal(string output)
        {
            ListaErrores = new List<string>();
            ListaWarnings = new List<string>();
            ListaNotes = new List<string>();
            ListaHints = new List<string>();

            string[] lineas = output.Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in lineas)
            {
                string[] contenido = item.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

                if (contenido[0].Contains("Fatal") || contenido[0].Contains("Error"))
                {
                    ListaErrores.Add(contenido[1]);
                }                
                else if (contenido[0].Contains("Warning"))
                {
                    ListaWarnings.Add(contenido[1]);
                }
                else if (contenido[0].Contains("Note"))
                {
                    ListaNotes.Add(contenido[1]);
                }
                else if (contenido[0].Contains("Hint"))
                {
                    ListaHints.Add(contenido[1]);
                }                
            }

            CompilacionPascalCorrecta = ListaErrores.Count == 0;           

        }
        
    }

    

   

  
}
