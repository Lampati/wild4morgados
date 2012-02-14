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
       

        private ModoVisual modo;
        
        FindReplaceMgr findAndReplaceManager = new FindReplaceMgr();

        public EsquemaCentral()
        {
            InitializeComponent();
            this.Modo = ModoVisual.Flujo;
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
}
