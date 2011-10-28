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
    public partial class Localidad
    {
        #region Primitive Properties
    
        public virtual string LocalidadId
        {
            get;
            set;
        }
    
        public virtual string Nombre
        {
            get;
            set;
        }
    
        public virtual string Descripcion
        {
            get;
            set;
        }
    
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

        #endregion
        #region Navigation Properties
    
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

        #endregion
        #region Association Fixup
    
        private void FixupPais(Pais previousValue)
        {
            if (previousValue != null && previousValue.Localidad.Contains(this))
            {
                previousValue.Localidad.Remove(this);
            }
    
            if (Pais != null)
            {
                if (!Pais.Localidad.Contains(this))
                {
                    Pais.Localidad.Add(this);
                }
                if (PaisId != Pais.PaisId)
                {
                    PaisId = Pais.PaisId;
                }
            }
        }
    
        private void FixupProvincia(Provincia previousValue)
        {
            if (previousValue != null && previousValue.Localidad.Contains(this))
            {
                previousValue.Localidad.Remove(this);
            }
    
            if (Provincia != null)
            {
                if (!Provincia.Localidad.Contains(this))
                {
                    Provincia.Localidad.Add(this);
                }
                if (ProvinciaId != Provincia.ProvinciaId)
                {
                    ProvinciaId = Provincia.ProvinciaId;
                }
            }
        }
    
        private void FixupUsuario(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Usuario item in e.NewItems)
                {
                    item.Localidad = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Usuario item in e.OldItems)
                {
                    if (ReferenceEquals(item.Localidad, this))
                    {
                        item.Localidad = null;
                    }
                }
            }
        }

        #endregion
    }
}
