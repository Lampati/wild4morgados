using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Interfases
{
    interface Accesible
    {
        void Abrir(string path);
        void Guardar(string path);
        
    }
}
