using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BibliotecaMails.Procesamiento;

namespace WebProgramAR.ConsolaMails
{
    class Program
    {
        static void Main(string[] args)
        {
            BandejaMails b = new BandejaMails();
            b.Comenzar();

            Console.ReadKey();
            b.Detener();
        }
    }
}
