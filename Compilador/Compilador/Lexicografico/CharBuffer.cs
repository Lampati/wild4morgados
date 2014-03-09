using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;

namespace CompiladorGargar.Lexicografico
{
    class CharBuffer
    {
        protected int fila;
        public int Fila
        {
            get { return fila; }            
        }

        protected int filaUltChar;
        public int FilaUltChar
        {
            get { return filaUltChar; }
        }

        protected int columna;
        public int Columna
        {
            get { return columna; }            
        }

        protected bool finArchivo = false;
        public bool FinArchivo
        {
            get { return finArchivo; }
        }

        protected bool habiaEspacio;
        public bool HabiaEspacio
        {
            get { return habiaEspacio; }
        }

        protected int offsetArchivo;
        protected char[] charBuffer;
        protected int charBufferMaxSize;
        protected int charBufferTope;

        protected bool ultimoBuffer = false;

        protected int indiceBuffer;

        protected String path;

        protected int extraEspaciosEnBuffer;

        public CharBuffer()
        {
        }

        public CharBuffer(String path)
        {
            this.path = path;

            this.fila = 1;
            this.columna = 0;
            this.offsetArchivo = 0;
            this.charBufferMaxSize = Convert.ToInt32(CompiladorGargar.Properties.Resources.CapacidadBuffer);
            ultimoBuffer = false;
            finArchivo = false;

            //es para que el look ahead en la ultima posicion del buffer no se pierda al cambiar el buffer
            this.extraEspaciosEnBuffer = Convert.ToInt32(CompiladorGargar.Properties.Resources.ExtraEspaciosEnBuffer);

            this.indiceBuffer = 0;
            this.LlenarBuffer();
        }

        protected virtual void LlenarBuffer()
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

            x = ObtenerProximoChar();

            while (CharHelper.esCaracterSalteable(x))
            {
                habiaEspacio = true;

                if (CharHelper.esRetornoDeCarro(x))
                {
                    x = ObtenerProximoChar();

                    if (CharHelper.esNuevaLinea(x))
                    {
                        cantFilasSalteadas++;
                        fila++;
                        columna = 0;
                    }
                }
                x = ObtenerProximoChar();
            }
            filaUltChar = fila - cantFilasSalteadas;

            return x;
        }

        public char ObtenerProximoChar()
        {
            char x = PeekProximoChar();
            indiceBuffer++;
            columna++;
            return x;
        }

        public char PeekProximoChar()
        {

            if (indiceBuffer >= charBufferTope)
            {
                LlenarBuffer();
            }
            char x = charBuffer[indiceBuffer];
            
            return x;
        }
    }
}
