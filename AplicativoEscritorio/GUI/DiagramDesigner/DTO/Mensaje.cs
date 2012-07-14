using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiagramDesigner.Enums;
using LibreriaActividades;

namespace DiagramDesigner.DTO
{
    public class Mensaje
    {
        private int linea;
        private int columna;
        private TipoMensaje tipo;
        private string contenido;
        private string figuraId;
        private string figuraNombre;
        private string figuraNombreProc;
        private object actividadReferenciada;
        

        public int Linea
        {
            get { return this.linea; }
            set { this.linea = value; }
        }

        public int Columna
        {
            get { return this.columna; }
            set { this.columna = value; }
        }

        public TipoMensaje Tipo
        {
            get { return this.tipo; }
            set { this.tipo = value; }
        }

        public string Contenido
        {
            get { return this.contenido; }
            set { this.contenido = value; }
        }

        public string FiguraId
        {
            get { return this.figuraId; }
            set { this.figuraId = value; }
        }

        public string Figura
        {
            get { return this.figuraNombre; }
            set { this.figuraNombre = value; }
        }

        public string Contexto
        {
            get { return this.figuraNombreProc; }
            set { this.figuraNombreProc = value; }
        }

        public object ActividadReferenciada
        {
            get { return this.actividadReferenciada; }
            set { this.actividadReferenciada = value; }
        }
        

        public Mensaje(string msg) : this(msg, TipoMensaje.Informacion)
        {
            this.contenido = msg;
        }

        public Mensaje(string msg, TipoMensaje tipo)
        {
            this.contenido = msg;
            this.linea = 1;
            this.columna = 1;
            this.tipo = tipo;
        }
    }
}
