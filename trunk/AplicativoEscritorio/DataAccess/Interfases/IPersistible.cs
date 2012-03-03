using System;
using DataAccess.Entidades;

namespace DataAccess.Interfases
{
    public interface IPersistible
    {
        void Guardar(string path);
        void Abrir(string path);
    }
}
