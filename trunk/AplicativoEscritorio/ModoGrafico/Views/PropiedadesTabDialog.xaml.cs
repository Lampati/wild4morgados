using System;
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
                    Grid.SetRow(lblNombre, 0);
                    Grid.SetRow(txtNombre, 0);

                    lblRetorno.Visibility = System.Windows.Visibility.Visible;
                    lblTipoRetorno.Visibility = System.Windows.Visibility.Visible;
                    txtRetorno.Visibility = System.Windows.Visibility.Visible;
                    cboTipoRetorno.Visibility = System.Windows.Visibility.Visible;

                  
                }
                else
                {
                    lblRetorno.Visibility = System.Windows.Visibility.Collapsed;
                    lblTipoRetorno.Visibility = System.Windows.Visibility.Collapsed;
                    txtRetorno.Visibility = System.Windows.Visibility.Collapsed;
                    cboTipoRetorno.Visibility = System.Windows.Visibility.Collapsed;

                    Grid.SetRow(lblNombre, 1);
                    Grid.SetRow(txtNombre, 1);
                }
                //if (tipoPropiedades == TipoContexto.Procedimiento)
                //{
                //    panelParteFuncion.Visibility = System.Windows.Visibility.Hidden;
                //}
                //else
                //{
                //    panelParteFuncion.Visibility = System.Windows.Visibility.Visible;
                //}

            }
        }

      

        //private bool esReadOnly;
        //public bool EsReadOnly
        //{
        //    get
        //    {
        //        return esReadOnly;
        //    }
        //    set
        //    {
        //        esReadOnly = value;

        //        if (esReadOnly)
        //        {
        //            panelAgregarVariable.Visibility = System.Windows.Visibility.Collapsed;
        //            panelAgregarArreglo.Visibility = System.Windows.Visibility.Collapsed;
        //        }
        //        else
        //        {
        //            panelAgregarVariable.Visibility = System.Windows.Visibility.Visible;
        //            panelAgregarArreglo.Visibility = System.Windows.Visibility.Visible;
        //        }

        //        dgData.IsEnabled = !esReadOnly;
        //        dgDataArreglos.IsEnabled = !esReadOnly;
        //        txtNombre.IsEnabled = !esReadOnly;
        //        txtRetorno.IsEnabled = !esReadOnly;
        //        cboTipoRetorno.IsEnabled = !esReadOnly;
             
        //    }
        //}


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

        public List<ParametroViewModel> Parametros
        {
            get
            {
                List<ParametroViewModel> listaRetorno = new List<ParametroViewModel>();

                foreach (var item in ParametrosVariables)
                {
                    listaRetorno.Add(new ParametroViewModel() { Nombre = item.Nombre, Tipo = item.Tipo, EsArreglo = false });
                }

                foreach (var item in ParametrosArreglos)
                {
                    listaRetorno.Add(new ParametroViewModel() { Nombre = item.Nombre, Tipo = item.Tipo, EsArreglo = true, TopeArreglo = item.Tope });
                }

                return listaRetorno;
            }

            set
            {
                ObservableCollection<Parametro> listaParametroVariable = new ObservableCollection<Parametro>();
                ObservableCollection<ParametroArreglo> listaParametroArreglo = new ObservableCollection<ParametroArreglo>();

                foreach (var item in value as List<ParametroViewModel>)
                {
                    if (item.EsArreglo)
                    {
                        listaParametroArreglo.Add(new ParametroArreglo() { Nombre = item.Nombre, Tipo = item.Tipo, Tope = item.TopeArreglo });
                    }
                    else
                    {
                        listaParametroVariable.Add(new Parametro() { Nombre = item.Nombre, Tipo = item.Tipo });
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
        }

         private void ButtonArreglos_Click(object sender, RoutedEventArgs e)
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

        private void ButtonEliminar_Click(object sender, RoutedEventArgs e)
        {
            Parametro paramFila = ((FrameworkElement)sender).DataContext as Parametro;

            var parametro = (from param in listaParametros
                             where param.Id == paramFila.Id
                             select param).SingleOrDefault();

            if (parametro != null)
            {
                if (!(MessageBox.Show("Are You Sure you want to Delete ?",
                    "Confirm Delete !", MessageBoxButton.YesNo) == MessageBoxResult.Yes))
                {
                    e.Handled = true;
                }
                else
                {
                    listaParametros.Remove(parametro);
                    ParametrosVariables = listaParametros;
                }

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
                if (!(MessageBox.Show("Are You Sure you want to Delete ?",
                    "Confirm Delete !", MessageBoxButton.YesNo) == MessageBoxResult.Yes))
                {
                    e.Handled = true;
                }
                else
                {
                    listaParametrosArreglos.Remove(parametro);
                    ParametrosArreglos = listaParametrosArreglos;
                }

            }
        }

        public bool Validar( out string mensaje)
        {
            mensaje = "";

            bool valido = true;

            valido &= !string.IsNullOrWhiteSpace(Nombre);

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

                    while (i < listaParametros.Count && listaParametros[i].Validar())
                    {
                        i++;
                    }
                    valido &= (i >= listaParametros.Count);

                    if (valido)
                    {
                        i = 0;

                        while (i < listaParametrosArreglos.Count && listaParametrosArreglos[i].Validar())
                        {
                            i++;
                        }
                        valido &= (i >= listaParametrosArreglos.Count);

                        if (!valido)
                        {
                            mensaje = string.Format("El parametro (arreglo) {0} no es valido. Debe tener nombre, tipo y tope definidos");
                        }
                    }
                    else
                    {
                        mensaje = string.Format("El parametro (variable) {0} no es valido. Debe tener nombre y tipo definidos");
                    }
                }
            }
            else
            {
                if (TipoPropiedades == TipoContexto.Funcion)
                {
                    mensaje = "La función debe tener un nombre";
                }
                else
                {
                    mensaje = "El procedimiento debe tener un nombre";
                }
            }


            return valido;

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

        public InterfazTextoGrafico.Enums.TipoDato Tipo {get; set;}

        public Parametro()
        {
            _id = ++_contId;
        }

        public virtual bool Validar()
        {
            return !string.IsNullOrWhiteSpace(Nombre);

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
