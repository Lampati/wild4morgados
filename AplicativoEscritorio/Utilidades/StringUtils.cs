using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Utilidades
{
    public static class StringUtils
    {
        public static void InsertarLineaEnArchivo(string archLeer, string textoAInsertar, int linea)
        {

            

            string tempfile = Path.GetTempFileName();
            StreamWriter writer = new StreamWriter(tempfile);
            StreamReader reader = new StreamReader(archLeer);
            
            int cantLineasLeidas = 1;
            while (!reader.EndOfStream)
            {
                if (cantLineasLeidas == linea)
                {
                    writer.WriteLine(textoAInsertar);
                }

                writer.WriteLine(reader.ReadLine());
                cantLineasLeidas++;
            }
            writer.Close();
            reader.Close();
            File.Copy(tempfile, archLeer, true);


        }
    }
}
