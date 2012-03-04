using System;
using System.Collections.Generic;
namespace DataAccess.Entidades
{
    interface IPropiedadesEjercicios
    {
        string Enunciado { get;  }
        string Gargar { get; set; }
        DataAccess.Enums.NivelDificultad NivelDificultad { get;  }
        string SolucionGargar { get;  }
        string SolucionTexto { get;  }
        Globales.Enums.ModoVisual UltimoModoGuardado { get; set; }
        DataAccess.Enums.ModoEjercicio Modo { get; set; }
        List<TestPrueba> TestsPrueba { get; }

        bool ModificadoDesdeUltimoGuardado { get; set; }
        string PathGuardadoActual { get; set; }
        string Nombre { get; set; }
    }
}
