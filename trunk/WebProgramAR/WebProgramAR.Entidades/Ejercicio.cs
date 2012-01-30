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
    public partial class Ejercicio
    {
        #region Primitive Properties
    
        public virtual int EjercicioId
        {
            get;
            set;
        }
    
        public virtual string Nombre
        {
            get;
            set;
        }
    
        public virtual string Enunciado
        {
            get;
            set;
        }
    
        public virtual string SolucionTexto
        {
            get;
            set;
        }
    
        public virtual byte[] Solucion
        {
            get;
            set;
        }
    
        public virtual bool Global
        {
            get;
            set;
        }
    
        public virtual System.DateTime FechaAlta
        {
            get;
            set;
        }
    
        public virtual int NivelEjercicioId
        {
            get { return _nivelEjercicioId; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_nivelEjercicioId != value)
                    {
                        if (NivelEjercicio != null && NivelEjercicio.NivelEjercicioId != value)
                        {
                            NivelEjercicio = null;
                        }
                        _nivelEjercicioId = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private int _nivelEjercicioId;
    
        public virtual Nullable<int> UsuarioId
        {
            get { return _usuarioId; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_usuarioId != value)
                    {
                        if (Usuario != null && Usuario.UsuarioId != value)
                        {
                            Usuario = null;
                        }
                        _usuarioId = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<int> _usuarioId;
    
        public virtual int EstadoEjercicioId
        {
            get { return _estadoEjercicioId; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_estadoEjercicioId != value)
                    {
                        if (EstadoEjercicio != null && EstadoEjercicio.EstadoEjercicioId != value)
                        {
                            EstadoEjercicio = null;
                        }
                        _estadoEjercicioId = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private int _estadoEjercicioId;

        #endregion
        #region Navigation Properties
    
        public virtual EstadoEjercicio EstadoEjercicio
        {
            get { return _estadoEjercicio; }
            set
            {
                if (!ReferenceEquals(_estadoEjercicio, value))
                {
                    var previousValue = _estadoEjercicio;
                    _estadoEjercicio = value;
                    FixupEstadoEjercicio(previousValue);
                }
            }
        }
        private EstadoEjercicio _estadoEjercicio;
    
        public virtual NivelEjercicio NivelEjercicio
        {
            get { return _nivelEjercicio; }
            set
            {
                if (!ReferenceEquals(_nivelEjercicio, value))
                {
                    var previousValue = _nivelEjercicio;
                    _nivelEjercicio = value;
                    FixupNivelEjercicio(previousValue);
                }
            }
        }
        private NivelEjercicio _nivelEjercicio;
    
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
    
        public virtual MensajeModeracion MensajeModeracion
        {
            get { return _mensajeModeracion; }
            set
            {
                if (!ReferenceEquals(_mensajeModeracion, value))
                {
                    var previousValue = _mensajeModeracion;
                    _mensajeModeracion = value;
                    FixupMensajeModeracion(previousValue);
                }
            }
        }
        private MensajeModeracion _mensajeModeracion;

        #endregion
        #region Association Fixup
    
        private bool _settingFK = false;
    
        private void FixupEstadoEjercicio(EstadoEjercicio previousValue)
        {
            if (previousValue != null && previousValue.Ejercicios.Contains(this))
            {
                previousValue.Ejercicios.Remove(this);
            }
    
            if (EstadoEjercicio != null)
            {
                if (!EstadoEjercicio.Ejercicios.Contains(this))
                {
                    EstadoEjercicio.Ejercicios.Add(this);
                }
                if (EstadoEjercicioId != EstadoEjercicio.EstadoEjercicioId)
                {
                    EstadoEjercicioId = EstadoEjercicio.EstadoEjercicioId;
                }
            }
        }
    
        private void FixupNivelEjercicio(NivelEjercicio previousValue)
        {
            if (previousValue != null && previousValue.Ejercicios.Contains(this))
            {
                previousValue.Ejercicios.Remove(this);
            }
    
            if (NivelEjercicio != null)
            {
                if (!NivelEjercicio.Ejercicios.Contains(this))
                {
                    NivelEjercicio.Ejercicios.Add(this);
                }
                if (NivelEjercicioId != NivelEjercicio.NivelEjercicioId)
                {
                    NivelEjercicioId = NivelEjercicio.NivelEjercicioId;
                }
            }
        }
    
        private void FixupUsuario(Usuario previousValue)
        {
            if (previousValue != null && previousValue.Ejercicios.Contains(this))
            {
                previousValue.Ejercicios.Remove(this);
            }
    
            if (Usuario != null)
            {
                if (!Usuario.Ejercicios.Contains(this))
                {
                    Usuario.Ejercicios.Add(this);
                }
                if (UsuarioId != Usuario.UsuarioId)
                {
                    UsuarioId = Usuario.UsuarioId;
                }
            }
            else if (!_settingFK)
            {
                UsuarioId = null;
            }
        }
    
        private void FixupMensajeModeracion(MensajeModeracion previousValue)
        {
            if (previousValue != null && ReferenceEquals(previousValue.Ejercicio, this))
            {
                previousValue.Ejercicio = null;
            }
    
            if (MensajeModeracion != null)
            {
                MensajeModeracion.Ejercicio = this;
            }
        }
    
        private void FixupCursoes(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Curso item in e.NewItems)
                {
                    if (!item.Ejercicios.Contains(this))
                    {
                        item.Ejercicios.Add(this);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Curso item in e.OldItems)
                {
                    if (item.Ejercicios.Contains(this))
                    {
                        item.Ejercicios.Remove(this);
                    }
                }
            }
        }

        #endregion
    }
}
