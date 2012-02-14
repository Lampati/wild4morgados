using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CompiladorGargar.Auxiliares.AF;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace CompiladorGargar.Lexicografico
{
    class AnalizadorLexicograficoConArchEnMemoria : AnalizadorLexicografico
    {

        private string texto;       
     



        //Inicializo lo estrictamente necesario. Creo el buffer a partir del archivo y el AFD a partir de otro archivo
        public AnalizadorLexicograficoConArchEnMemoria(String texto)
        {
            try
            {                
                this.texto = texto;                              

                this.dentroComentario = false;

                
                long timeStamp = Stopwatch.GetTimestamp();
                
                //base.afd = new AFD(Path.Combine(CompiladorForm.directorioActual, System.Configuration.ConfigurationManager.AppSettings["archAFD"].ToString()));
                base.afd = new AFD();


                float num = ((float)(Stopwatch.GetTimestamp() - timeStamp)) / ((float)Stopwatch.Frequency);
                String hola = (String.Format("{0} segundos", num.ToString()));


                base.charBuffer = new CharBufferEnMemoria(texto);

            }
            catch (Exception ex)
            {
                Utils.Log.AddError(ex.Message);
                throw new Exception("Error al crear el analizador lexicografico"+"\r\n"+ex.Message);
            }
        }

        
      
    }

    
}
