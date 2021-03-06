﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using InterfazTextoGrafico;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ModoGrafico.Views
{
  

    /// <summary>
    /// Interaction logic for PropiedadesMetodoDialog.xaml
    /// </summary>
    public partial class PropiedadesTabDialog : Window, INotifyPropertyChanged
    {
        public enum TipoContexto
        {
            Funcion,
            Procedimiento
        }

        ObservableCollection<Parametro> listaParametros = new ObservableCollection<Parametro>();
        ObservableCollection<ParametroArreglo> listaParametrosArreglos = new ObservableCollection<ParametroArreglo>();

        public PropiedadesTabDialog()
        {
            InitializeComponent();

            grdPropiedades.DataContext = this;

            if (cboTipo.Items.Count > 0)
            {
                cboTipo.SelectedIndex = 0;
            }

            if (cboTipoArreglo.Items.Count > 0)
            {
                cboTipoArreglo.SelectedIndex = 0;
            }
        }

        private TipoContexto tipoPropiedades;
        public TipoContexto TipoPropiedades
        {
            get
            {
                return tipoPropiedades;
            }
            set
            {
                tipoPropiedades = value;

                if (tipoPropiedades == TipoContexto.Funcion)
                {
                    grdPropiedades.RowDefinitions.Clear();

                    grdPropiedades.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(25) });
                    grdPropiedades.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(25) });
                    grdPropiedades.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(25) });
                    grdPropiedades.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(25) });

                    Grid.SetRow(lblEncabezado, 0);
                    Grid.SetRow(lblEncabezadoTipo, 0);

                    Grid.SetRow(lblNombre, 1);
                    Grid.SetRow(txtNombre, 1);

                    Grid.SetRow(lblRetorno, 2);
                    Grid.SetRow(txtRetorno, 2);

                    Grid.SetRow(lblTipoRetorno, 3);
                    Grid.SetRow(cboTipoRetorno, 3);

                    lblRetorno.Visibility = System.Windows.Visibility.Visible;
                    lblTipoRetorno.Visibility = System.Windows.Visibility.Visible;
                    txtRetorno.Visibility = System.Windows.Visibility.Visible;
                    cboTipoRetorno.Visibility = System.Windows.Visibility.Visible;

                    lblEncabezadoTipo.Content = "Funcion";
                }
                else
                {
                    Height -= 45;

                    grdPropiedades.RowDefinitions.Clear();

                    grdPropiedades.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(25) });
                    grdPropiedades.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(25) });

                    lblRetorno.Visibility = System.Windows.Visibility.Collapsed;
                    lblTipoRetorno.Visibility = System.Windows.Visibility.Collapsed;
                    txtRetorno.Visibility = System.Windows.Visibility.Collapsed;
                    cboTipoRetorno.Visibility = System.Windows.Visibility.Collapsed;

                    Grid.SetRow(lblEncabezado, 0);
                    Grid.SetRow(lblEncabezadoTipo, 0);

                    Grid.SetRow(lblNombre, 1);
                    Grid.SetRow(txtNombre, 1);

                    lblEncabezadoTipo.Content = "Procedimiento";
                }
              

            }
        }

        private bool nombreReadOnly = false;
        public bool NombreReadOnly
        {
            get
            {
                return nombreReadOnly;
            }
            set
            {
                nombreReadOnly = value;
                txtNombre.IsReadOnly = nombreReadOnly;
            }

        }
     


        private string nombre;
        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;

                if (!string.IsNullOrEmpty(value))
                {
                    NotifyPropertyChanged("Nombre");
                }
            }
        }

        private InterfazTextoGrafico.Enums.TipoDato tipoRetorno;
        public InterfazTextoGrafico.Enums.TipoDato TipoRetorno
        {
            get
            {
                return tipoRetorno;
            }
            set
            {
                tipoRetorno = value;


                NotifyPropertyChanged("TipoRetorno");
            }
        }

        private string retorno;
        public string Retorno
        {
            get
            {
                return retorno;
            }
            set
            {
                retorno = value;

                if (!string.IsNullOrEmpty(value))
                {
                    NotifyPropertyChanged("Retorno");
                }
            }
        }

        private ObservableCollection<Parametro> ParametrosVariables
        {
            get { return listaParametros; }
            set 
            { 
                listaParametros = value;

                dgData.ItemsSource = listaParametros;
                dgData.Items.Refresh();
            }
        }

        private ObservableCollection<ParametroArreglo> ParametrosArreglos
        {
            get { return listaParametrosArreglos; }
            set
            {
                listaParametrosArreglos = value;

                dgDataArreglos.ItemsSource = listaParametrosArreglos;
                dgDataArreglos.Items.Refresh();
            }
        }

        public ObservableCollection<ParametroViewModel> Parametros
        {
            get
            {
                ObservableCollection<ParametroViewModel> listaRetorno = new ObservableCollection<ParametroViewModel>();

                foreach (var item in ParametrosVariables)
                {
                    listaRetorno.Add(new ParametroViewModel() { Nombre = item.Nombre, Tipo = item.Tipo, EsArreglo = false , EsReferencia = item.EsReferencia});
                }

                foreach (var item in ParametrosArreglos)
                {
                    listaRetorno.Add(new ParametroViewModel() { Nombre = item.Nombre, Tipo = item.Tipo, EsArreglo = true, TopeArreglo = item.Tope, EsReferencia = item.EsReferencia });
                }

                return listaRetorno;
            }

            set
            {
                ObservableCollection<Parametro> listaParametroVariable = new ObservableCollection<Parametro>();
                ObservableCollection<ParametroArreglo> listaParametroArreglo = new ObservableCollection<ParametroArreglo>();

                foreach (var item in value as ObservableCollection<ParametroViewModel>)
                {
                    if (item.EsArreglo)
                    {
                        listaParametroArreglo.Add(new ParametroArreglo() { Nombre = item.Nombre, Tipo = item.Tipo, Tope = item.TopeArreglo , EsReferencia = item.EsReferencia});
                    }
                    else
                    {
                        listaParametroVariable.Add(new Parametro() { Nombre = item.Nombre, Tipo = item.Tipo, EsReferencia = item.EsReferencia });
                    }
                }

                ParametrosVariables = listaParametroVariable;
                ParametrosArreglos = listaParametroArreglo;
            }
        }

        #region Create and Update Operation
        private void dgData_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                Parametro paramFila = e.Row.DataContext as Parametro;
                var parametro = (from param in listaParametros 
                                       where param.Id == paramFila.Id 
                                       select param).SingleOrDefault();

                if (parametro == null)
                {
                    listaParametros.Add(parametro);
                }
                else
                {
                    parametro.Nombre = paramFila.Nombre;
                    parametro.Tipo = paramFila.Tipo;
                }
            }
        }

        private void dgDataArreglos_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                ParametroArreglo paramFila = e.Row.DataContext as ParametroArreglo;
                var parametro = (from param in listaParametrosArreglos
                                 where param.Id == paramFila.Id
                                 select param).SingleOrDefault();

                if (parametro == null)
                {
                    listaParametrosArreglos.Add(parametro);
                }
                else
                {
                    parametro.Nombre = paramFila.Nombre;
                    parametro.Tipo = paramFila.Tipo;
                    parametro.Tope = paramFila.Tope;
                }
            }
        }
        #endregion
        
        #region Delete Operation
        private void dgData_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Parametro paramFila = dgData.SelectedItem as Parametro;

            if (paramFila != null)
            {
                
            }
        }

        private void dgDataArreglos_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ParametroArreglo paramFila = dgData.SelectedItem as ParametroArreglo;

            if (paramFila != null)
            {
                
            }
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombreParam.Text) )
            {
                Parametro param = new Parametro();
                param.Nombre = txtNombreParam.Text;

                switch (cboTipo.SelectedValue.ToString().ToUpper().Trim())
                {
                    case "NUMERO":
                        param.Tipo = InterfazTextoGrafico.Enums.TipoDato.Numero;
                        break;
                    case "TEXTO":
                        param.Tipo = InterfazTextoGrafico.Enums.TipoDato.Texto;
                        break;
                    case "BOOLEANO":
                        param.Tipo = InterfazTextoGrafico.Enums.TipoDato.Booleano;
                        break;
                    default:
                        break;
                }


                listaParametros.Add(param);

                ParametrosVariables = listaParametros;

                txtNombreParam.Text = string.Empty;
            }
        }

         private void ButtonArreglos_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombreParamArreglo.Text) && !string.IsNullOrEmpty(txtTopeArreglo.Text))
            {
                ParametroArreglo param = new ParametroArreglo();
                param.Nombre = txtNombreParamArreglo.Text;
                param.Tope = txtTopeArreglo.Text;

                switch (cboTipoArreglo.SelectedValue.ToString().ToUpper().Trim())
                {
                    case "NUMERO":
                        param.Tipo = InterfazTextoGrafico.Enums.TipoDato.Numero;
                        break;
                    case "TEXTO":
                        param.Tipo = InterfazTextoGrafico.Enums.TipoDato.Texto;
                        break;
                    case "BOOLEANO":
                        param.Tipo = InterfazTextoGrafico.Enums.TipoDato.Booleano;
                        break;
                    default:
                        break;
                }

                listaParametrosArreglos.Add(param);

                ParametrosArreglos = listaParametrosArreglos;

                txtNombreParamArreglo.Text = string.Empty;
                txtTopeArreglo.Text = string.Empty;
            }
        } 


        private void DataGridCell_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            if (cell != null && !cell.IsEditing && !cell.IsReadOnly)
            {
                if (!cell.IsFocused)
                {
                    cell.Focus();
                }
                DataGrid dataGrid = FindVisualParent<DataGrid>(cell);
                if (dataGrid != null)
                {
                    if (dataGrid.SelectionUnit != DataGridSelectionUnit.FullRow)
                    {
                        if (!cell.IsSelected)
                            cell.IsSelected = true;
                    }
                    else
                    {
                        DataGridRow row = FindVisualParent<DataGridRow>(cell);
                        if (row != null && !row.IsSelected)
                        {
                            row.IsSelected = true;
                        }
                    }
                }
            }
        }

        static T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            UIElement parent = element;
            while (parent != null)
            {
                T correctlyTyped = parent as T;
                if (correctlyTyped != null)
                {
                    return correctlyTyped;
                }

                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return null;
        }

        private void chkRefArreglo_Click(object sender, RoutedEventArgs e)
        {
            CheckBox chkBox = sender as CheckBox;
            Parametro paramFila = ((FrameworkElement)sender).DataContext as Parametro;

            var parametro = (from param in listaParametros
                             where param.Id == paramFila.Id
                             select param).SingleOrDefault();

            if (parametro != null)
            {
                parametro.EsReferencia = chkBox.IsChecked.HasValue ? chkBox.IsChecked.Value : false;

            }
        }

        private void chkRef_Click(object sender, RoutedEventArgs e)
        {
            CheckBox chkBox = sender as CheckBox;
            Parametro paramFila = ((FrameworkElement)sender).DataContext as Parametro;

            var parametro = (from param in listaParametros
                             where param.Id == paramFila.Id
                             select param).SingleOrDefault();

            if (parametro != null)
            {
                parametro.EsReferencia = chkBox.IsChecked.HasValue ? chkBox.IsChecked.Value : false;

            }
        }


        private void ButtonEliminar_Click(object sender, RoutedEventArgs e)
        {
            Parametro paramFila = ((FrameworkElement)sender).DataContext as Parametro;

            var parametro = (from param in listaParametros
                             where param.Id == paramFila.Id
                             select param).SingleOrDefault();

            if (parametro != null)
            {
                
                listaParametros.Remove(parametro);
                ParametrosVariables = listaParametros;
                

            }
        }

        private void ButtonEliminarArreglo_Click(object sender, RoutedEventArgs e)
        {
            ParametroArreglo paramFila = ((FrameworkElement)sender).DataContext as ParametroArreglo;

            var parametro = (from param in listaParametrosArreglos
                             where param.Id == paramFila.Id
                             select param).SingleOrDefault();

            if (parametro != null)
            {
                
                listaParametrosArreglos.Remove(parametro);
                ParametrosArreglos = listaParametrosArreglos;
                

            }
        }

        public bool Validar( out string mensaje)
        {
            mensaje = "";

            bool valido = true;

            valido &= ValidarNombre();

            if (valido)
            {
                

                if (TipoPropiedades == TipoContexto.Funcion)
                {
                    valido &= cboTipoRetorno.SelectedIndex != -1;

                    if (!valido)
                    {
                        mensaje = "La función debe tener un tipo de retorno";
                    }                
                }

                if (valido)
                {

                    int i = 0;

                    while (i < listaParametros.Count && listaParametros[i].Validar() && listaParametros[i].ValidarNombre())
                    {
                        i++;
                    }
                    valido &= (i >= listaParametros.Count);

                    if (valido)
                    {
                        i = 0;

                        while (i < listaParametrosArreglos.Count && listaParametrosArreglos[i].Validar() && listaParametrosArreglos[i].ValidarNombre())
                        {
                            i++;
                        }
                        valido &= (i >= listaParametrosArreglos.Count);

                        if (!valido)
                        {
                            if (listaParametrosArreglos[i].ValidarNombre())
                            {
                                mensaje = string.Format("El parametro (arreglo) {0} no es valido. Debe tener nombre, tipo y tope definidos", listaParametrosArreglos[i].Nombre);                                
                            }
                            else
                            {
                                mensaje = string.Format("El nombre del parametro {0} no es valido. Debe contener únicamente caracteres alfanumericos", listaParametrosArreglos[i].Nombre);
                            }
                            
                        }
                    }
                    else
                    {
                        if (listaParametros[i].ValidarNombre())
                        {
                            mensaje = string.Format("El parametro (variable) {0} no es valido. Debe tener nombre y tipo definidos", listaParametros[i].Nombre);
                        }
                        else
                        {
                            mensaje = string.Format("El nombre del parametro {0} no es valido. Debe contener únicamente caracteres alfanumericos", listaParametros[i].Nombre);
                            
                        }
                        
                    }
                }
            }
            else
            {
                if (TipoPropiedades == TipoContexto.Funcion)
                {
                    if (Nombre != string.Empty)
                    {
                        mensaje = string.Format("La función {0} debe tener un nombre que contenga solo caracteres alfanumericos", Nombre);
                    }
                    else
                    {
                        mensaje = "La funcion debe tener un nombre que contenga solo caracteres alfanumericos";
                    }
                }
                else
                {
                    if (Nombre != string.Empty)
                    {
                        mensaje = string.Format("El procedimiento {0} debe tener un nombre que contenga solo caracteres alfanumericos", Nombre);
                    }
                    else
                    {
                        mensaje = "El procedimiento debe tener un nombre que contenga solo caracteres alfanumericos";
                    }
                }
            }


            return valido;

        }

        public virtual bool ValidarNombre()
        {
            Regex regex = new Regex(@"^[a-zA-Z]+[a-zA-Z0-9]*$");
            return regex.IsMatch(Nombre);
        }

        private void bttnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }

        private void bttnAceptar_Click(object sender, RoutedEventArgs e)
        {
            string mensaje;
            if (Validar(out mensaje))
            {
                this.DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show(this, mensaje);
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

   
       

      
     


       
    }

    public class Parametro
    {
        static int _contId = 0;
        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }

        }
        public string Nombre { get; set; }

        public bool EsReferencia { get; set; }

        public InterfazTextoGrafico.Enums.TipoDato Tipo {get; set;}

        public Parametro()
        {
            _id = ++_contId;
        }

        public virtual bool Validar()
        {
            return !string.IsNullOrWhiteSpace(Nombre);

        }

        public virtual bool ValidarNombre()
        {
            Regex regex = new Regex(@"^[a-zA-Z]+[a-zA-Z0-9]*$");                                     
            return regex.IsMatch(Nombre) ;
        }
    }

    public class ParametroArreglo : Parametro
    {        
        public string Tope { get; set; }

        public ParametroArreglo() : base()
        {
            
        }

        public override bool Validar()
        {
            return !string.IsNullOrWhiteSpace(Nombre) && !string.IsNullOrWhiteSpace(Tope);

        }
        
    }
}
