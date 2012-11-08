using System;
using AplicativoEscritorio.DataAccess.Entidades;

namespace AplicativoEscritorio.DataAccess.Interfases
{
    public interface IPersistible
    {
        void Guardar(string path);
        void Abrir(string path);
    }
}
