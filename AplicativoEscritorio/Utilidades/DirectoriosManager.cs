using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Utilidades
{
    public class DirectoriosManager
    {

        public static void BorrarArchivosDelDirPorExtension(string dir, string extension)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dir);

            foreach (FileInfo item in dirInfo.GetFiles(extension))
            {
                try
                {
                    item.Delete();
                }
                catch (Exception)
                {

                }
            }   
        }

      
    }
}
