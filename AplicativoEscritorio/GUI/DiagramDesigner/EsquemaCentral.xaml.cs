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
        #region Hotkeys Definicion
        HotKey hotKeyCompilar;
        HotKey hotKeyEjecutar;
        #endregion

        private ModoVisual modo;
        Compilador compilador;
        FindReplaceMgr findAndReplaceManager = new FindReplaceMgr();

        public EsquemaCentral()
        {
            InitializeComponent();
            this.Modo = ModoVisual.Flujo;
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

            //NO TENGO EL XML!!!
            //ConfigurarCompilador();

            hotKeyCompilar = new HotKey(ModifierKeys.None, Keys.F5, Window.GetWindow(this));
            hotKeyEjecutar = new HotKey(ModifierKeys.None, Keys.F6, Window.GetWindow(this));

            hotKeyCompilar.HotKeyPressed += new Action<HotKey>(hotKeyCompilar_HotKeyPressed);
            hotKeyEjecutar.HotKeyPressed += new Action<HotKey>(hotKeyCompilar_HotKeyPressed);
        }

        private void ConfigurarCompilador()
        {
            bool modoDebug = false;

            string directorioActual = ""; // Application.StartupPath;
            string pathArchGramatica = ""; // System.IO.Path.Combine(directorioActual, System.Configuration.ConfigurationManager.AppSettings["archGramatica"].ToString());
            compilador = new Compilador(pathArchGramatica, modoDebug, directorioActual, directorioActual, "prueba");
        }

        void hotKeyCompilar_HotKeyPressed(HotKey obj)
        {
            switch (obj.Key)
            {
                case Keys.F5:
                    Compilar();
                    break;
                case Keys.F6:
                    Ejecutar();
                    break;

                default:
                    break;
            }

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

        private void Compilar()
        {
            ReiniciarIDEParaCompilacion();

            string programa = this.textEditor.Text;
            ResultadoCompilacion res = this.compilador.Compilar(programa);

            MostrarResultadosCompilacion(res);

            if (!string.IsNullOrEmpty(res.Error))
            {
                MessageBox.Show(res.Error);
            }

        }

        private void Ejecutar()
        {
            ReiniciarIDEParaCompilacion();

            string programa = this.textEditor.Text;
            ResultadoCompilacion res = this.compilador.Compilar(programa);

            MostrarResultadosCompilacion(res);

            if (!string.IsNullOrEmpty(res.Error))
            {
                MessageBox.Show(res.Error);
            }

            if (res.CompilacionGarGarCorrecta && res.GeneracionEjectuableCorrecto)
            {
                EjecucionManager.EjecutarConVentana(res.ArchEjecutableConRuta);
            }
        }

        private void ReiniciarIDEParaCompilacion()
        {
            throw new NotImplementedException();
        }

        private void MostrarResultadosCompilacion(ResultadoCompilacion res)
        {

        }
    }
}
