using System;
using System.Collections.Generic;
using AplicativoEscritorio.DataAccess.Entidades;

namespace AplicativoEscritorio.DataAccess.Interfases
{
    interface IPropiedadesEjercicios
    {
        string Enunciado { get;  }
        string Gargar { get; set; }
        short NivelDificultad { get;  }
        string SolucionGargar { get;  }
        string SolucionTexto { get;  }
        Globales.Enums.ModoVisual UltimoModoGuardado { get; set; }
        DataAccess.Enums.ModoEjercicio Modo { get; set; }
        List<TestPrueba> TestsPrueba { get; }

        bool ModificadoDesdeUltimoGuardado { get; set; }
        string PathGuardadoActual { get; set; }
        string Nombre { get; }
        string Extension { get; }
        bool CompilacionCorrecta { get; set; }
        bool EjecucionCorrecta { get; set; }
    }
}
