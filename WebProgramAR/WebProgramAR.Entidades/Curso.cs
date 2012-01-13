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
    public partial class Curso
    {
        #region Primitive Properties
    
        public virtual int CursoId
        {
            get;
            set;
        }
    
        public virtual string Nombre
        {
            get;
            set;
        }
    
        public virtual System.DateTime FechaAlta
        {
            get;
            set;
        }
    
        public virtual int UsuarioId
        {
            get { return _usuarioId; }
            set
            {
                if (_usuarioId != value)
                {
                    if (Usuario != null && Usuario.UsuarioId != value)
                    {
                        Usuario = null;
                    }
                    _usuarioId = value;
                }
            }
        }
        private int _usuarioId;

        #endregion
        #region Navigation Properties
    
        public virtual Usuario Usuario
        {
            get { return _usuario; }
            set
            {
                if (!ReferenceEquals(_usuario, value))
                {
                    var previousValue = _usuario;
                    _usuario = value;
                    FixupUsuario(previousValue);
                }
            }
        }
        private Usuario _usuario;
    
        public virtual ICollection<Ejercicio> Ejercicio
        {
            get
            {
                if (_ejercicio == null)
                {
                    var newCollection = new FixupCollection<Ejercicio>();
                    newCollection.CollectionChanged += FixupEjercicio;
                    _ejercicio = newCollection;
                }
                return _ejercicio;
            }
            set
            {
                if (!ReferenceEquals(_ejercicio, value))
                {
                    var previousValue = _ejercicio as FixupCollection<Ejercicio>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupEjercicio;
                    }
                    _ejercicio = value;
                    var newValue = value as FixupCollection<Ejercicio>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupEjercicio;
                    }
                }
            }
        }
        private ICollection<Ejercicio> _ejercicio;

        #endregion
        #region Association Fixup
    
        private void FixupUsuario(Usuario previousValue)
        {
            if (previousValue != null && previousValue.Curso.Contains(this))
            {
                previousValue.Curso.Remove(this);
            }
    
            if (Usuario != null)
            {
                if (!Usuario.Curso.Contains(this))
                {
                    Usuario.Curso.Add(this);
                }
                if (UsuarioId != Usuario.UsuarioId)
                {
                    UsuarioId = Usuario.UsuarioId;
                }
            }
        }
    
        private void FixupEjercicio(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Ejercicio item in e.NewItems)
                {
                    if (!item.Curso.Contains(this))
                    {
                        item.Curso.Add(this);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Ejercicio item in e.OldItems)
                {
                    if (item.Curso.Contains(this))
                    {
                        item.Curso.Remove(this);
                    }
                }
            }
        }

        #endregion
    }
}