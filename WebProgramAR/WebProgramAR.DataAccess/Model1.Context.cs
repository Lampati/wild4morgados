//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.EntityClient;
using WebProgramAR.Entidades;

namespace WebProgramAR.DataAccess
{
    public partial class WebProgramAREntities : ObjectContext
    {
        public const string ConnectionString = "name=WebProgramAREntities";
        public const string ContainerName = "WebProgramAREntities";
    
        #region Constructors
    
        public WebProgramAREntities()
            : base(ConnectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public WebProgramAREntities(string connectionString)
            : base(connectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public WebProgramAREntities(EntityConnection connection)
            : base(connection, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        #endregion
    
        #region ObjectSet Properties
    
        public ObjectSet<Curso> Cursos
        {
            get { return _cursos  ?? (_cursos = CreateObjectSet<Curso>("Cursos")); }
        }
        private ObjectSet<Curso> _cursos;
    
        public ObjectSet<EstadoEjercicio> EstadoEjercicios
        {
            get { return _estadoEjercicios  ?? (_estadoEjercicios = CreateObjectSet<EstadoEjercicio>("EstadoEjercicios")); }
        }
        private ObjectSet<EstadoEjercicio> _estadoEjercicios;
    
        public ObjectSet<Localidad> Localidades
        {
            get { return _localidades  ?? (_localidades = CreateObjectSet<Localidad>("Localidades")); }
        }
        private ObjectSet<Localidad> _localidades;
    
        public ObjectSet<Pais> Paises
        {
            get { return _paises  ?? (_paises = CreateObjectSet<Pais>("Paises")); }
        }
        private ObjectSet<Pais> _paises;
    
        public ObjectSet<Provincia> Provincias
        {
            get { return _provincias  ?? (_provincias = CreateObjectSet<Provincia>("Provincias")); }
        }
        private ObjectSet<Provincia> _provincias;
    
        public ObjectSet<TipoUsuario> TipoUsuarios
        {
            get { return _tipoUsuarios  ?? (_tipoUsuarios = CreateObjectSet<TipoUsuario>("TipoUsuarios")); }
        }
        private ObjectSet<TipoUsuario> _tipoUsuarios;
    
        public ObjectSet<Columna> Columnas
        {
            get { return _columnas  ?? (_columnas = CreateObjectSet<Columna>("Columnas")); }
        }
        private ObjectSet<Columna> _columnas;
    
        public ObjectSet<Comparador> Comparadors
        {
            get { return _comparadors  ?? (_comparadors = CreateObjectSet<Comparador>("Comparadors")); }
        }
        private ObjectSet<Comparador> _comparadors;
    
        public ObjectSet<ReglasSeguridad> ReglasSeguridads
        {
            get { return _reglasSeguridads  ?? (_reglasSeguridads = CreateObjectSet<ReglasSeguridad>("ReglasSeguridads")); }
        }
        private ObjectSet<ReglasSeguridad> _reglasSeguridads;
    
        public ObjectSet<Tabla> Tablas
        {
            get { return _tablas  ?? (_tablas = CreateObjectSet<Tabla>("Tablas")); }
        }
        private ObjectSet<Tabla> _tablas;
    
        public ObjectSet<Tipo> Tipoes
        {
            get { return _tipoes  ?? (_tipoes = CreateObjectSet<Tipo>("Tipoes")); }
        }
        private ObjectSet<Tipo> _tipoes;
    
        public ObjectSet<Usuario> Usuarios
        {
            get { return _usuarios  ?? (_usuarios = CreateObjectSet<Usuario>("Usuarios")); }
        }
        private ObjectSet<Usuario> _usuarios;
    
        public ObjectSet<Ejercicio> Ejercicios
        {
            get { return _ejercicios  ?? (_ejercicios = CreateObjectSet<Ejercicio>("Ejercicios")); }
        }
        private ObjectSet<Ejercicio> _ejercicios;
    
        public ObjectSet<MensajeModeracion> MensajeModeracions
        {
            get { return _mensajeModeracions  ?? (_mensajeModeracions = CreateObjectSet<MensajeModeracion>("MensajeModeracions")); }
        }
        private ObjectSet<MensajeModeracion> _mensajeModeracions;

        #endregion
    }
}
