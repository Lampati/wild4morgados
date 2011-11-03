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
    public partial class TipoUsuario
    {
        #region Primitive Properties
    
        public virtual int TipoUsuarioId
        {
            get;
            set;
        }
    
        public virtual string Descripcion
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<Usuario> Usuario
        {
            get
            {
                if (_usuario == null)
                {
                    var newCollection = new FixupCollection<Usuario>();
                    newCollection.CollectionChanged += FixupUsuario;
                    _usuario = newCollection;
                }
                return _usuario;
            }
            set
            {
                if (!ReferenceEquals(_usuario, value))
                {
                    var previousValue = _usuario as FixupCollection<Usuario>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupUsuario;
                    }
                    _usuario = value;
                    var newValue = value as FixupCollection<Usuario>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupUsuario;
                    }
                }
            }
        }
        private ICollection<Usuario> _usuario;
    
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

        #endregion
        #region Association Fixup
    
        private void FixupUsuario(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Usuario item in e.NewItems)
                {
                    item.TipoUsuario = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Usuario item in e.OldItems)
                {
                    if (ReferenceEquals(item.TipoUsuario, this))
                    {
                        item.TipoUsuario = null;
                    }
                }
            }
        }
    
        private void FixupReglasSeguridads(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (ReglasSeguridad item in e.NewItems)
                {
                    item.TipoUsuario = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (ReglasSeguridad item in e.OldItems)
                {
                    if (ReferenceEquals(item.TipoUsuario, this))
                    {
                        item.TipoUsuario = null;
                    }
                }
            }
        }

        #endregion
    }
}
