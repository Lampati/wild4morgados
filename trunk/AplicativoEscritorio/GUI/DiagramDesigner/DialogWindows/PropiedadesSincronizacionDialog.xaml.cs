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
using System.Text.RegularExpressions;

namespace ModoGrafico.Views
{
  

    /// <summary>
    /// Interaction logic for PropiedadesMetodoDialog.xaml
    /// </summary>
    public partial class PropiedadesSincronizacionDialog : Window, INotifyPropertyChanged
    {
        ObservableCollection<Servidor> listaServidores = new ObservableCollection<Servidor>();

        public PropiedadesSincronizacionDialog()
        {
            InitializeComponent();
            listaServidores = new ObservableCollection<Servidor>();
        }

        public ObservableCollection<Servidor> Servidores
        {
            get { return listaServidores; }
            set 
            {
                listaServidores = value;

                dgData.ItemsSource = listaServidores;
                dgData.Items.Refresh();
            }
        }

        private void dgData_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                Servidor paramFila = e.Row.DataContext as Servidor;
                var parametro = (from param in listaServidores 
                                       where param.Id == paramFila.Id 
                                       select param).SingleOrDefault();

                if (parametro == null)
                {
                    listaServidores.Add(parametro);
                }
                else
                {
                    parametro.Ip = paramFila.Ip;
                    parametro.Puerto = paramFila.Puerto;
                }
            }
        }
     
        private void dgData_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Servidor paramFila = dgData.SelectedItem as Servidor;

            if (paramFila != null)
            {
                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Servidor server = new Servidor();
            server.Ip = txtIP.Text;
            server.Puerto = txtPuerto.Text;

            if (server.ValidarIpCorrecta())
            {
                if (server.ValidarPuertoCorrecto())
                {
                    listaServidores.Add(server);

                    Servidores = listaServidores;

                    txtIP.Text = string.Empty;
                    txtPuerto.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("El puerto que se intenta ingresar es incorrecto");
                }
            }
            else
            {
                MessageBox.Show("El IP que se intenta ingresar es incorrecto");
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

        private void ButtonEliminar_Click(object sender, RoutedEventArgs e)
        {
            Servidor paramFila = ((FrameworkElement)sender).DataContext as Servidor;

            var parametro = (from param in listaServidores
                             where param.Id == paramFila.Id
                             select param).SingleOrDefault();

            if (parametro != null)
            {

                listaServidores.Remove(parametro);
                Servidores = listaServidores;
            }
        }

        
        public bool Validar( out string mensaje)
        {
            mensaje = "";
            bool valido = true;

            int i = 0;

            while (i < listaServidores.Count && listaServidores[i].Validar())
            {
                i++;
            }
            valido &= (i >= listaServidores.Count);

            if (!valido)
            {
                mensaje = string.Format("El servidor {0} no es valido. Debe tener IP y puerto definidos", i);
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
        


        internal void CargarServidores(string servidores)
        {
            try
            {
                if (!string.IsNullOrEmpty(servidores))
                {
                    foreach (string item in servidores.Split(new string[] { "\r\n"}, StringSplitOptions.RemoveEmptyEntries))
                    {
                        //string[] entrada = item.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

                        string url = item.Substring(0, item.LastIndexOf(':'));
                        string puerto = item.Substring(item.LastIndexOf(':') + 1);

                        //listaServidores.Add(new Servidor() { Ip = entrada[0], Puerto = entrada[1] });
                        listaServidores.Add(new Servidor() { Ip = url, Puerto = puerto });
                    }

                    dgData.ItemsSource = listaServidores;
                    dgData.Items.Refresh();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha producido un error al cargar los servidores desde el archivo de configuración");
            }
        }
    }

    public class Servidor
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
        public string Ip { get; set; }
        public string Puerto { get; set; }


        public Servidor()
        {
            _id = ++_contId;
        }

        public virtual bool ValidarIpCorrecta()
        {
            return !string.IsNullOrWhiteSpace(Ip) ;
        }

        public virtual bool ValidarPuertoCorrecto()
        {
            Regex regex = new Regex(@"^[0-9]+$");
            
            return !string.IsNullOrWhiteSpace(Puerto) && regex.IsMatch(Puerto);
        }

        internal bool Validar()
        {
            return ValidarIpCorrecta() && ValidarPuertoCorrecto();
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Ip, Puerto);
        }
    }

   
}
