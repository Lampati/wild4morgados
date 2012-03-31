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
    public partial class Comparador
    {
        #region Primitive Properties
    
        public virtual int ComparadorId
        {
            get;
            set;
        }
    
        public virtual string Nombre
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
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
    
        public virtual ICollection<Tipo> Tipoes
        {
            get
            {
                if (_tipoes == null)
                {
                    var newCollection = new FixupCollection<Tipo>();
                    newCollection.CollectionChanged += FixupTipoes;
                    _tipoes = newCollection;
                }
                return _tipoes;
            }
            set
            {
                if (!ReferenceEquals(_tipoes, value))
                {
                    var previousValue = _tipoes as FixupCollection<Tipo>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupTipoes;
                    }
                    _tipoes = value;
                    var newValue = value as FixupCollection<Tipo>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupTipoes;
                    }
                }
            }
        }
        private ICollection<Tipo> _tipoes;

        #endregion
        #region Association Fixup
    
        private void FixupReglasSeguridads(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (ReglasSeguridad item in e.NewItems)
                {
                    item.Comparador = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (ReglasSeguridad item in e.OldItems)
                {
                    if (ReferenceEquals(item.Comparador, this))
                    {
                        item.Comparador = null;
                    }
                }
            }
        }
    
        private void FixupTipoes(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Tipo item in e.NewItems)
                {
                    if (!item.Comparadors.Contains(this))
                    {
                        item.Comparadors.Add(this);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Tipo item in e.OldItems)
                {
                    if (item.Comparadors.Contains(this))
                    {
                        item.Comparadors.Remove(this);
                    }
                }
            }
        }

        #endregion
    }
}
