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
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;
using System.IO;
using System.Xml;
using System.Windows.Threading;
using ICSharpCode.AvalonEdit.Folding;
using AplicativoEscritorio.ModoTexto.Configuracion.Indentacion;
using FindReplace;
using AplicativoEscritorio.ModoTexto.Configuracion;
using ICSharpCode.AvalonEdit.Document;
using System.Windows.Media.TextFormatting;
using ICSharpCode.AvalonEdit.Rendering;
using WpfApplicationHotKey.WinApi;

namespace AplicativoEscritorio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Hotkeys Definicion

        HotKey hotKeyCompilar;
        HotKey hotKeyEjecutar;

        #endregion


        FindReplaceMgr findAndReplaceManager = new FindReplaceMgr();


        public MainWindow()
        {
            InitializeComponent();

            ConfigurarModoTexto();

            hotKeyCompilar = new HotKey(ModifierKeys.None, Keys.F5, this);
            hotKeyEjecutar = new HotKey(ModifierKeys.None, Keys.F6, this);

            hotKeyCompilar.HotKeyPressed += new Action<HotKey>(hotKeyCompilar_HotKeyPressed);
            hotKeyEjecutar.HotKeyPressed += new Action<HotKey>(hotKeyCompilar_HotKeyPressed);

            //DocumentLine linea = textEditor.Document.GetLineByNumber(1);
            //string t = textEditor.Document.GetText(linea);
            //VisualLine visualLine = new VisualLine(textEditor.TextArea.TextView, linea);


            //VisualLineTextSource textSource = new VisualLineTextSource(visualLine)
            //{
            //    Document = textEditor.Document,                
            //    TextView = textEditor.TextArea.TextView
            //};

            //TextRun ttt = textSource.GetTextRun(linea.Offset);


			
            //visualLine.ConstructVisualElements(textSource, elementGeneratorsArray);

            //textEditor.TextRunProperties.SetForegroundBrush(Brushes.Blue);
            //this.TextRunProperties.SetTextDecorations(TextDecorations.Underline);

            //VisualLineTextSource

            //TextDecoration t = new TextDecoration();
            //t.Location = TextDecorationLocation.Underline;
            
            
        }

        void hotKeyCompilar_HotKeyPressed(HotKey obj)
        {
            switch (obj.Key)
            {             
                case Keys.F5:
                    int i = 1;
                    break;
                case Keys.F6:
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
            findAndReplaceManager.OwnerWindow = this;


            CommandBindings.Add(findAndReplaceManager.FindBinding);
            CommandBindings.Add(findAndReplaceManager.ReplaceBinding);
            CommandBindings.Add(findAndReplaceManager.FindNextBinding);

         
        }
    }
}
