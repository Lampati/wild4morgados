﻿using System;
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
    class AnalizadorLexicografico
    {

        protected String path;

        protected AFD afd;

        protected CharBuffer charBuffer;

        protected bool dentroComentario;

        public AnalizadorLexicografico()
        {

        }

        //Inicializo lo estrictamente necesario. Creo el buffer a partir del archivo y el AFD a partir de otro archivo
        public AnalizadorLexicografico(String pathArch)
        {
            try
            {                
                this.path = pathArch;                              

                this.dentroComentario = false;

                
                long timeStamp = Stopwatch.GetTimestamp();
                
                //this.afd = new AFD(Path.Combine(CompiladorForm.directorioActual, System.Configuration.ConfigurationManager.AppSettings["archAFD"].ToString()));
                this.afd = new AFD();

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
            while (!charBuffer.FinArchivo)
            {    
                //Me va a devolver cualquier caracter menos un \r o \n... esos los saltea.                
                char x = charBuffer.ObtenerProximoCaracterValido();

                componente.AntecedidoPorSeparador = this.charBuffer.HabiaEspacio;
                //Puede haber sido el ultimo caracter del archivo un \r o \n, asi que chequeo
                if (x != char.MinValue)
                {
                    componente.Lexema += x;
                    if (afd.TryAvanzar(x))
                    {
                        afd.Avanzar(x);

                        //Mientras pueda avanzar (que el AFD lo permita), voy a seguir añadiendo caracteres al lexema.
                        while (LookAhead())
                        {
                            x = charBuffer.ObtenerProximoChar();
                            componente.Lexema += x;
                            afd.Avanzar(x);
                        }

                        
                        if (!dentroComentario)
                        {

                            if (afd.EstadoActual.EsFinal)
                            {
                                
                                if (!(afd.EstadoActual.Token == ComponenteLexico.TokenType.ComentarioApertura))
                                {
                                    componente.Token = afd.EstadoActual.Token;
                                    componente.Fila = charBuffer.Fila;
                                    //componente.Fila = charBuffer.FilaUltChar;
                                    componente.Columna = charBuffer.Columna - componente.Lexema.Length;

                                    afd.ResetearAFD();

                                    return componente;
                                }
                                else
                                {
                                    dentroComentario = true;
                                    componente = new ComponenteLexico();

                                    afd.ResetearAFD();
                                }


                            }

                            else
                            {
                                //el lexema no pertenecia a ningun token si el afd no termino en un estado final.
                                componente.Token = ComponenteLexico.TokenType.Error;
                                componente.Fila = charBuffer.Fila;
                                //componente.Fila = charBuffer.FilaUltChar;
                                componente.Columna = charBuffer.Columna - componente.Lexema.Length;
                                componente.Descripcion = "'"+componente.Lexema + "' no es un lexema valido en el lenguaje CPL";
                                afd.ResetearAFD();
                                return componente;
                            }
                        }
                        else
                        {
                            //Si estoy dentro de un comentario, me fijo si el token armado es el de salida.
                            if ((afd.EstadoActual.Token == ComponenteLexico.TokenType.ComentarioClausura))
                            {
                                dentroComentario = false;
                                componente = new ComponenteLexico();
                            }
                            afd.ResetearAFD();
                        }  
                    }
                    else
                    {
                        //Si estoy dentro de un comentario, simplemente lo descarto.
                        if (!dentroComentario)
                        {
                            //El primer caracter leido no servia para formar ningun lexema
                            componente.Token = ComponenteLexico.TokenType.Error;
                            componente.Fila = charBuffer.Fila;
                            //componente.Fila = this.charBuffer.FilaUltChar;
                            componente.Columna = charBuffer.Columna - componente.Lexema.Length;
                            componente.Descripcion = "'" + componente.Lexema + "' no es un lexema valido en el lenguaje CPL";
                            afd.ResetearAFD();
                            return componente;
                        }
                    }
                }                
            }

            //Se devuelve EOF si no se hizo retorno dentro del while.
            componente.Fila = charBuffer.Fila;
            //componente.Fila = charBuffer.FilaUltChar;
            componente.Columna = charBuffer.Columna;
            componente.Token = ComponenteLexico.TokenType.EOF;
            componente.Lexema = "$";
            afd.ResetearAFD();
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
