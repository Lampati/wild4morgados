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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DiagramDesigner.Enums;
using FindReplace;
using DiagramDesigner.ModoTexto.Configuracion;
using CompiladorGargar.Resultado;
using Utilidades;
using CompiladorGargar;
using WpfApplicationHotKey.WinApi;
using DiagramDesigner.EventArgsClasses;
using DataAccess.Interfases;
using DataAccess.Entidades;
using Globales.Enums;


namespace DiagramDesigner.UserControls.Entorno
{
    /// <summary>
    /// Interaction logic for EsquemaCentral.xaml
    /// </summary>
    public partial class EsquemaCentral : UserControl
    {
        


        private ModoVisual modo;

        FindReplaceMgr findAndReplaceManager;

        public EsquemaCentral()
        {
            InitializeComponent();

            this.InicializarAvalon();

            this.textEditor.TextArea.Caret.PositionChanged += new EventHandler(Caret_PositionChanged);
        }

        void Caret_PositionChanged(object sender, EventArgs e)
        {
            if (this.textEditor.TextArea.Caret != null)
            {
                ModoTextoCambiarPosicionEventFire(
                    new ModoTextoCambiarPosicionEventArgs(
                        this.textEditor.TextArea.Caret.Line, 
                        this.textEditor.TextArea.Caret.Column)
                        );
            }
        }

      

        public string GarGarACompilar
        {
            get
            {
                if (Modo == ModoVisual.Texto)
                {
                    return this.textEditor.Text;
                }
                else
                {
                    return this.textEditor.Text;
                }
            }
        }

        public ModoVisual Modo
        {
            get { return this.modo; }
            set
            {                
                this.modo = value;
                switch (this.modo)
                {
                    case ModoVisual.Flujo:
                        this.grdVisual.Visibility = System.Windows.Visibility.Visible;
                        this.grdTexto.Visibility = System.Windows.Visibility.Collapsed;
                        DesconfigurarBuscarYReemplazarModoTexto();
                        MyDesigner.Focus();                        
                        break;
                    case ModoVisual.Texto:
                        this.grdVisual.Visibility = System.Windows.Visibility.Collapsed;
                        this.grdTexto.Visibility = System.Windows.Visibility.Visible;
                        ConfigurarBuscarYReemplazarModoTexto();
                        textEditor.TextArea.Focus();
                        break;
                }
            }
        }

        private EntidadBase archCargado = null;
        public EntidadBase ArchCargado
        {
            get { return archCargado; }
            set
            {
                archCargado = value;

                if (archCargado == null)
                {
                    this.grdVisual.Visibility = System.Windows.Visibility.Hidden;
                    this.grdTexto.Visibility = System.Windows.Visibility.Collapsed;
                    DesconfigurarBuscarYReemplazarModoTexto();
                }
                else
                {
                    textEditor.Text = archCargado.Gargar;
                }
            }
        }

        private void InicializarAvalon()
        {
            InitializeComponent();

            ConfigurarModoTexto();

         
        }

        private void ConfigurarModoTexto()
        {
            ModoTextoConfiguracion.ConfigurarAvalonEdit(textEditor);
            textEditor.TextChanged += new EventHandler(textEditor_TextChanged);
            ConfigurarBuscarYReemplazarModoTexto();
        }

        void textEditor_TextChanged(object sender, EventArgs e)
        {
            if (ArchCargado != null)
            {
                ArchCargado.ModificadoDesdeUltimoGuardado = true;
                ArchCargado.Gargar = textEditor.Text;
            }
        }

        public void ConfigurarBuscarYReemplazarModoTexto()
        {
            findAndReplaceManager = new FindReplaceMgr();

            findAndReplaceManager.CurrentEditor = new FindReplace.TextEditorAdapter(textEditor);
            findAndReplaceManager.ShowSearchIn = false;
            findAndReplaceManager.OwnerWindow = Window.GetWindow(this);

            CommandBindings.Add(findAndReplaceManager.FindBinding);
            CommandBindings.Add(findAndReplaceManager.ReplaceBinding);
            CommandBindings.Add(findAndReplaceManager.FindNextBinding);
        }

        public void DesconfigurarBuscarYReemplazarModoTexto()
        {
            if (findAndReplaceManager != null)
            {
                findAndReplaceManager.CurrentEditor = null;
                findAndReplaceManager.ShowSearchIn = false;
                findAndReplaceManager.OwnerWindow = null;

                CommandBindings.Clear();

                findAndReplaceManager = null;
            }
        }


        internal void PosicionarseEn(int fila, int col)
        {
            if (Modo == ModoVisual.Texto)
            {
                this.textEditor.TextArea.Caret.Line = fila;
                this.textEditor.TextArea.Caret.Column = col;
                this.textEditor.TextArea.Caret.Show();
                this.textEditor.TextArea.Caret.BringCaretToView();
            }
        }

        internal void MostrarBuscar(bool esBusqYReemp)
        {
            if (esBusqYReemp)
            {
                findAndReplaceManager.ShowAsReplace();
            }
            else
            {
                findAndReplaceManager.ShowAsFind();
            }
        }
    }

    
}
