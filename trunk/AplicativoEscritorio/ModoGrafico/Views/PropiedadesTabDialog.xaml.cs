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

namespace ModoGrafico.Views
{
  

    /// <summary>
    /// Interaction logic for PropiedadesMetodoDialog.xaml
    /// </summary>
    public partial class PropiedadesTabDialog : Window
    {
        ObservableCollection<Parametro> listaParametros = new ObservableCollection<Parametro>();
        ObservableCollection<ParametroArreglo> listaParametrosArreglos = new ObservableCollection<ParametroArreglo>();

        private int cont = 4;
        //private System.Collections.Hashtable htBotones;

        public PropiedadesTabDialog()
        {
            InitializeComponent();
            foreach (string s in Enum.GetNames(typeof(InterfazTextoGrafico.Enums.TipoDato)))
            {
                this.cboTipoRetorno.Items.Add(s);
            }
            //this.htBotones = new System.Collections.Hashtable();
        }

       

        public string Nombre
        {
            get { return this.txtNombre.Text; }
            set { this.txtNombre.Text = value; }
        }

       

        public InterfazTextoGrafico.Enums.TipoDato TipoRetorno
        {
            get
            {
                return (InterfazTextoGrafico.Enums.TipoDato)Enum.Parse(typeof(InterfazTextoGrafico.Enums.TipoDato),
                    this.cboTipoRetorno.SelectedValue.ToString());
            }
            set { }
        }

        public string Retorno
        {
            get { return this.txtRetorno.Text; }
            set { this.txtRetorno.Text = value; }
        }

        public ObservableCollection<Parametro> Parametros
        {
            get { return listaParametros; }
            set 
            { 
                listaParametros = value;

                dgData.ItemsSource = listaParametros;
                dgData.Items.Refresh();
            }
        }

        public ObservableCollection<ParametroArreglo> ParametrosArreglos
        {
            get { return listaParametrosArreglos; }
            set
            {
                listaParametrosArreglos = value;

                dgDataArreglos.ItemsSource = listaParametrosArreglos;
                dgDataArreglos.Items.Refresh();
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

            listaParametros.Add(param);

            Parametros = listaParametros;
        }

         private void ButtonArreglos_Click(object sender, RoutedEventArgs e)
        {
            ParametroArreglo param = new ParametroArreglo();
            param.Nombre = txtNombreParamArreglo.Text;
            param.Tope = txtTopeArreglo.Text;

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
                    Parametros = listaParametros;
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

    }

    public class ParametroArreglo
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
        public string Tope { get; set; }

        public InterfazTextoGrafico.Enums.TipoDato Tipo { get; set; }

        public ParametroArreglo()
        {
            _id = ++_contId;
        }

    }
}
