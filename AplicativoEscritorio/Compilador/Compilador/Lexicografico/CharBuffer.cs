using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;

namespace Compilador.Lexicografico
{
    class CharBuffer
    {
        private int fila;
        public int Fila
        {
            get { return fila; }            
        }

        private int filaUltChar;
        public int FilaUltChar
        {
            get { return filaUltChar; }
        }

        private int columna;
        public int Columna
        {
            get { return columna; }            
        }        

        private bool finArchivo = false;
        public bool FinArchivo
        {
            get { return finArchivo; }
        }

        private bool habiaEspacio;
        public bool HabiaEspacio
        {
            get { return habiaEspacio; }
        }

        private int offsetArchivo;
        private char[] charBuffer;
        private int charBufferMaxSize;
        private int charBufferTope;

        private bool ultimoBuffer = false;

        private int indiceBuffer;

        private String path;

        private int extraEspaciosEnBuffer;

        public CharBuffer(String path)
        {
            this.path = path;

            this.fila = 1;
            this.columna = 0;
            this.offsetArchivo = 0;
            this.charBufferMaxSize = Convert.ToInt32(ConfigurationSettings.AppSettings["capacidadBuffer"].ToString());
            ultimoBuffer = false;
            finArchivo = false;

            //es para que el look ahead en la ultima posicion del buffer no se pierda al cambiar el buffer
            this.extraEspaciosEnBuffer = Convert.ToInt32(ConfigurationSettings.AppSettings["extraEspaciosEnBuffer"].ToString());

            this.indiceBuffer = 0;
            this.LlenarBuffer();
        }

        private void LlenarBuffer()
        {
            try
            {
                if (!ultimoBuffer)
                {
                    this.indiceBuffer = 0;

                    StreamReader strReader = new StreamReader(path);

                    //Creo el buffer con la capacidad elegida y unos caracteres mas.
                    charBuffer = new char[this.charBufferMaxSize + this.extraEspaciosEnBuffer];

                    strReader.BaseStream.Seek(offsetArchivo, SeekOrigin.Begin);

                    //Le resto la cantidad de espacios extras al tope, asi el offset del archivo 
                    // queda esa cantidad de caracteres retrasados, para volverlos a leer cuando haga el switch
                    // del buffer.
                    this.charBufferTope = strReader.Read(charBuffer, 0, charBuffer.Length) - this.extraEspaciosEnBuffer;
                    
                    offsetArchivo += this.charBufferTope;

                    if (this.charBufferTope < charBuffer.Length - this.extraEspaciosEnBuffer)
                    {
                        this.ultimoBuffer = true;
                    }

                    //String aux = strReader.ReadToEnd();

                    strReader.Close();
                }
                else
                {
                    this.finArchivo = true;
                }
            }
            catch (Exception ex)
            {
                Utils.Log.AddError(ex.Message);
                throw new Exception("Error al rellenar el buffer");
            }
            //return aux;
        }

        public char ObtenerProximoCaracterValido()
        {
            char x;

            this.habiaEspacio = false;

            int cantFilasSalteadas = 0;

            x = this.ObtenerProximoChar();

            while (CharHelper.esCaracterSalteable(x))
            {
                this.habiaEspacio = true;

                if (CharHelper.esRetornoDeCarro(x))
                {
                    x = this.ObtenerProximoChar();

                    if (CharHelper.esNuevaLinea(x))
                    {
                        cantFilasSalteadas++;
                        fila++;
                        columna = 0;
                    }
                }
                x = this.ObtenerProximoChar();
            }
            filaUltChar = fila - cantFilasSalteadas;

            return x;
        }

        public char ObtenerProximoChar()
        {
            char x = this.PeekProximoChar();
            indiceBuffer++;
            columna++;
            return x;
        }

        public char PeekProximoChar()
        {

            if (indiceBuffer >= this.charBufferTope)
            {
                this.LlenarBuffer();
            }
            char x = this.charBuffer[indiceBuffer];
            
            return x;
        }
    }
}
