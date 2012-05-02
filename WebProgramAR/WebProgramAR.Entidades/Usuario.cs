//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace WebProgramAR.Entidades
{
    public partial class Usuario
    {
        #region Primitive Properties
    
        public virtual int UsuarioId
        {
            get;
            set;
        }
    
        public virtual string Nombre
        {
            get;
            set;
        }
    
        public virtual string Apellido
        {
            get;
            set;
        }
    
        public virtual System.DateTime FechaNacimiento
        {
            get;
            set;
        }
    
        public virtual System.DateTime FechaAlta
        {
            get;
            set;
        }
    
        public virtual string UsuarioNombre
        {
            get;
            set;
        }
    
        public virtual int TipoUsuarioId
        {
            get { return _tipoUsuarioId; }
            set
            {
                if (_tipoUsuarioId != value)
                {
                    if (TipoUsuario != null && TipoUsuario.TipoUsuarioId != value)
                    {
                        TipoUsuario = null;
                    }
                    _tipoUsuarioId = value;
                }
            }
        }
        private int _tipoUsuarioId;
    
        public virtual string PaisId
        {
            get { return _paisId; }
            set
            {
                if (_paisId != value)
                {
                    if (Pais != null && Pais.PaisId != value)
                    {
                        Pais = null;
                    }
                    _paisId = value;
                }
            }
        }
        private string _paisId;
    
        public virtual string ProvinciaId
        {
            get { return _provinciaId; }
            set
            {
                if (_provinciaId != value)
                {
                    if (Provincia != null && Provincia.ProvinciaId != value)
                    {
                        Provincia = null;
                    }
                    _provinciaId = value;
                }
            }
        }
        private string _provinciaId;
    
        public virtual string LocalidadId
        {
            get { return _localidadId; }
            set
            {
                if (_localidadId != value)
                {
                    if (Localidad != null && Localidad.LocalidadId != value)
                    {
                        Localidad = null;
                    }
                    _localidadId = value;
                }
            }
        }
        private string _localidadId;

        #endregion
        #region Navigation Properties
    
        public virtual Localidad Localidad
        {
            get { return _localidad; }
            set
            {
                if (!ReferenceEquals(_localidad, value))
                {
                    var previousValue = _localidad;
                    _localidad = value;
                    FixupLocalidad(previousValue);
                }
            }
        }
        private Localidad _localidad;
    
        public virtual Pais Pais
        {
            get { return _pais; }
            set
            {
                if (!ReferenceEquals(_pais, value))
                {
                    var previousValue = _pais;
                    _pais = value;
                    FixupPais(previousValue);
                }
            }
        }
        private Pais _pais;
    
        public virtual Provincia Provincia
        {
            get { return _provincia; }
            set
            {
                if (!ReferenceEquals(_provincia, value))
                {
                    var previousValue = _provincia;
                    _provincia = value;
                    FixupProvincia(previousValue);
                }
            }
        }
        private Provincia _provincia;
    
        public virtual ICollection<ReglasSeguridad> ReglasSeguridads
        {
            get
            {
                if (_reglasSeguridads == null)
                {
                    var newCollection = new FixupCollection<ReglasSeguridad>();
                    newCollection.CollectionChanged += FixupReglasSeguridads;
                    _reglasSeguridads = newCollection;
                }
                return _reglasSeguridads;
            }
            set
            {
                if (!ReferenceEquals(_reglasSeguridads, value))
                {
                    var previousValue = _reglasSeguridads as FixupCollection<ReglasSeguridad>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupReglasSeguridads;
                    }
                    _reglasSeguridads = value;
                    var newValue = value as FixupCollection<ReglasSeguridad>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupReglasSeguridads;
                    }
                }
            }
        }
        private ICollection<ReglasSeguridad> _reglasSeguridads;
    
        public virtual TipoUsuario TipoUsuario
        {
            get { return _tipoUsuario; }
            set
            {
                if (!ReferenceEquals(_tipoUsuario, value))
                {
                    var previousValue = _tipoUsuario;
                    _tipoUsuario = value;
                    FixupTipoUsuario(previousValue);
                }
            }
        }
        private TipoUsuario _tipoUsuario;
    
        public virtual ICollection<Curso> Cursoes
        {
            get
            {
                if (_cursoes == null)
                {
                    var newCollection = new FixupCollection<Curso>();
                    newCollection.CollectionChanged += FixupCursoes;
                    _cursoes = newCollection;
                }
                return _cursoes;
            }
            set
            {
                if (!ReferenceEquals(_cursoes, value))
                {
                    var previousValue = _cursoes as FixupCollection<Curso>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupCursoes;
                    }
                    _cursoes = value;
                    var newValue = value as FixupCollection<Curso>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupCursoes;
                    }
                }
            }
        }
        private ICollection<Curso> _cursoes;
    
        public virtual ICollection<Ejercicio> Ejercicios
        {
            get
            {
                if (_ejercicios == null)
                {
                    var newCollection = new FixupCollection<Ejercicio>();
                    newCollection.CollectionChanged += FixupEjercicios;
                    _ejercicios = newCollection;
                }
                return _ejercicios;
            }
            set
            {
                if (!ReferenceEquals(_ejercicios, value))
                {
                    var previousValue = _ejercicios as FixupCollection<Ejercicio>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupEjercicios;
                    }
                    _ejercicios = value;
                    var newValue = value as FixupCollection<Ejercicio>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupEjercicios;
                    }
                }
            }
        }
        private ICollection<Ejercicio> _ejercicios;

        #endregion
        #region Association Fixup
    
        private void FixupLocalidad(Localidad previousValue)
        {
            if (previousValue != null && previousValue.Usuarios.Contains(this))
            {
                previousValue.Usuarios.Remove(this);
            }
    
            if (Localidad != null)
            {
                if (!Localidad.Usuarios.Contains(this))
                {
                    Localidad.Usuarios.Add(this);
                }
                if (LocalidadId != Localidad.LocalidadId)
                {
                    LocalidadId = Localidad.LocalidadId;
                }
            }
        }
    
        private void FixupPais(Pais previousValue)
        {
            if (previousValue != null && previousValue.Usuarios.Contains(this))
            {
                previousValue.Usuarios.Remove(this);
            }
    
            if (Pais != null)
            {
                if (!Pais.Usuarios.Contains(this))
                {
                    Pais.Usuarios.Add(this);
                }
                if (PaisId != Pais.PaisId)
                {
                    PaisId = Pais.PaisId;
                }
            }
        }
    
        private void FixupProvincia(Provincia previousValue)
        {
            if (previousValue != null && previousValue.Usuarios.Contains(this))
            {
                previousValue.Usuarios.Remove(this);
            }
    
            if (Provincia != null)
            {
                if (!Provincia.Usuarios.Contains(this))
                {
                    Provincia.Usuarios.Add(this);
                }
                if (ProvinciaId != Provincia.ProvinciaId)
                {
                    ProvinciaId = Provincia.ProvinciaId;
                }
            }
        }
    
        private void FixupTipoUsuario(TipoUsuario previousValue)
        {
            if (previousValue != null && previousValue.Usuarios.Contains(this))
            {
                previousValue.Usuarios.Remove(this);
            }
    
            if (TipoUsuario != null)
            {
                if (!TipoUsuario.Usuarios.Contains(this))
                {
                    TipoUsuario.Usuarios.Add(this);
                }
                if (TipoUsuarioId != TipoUsuario.TipoUsuarioId)
                {
                    TipoUsuarioId = TipoUsuario.TipoUsuarioId;
                }
            }
        }
    
        private void FixupReglasSeguridads(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (ReglasSeguridad item in e.NewItems)
                {
                    item.Usuario = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (ReglasSeguridad item in e.OldItems)
                {
                    if (ReferenceEquals(item.Usuario, this))
                    {
                        item.Usuario = null;
                    }
                }
            }
        }
    
        private void FixupCursoes(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Curso item in e.NewItems)
                {
                    item.Usuario = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Curso item in e.OldItems)
                {
                    if (ReferenceEquals(item.Usuario, this))
                    {
                        item.Usuario = null;
                    }
                }
            }
        }
    
        private void FixupEjercicios(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Ejercicio item in e.NewItems)
                {
                    item.Usuario = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Ejercicio item in e.OldItems)
                {
                    if (ReferenceEquals(item.Usuario, this))
                    {
                        item.Usuario = null;
                    }
                }
            }
        }

        #endregion
    }
}
