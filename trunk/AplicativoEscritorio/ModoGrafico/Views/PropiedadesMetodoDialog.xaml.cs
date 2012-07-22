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

namespace ModoGrafico.Views
{
    internal class Objetin
    {
        internal int Index { get; set; }
        internal StackPanel SP { get; set; }
        internal RowDefinition RD { get; set; }

        internal Objetin(int ix, StackPanel sp, RowDefinition rd)
        {
            this.Index = ix;
            this.SP = sp;
            this.RD = rd;
        }
    }

    /// <summary>
    /// Interaction logic for PropiedadesMetodoDialog.xaml
    /// </summary>
    public partial class PropiedadesMetodoDialog : Window, ModoGrafico.Interfaces.IPropiedadesContexto
    {
        private int cont = 4;
        //private System.Collections.Hashtable htBotones;

        public PropiedadesMetodoDialog()
        {
            InitializeComponent();
            foreach (string s in Enum.GetNames(typeof(InterfazTextoGrafico.Enums.TipoDato)))
            {
                this.cboTipoRetorno.Items.Add(s);
                this.cboTipo.Items.Add(s);
            }
            //this.htBotones = new System.Collections.Hashtable();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            TextBox txt = new TextBox();
            txt.Height = 24;
            txt.Width = 100;
            txt.Margin = new Thickness(2, 0, 0, 0);
            txt.Name = String.Format("txt_{0}", cont.ToString());

            ComboBox cbo = new ComboBox();
            cbo.Height = 24;
            cbo.Width = 70;
            cbo.Margin = new Thickness(2, 0, 0, 0);
            cbo.Name = String.Format("cbo_{0}", cont.ToString());
            foreach (string s in Enum.GetNames(typeof(InterfazTextoGrafico.Enums.TipoDato)))
                cbo.Items.Add(s);

            Button btnQuitar = new Button();
            btnQuitar.Margin = new Thickness(2, 0, 0, 0);
            btnQuitar.Width = 30;
            btnQuitar.Content = "-";
            btnQuitar.Click += new RoutedEventHandler(btnQuitar_Click);
            btnQuitar.Name = String.Format("btn1_{0}", cont.ToString());            

            RowDefinition rd = new RowDefinition();
            rd.Height = new GridLength(25);
            this.grdPropiedades.RowDefinitions.Add(rd);
            int rowIndex = this.grdPropiedades.RowDefinitions.Count - 1;
            
            
            //this.htBotones.Add(rowIndex, btnQuitar);

            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            sp.Children.Add(txt);
            sp.Children.Add(cbo);
            sp.Children.Add(btnQuitar);
            Grid.SetRow(sp, rowIndex);
            Grid.SetColumn(sp, 1);
            this.grdPropiedades.Children.Add(sp);

            Objetin o = new Objetin(rowIndex, sp, rd);
            btnQuitar.Tag = o;
            /*Grid.SetRow(txt, rowIndex);
            Grid.SetColumn(txt, 1);
            this.grdPropiedades.Children.Add(txt);
            Grid.SetRow(cbo, rowIndex);
            Grid.SetColumn(cbo, 1);
            this.grdPropiedades.Children.Add(cbo);
            Grid.SetRow(btnAgregar, rowIndex);
            Grid.SetColumn(btnAgregar, 1);
            this.grdPropiedades.Children.Add(btnAgregar);*/
            cont++;
        }

        void btnQuitar_Click(object sender, RoutedEventArgs e)
        {            
            Control button = (Control)sender;
            Objetin rd = (Objetin)button.Tag;

            rd.SP.Children.Clear();
            this.grdPropiedades.Children.Remove(rd.SP);
            this.grdPropiedades.RowDefinitions.Remove(rd.RD);
            this.grdPropiedades.InvalidateArrange();
            
            /*int ix = (int)button.Tag;
            this.grdPropiedades.RowDefinitions.RemoveAt(ix);
            this.htBotones.Remove(ix);
            if (ix < this.grdPropiedades.RowDefinitions.Count - 1)
            {
                foreach (int el in new System.Collections.ArrayList(this.htBotones.Keys))
                    if (el > ix)
                    {
                        FrameworkElement fe = (FrameworkElement)this.htBotones[el];
                        fe.Tag = (int)fe.Tag - 1;
                        this.htBotones.Remove(el);
                        this.htBotones.Add(el - 1, fe);
                    }
            }*/
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

        public ObservableCollection<InterfazTextoGrafico.ParametroViewModel> Parametros
        {
            get { return null; }
            set { }
        }
    }
}
