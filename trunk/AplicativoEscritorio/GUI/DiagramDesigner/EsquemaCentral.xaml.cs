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
using AplicativoEscritorio.ModoTexto.Configuracion;
using CompiladorGargar.Resultado;
using Utilidades;
using CompiladorGargar;
using WpfApplicationHotKey.WinApi;

namespace DiagramDesigner
{
    /// <summary>
    /// Interaction logic for EsquemaCentral.xaml
    /// </summary>
    public partial class EsquemaCentral : UserControl
    {
        public delegate void ModoTextoCambiarPosicionEventHandler(object o, ModoTextoCambiarPosicionEventArgs e);
        public event ModoTextoCambiarPosicionEventHandler ModoTextoCambiarPosicionEvent;

        private ModoVisual modo;
        
        FindReplaceMgr findAndReplaceManager = new FindReplaceMgr();

        public EsquemaCentral()
        {
            InitializeComponent();
            this.Modo = ModoVisual.Flujo;

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

        private void ModoTextoCambiarPosicionEventFire(ModoTextoCambiarPosicionEventArgs e)
        {
            if (ModoTextoCambiarPosicionEvent != null)
            {
                ModoTextoCambiarPosicionEvent(this, e);
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
                        break;
                    case ModoVisual.Texto:
                        this.grdVisual.Visibility = System.Windows.Visibility.Collapsed;
                        this.grdTexto.Visibility = System.Windows.Visibility.Visible;
                        this.InicializarAvalon();
                        break;
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
            ConfigurarBuscarYReemplazarModoTexto();
        }

        private void ConfigurarBuscarYReemplazarModoTexto()
        {
            findAndReplaceManager.CurrentEditor = new FindReplace.TextEditorAdapter(textEditor);
            findAndReplaceManager.ShowSearchIn = false;
            findAndReplaceManager.OwnerWindow = Window.GetWindow(this);


            CommandBindings.Add(findAndReplaceManager.FindBinding);
            CommandBindings.Add(findAndReplaceManager.ReplaceBinding);
            CommandBindings.Add(findAndReplaceManager.FindNextBinding);
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
    }

    public class ModoTextoCambiarPosicionEventArgs
    {
        private int fila;
        public int Fila
        {
            get
            {
                return fila;
            }
        }

        private int columna;
        public int Columna
        {
            get
            {
                return columna;
            }
        }

        public ModoTextoCambiarPosicionEventArgs(int f, int c)
        {
            fila = f;
            columna = c;
        }
    }
}
