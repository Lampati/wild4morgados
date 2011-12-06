using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Compilador.Auxiliares.AF;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace Compilador.Lexicografico
{
    class AnalizadorLexicografico
    {

        private String path;
        
        private AFD afd;

        private CharBuffer charBuffer;

        private bool dentroComentario;


        //Inicializo lo estrictamente necesario. Creo el buffer a partir del archivo y el AFD a partir de otro archivo
        public AnalizadorLexicografico(String pathArch)
        {
            try
            {                
                this.path = pathArch;                              

                this.dentroComentario = false;

                
                long timeStamp = Stopwatch.GetTimestamp();
                
                this.afd = new AFD(Path.Combine(Compilador.directorioActual, ConfigurationSettings.AppSettings["archAFD"].ToString()));

                float num = ((float)(Stopwatch.GetTimestamp() - timeStamp)) / ((float)Stopwatch.Frequency);
                String hola = (String.Format("{0} segundos", num.ToString()));

                
                this.charBuffer = new CharBuffer(pathArch);

            }
            catch (Exception ex)
            {
                Utils.Log.AddError(ex.Message);
                throw new Exception("Error al crear el analizador lexicografico"+"\r\n"+ex.Message);
            }
        }

        
        public ComponenteLexico ObtenerProximoToken()
        {
            ComponenteLexico componente = new ComponenteLexico();
            while (!this.charBuffer.FinArchivo)
            {    
                //Me va a devolver cualquier caracter menos un \r o \n... esos los saltea.                
                char x = this.charBuffer.ObtenerProximoCaracterValido();

                componente.AntecedidoPorSeparador = this.charBuffer.HabiaEspacio;
                //Puede haber sido el ultimo caracter del archivo un \r o \n, asi que chequeo
                if (x != char.MinValue)
                {
                    componente.Lexema += x;
                    if (this.afd.TryAvanzar(x))
                    {
                        this.afd.Avanzar(x);

                        //Mientras pueda avanzar (que el AFD lo permita), voy a seguir añadiendo caracteres al lexema.
                        while (this.LookAhead())
                        {
                            x = this.charBuffer.ObtenerProximoChar();
                            componente.Lexema += x;
                            this.afd.Avanzar(x);
                        }

                        
                        if (!dentroComentario)
                        {

                            if (this.afd.EstadoActual.EsFinal)
                            {
                                
                                if (!(this.afd.EstadoActual.Token == ComponenteLexico.TokenType.ComentarioApertura))
                                {
                                    componente.Token = this.afd.EstadoActual.Token;
                                    componente.Fila = this.charBuffer.Fila;
                                    componente.Columna = this.charBuffer.Columna - componente.Lexema.Length;

                                    this.afd.ResetearAFD();

                                    return componente;
                                }
                                else
                                {
                                    dentroComentario = true;
                                    componente = new ComponenteLexico();

                                    this.afd.ResetearAFD();
                                }


                            }

                            else
                            {
                                //el lexema no pertenecia a ningun token si el afd no termino en un estado final.
                                componente.Token = ComponenteLexico.TokenType.Error;
                                componente.Fila = this.charBuffer.Fila;
                                componente.Columna = this.charBuffer.Columna - componente.Lexema.Length;
                                componente.Descripcion = "'"+componente.Lexema + "' no es un lexema valido en el lenguaje CPL";
                                this.afd.ResetearAFD();
                                return componente;
                            }
                        }
                        else
                        {
                            //Si estoy dentro de un comentario, me fijo si el token armado es el de salida.
                            if ((this.afd.EstadoActual.Token == ComponenteLexico.TokenType.ComentarioClausura))
                            {
                                dentroComentario = false;
                                componente = new ComponenteLexico();
                            }
                            this.afd.ResetearAFD();
                        }  
                    }
                    else
                    {
                        //Si estoy dentro de un comentario, simplemente lo descarto.
                        if (!dentroComentario)
                        {
                            //El primer caracter leido no servia para formar ningun lexema
                            componente.Token = ComponenteLexico.TokenType.Error;
                            componente.Fila = this.charBuffer.Fila;
                            componente.Columna = this.charBuffer.Columna - componente.Lexema.Length;
                            componente.Descripcion = "'" + componente.Lexema + "' no es un lexema valido en el lenguaje CPL";
                            this.afd.ResetearAFD();
                            return componente;
                        }
                    }
                }                
            }

            //Se devuelve EOF si no se hizo retorno dentro del while.
            componente.Fila = this.charBuffer.Fila;
            componente.Columna = this.charBuffer.Columna;
            componente.Token = ComponenteLexico.TokenType.EOF;
            componente.Lexema = "$";
            this.afd.ResetearAFD();
            return componente;
            
        }

        private bool LookAhead()
        {
            char x = this.charBuffer.PeekProximoChar();

            return this.afd.TryAvanzar(x);
            
        }

        public int FilaActual()
        {
            return this.charBuffer.Fila;
        }

        public int ColumnaActual()
        {
            return this.charBuffer.Columna;
        }
    }

    
}
