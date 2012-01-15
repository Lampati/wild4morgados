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

namespace AplicativoEscritorio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FindReplaceMgr findAndReplaceManager = new FindReplaceMgr();

        public MainWindow()
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
            findAndReplaceManager.OwnerWindow = this;


            CommandBindings.Add(findAndReplaceManager.FindBinding);
            CommandBindings.Add(findAndReplaceManager.ReplaceBinding);
            CommandBindings.Add(findAndReplaceManager.FindNextBinding);

         
        }

       

    

        #region Folding
        FoldingManager foldingManager;
        AbstractFoldingStrategy foldingStrategy;

        void HighlightingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (textEditor.SyntaxHighlighting == null)
            {
                foldingStrategy = null;
            }
            else
            {
                switch (textEditor.SyntaxHighlighting.Name)
                {
                    case "XML":
                        foldingStrategy = new XmlFoldingStrategy();
                        textEditor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.DefaultIndentationStrategy();
                        break;
                    case "C#":
                    case "C++":
                    case "PHP":
                    case "Java":
                        textEditor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.CSharp.CSharpIndentationStrategy(textEditor.Options);
                        //foldingStrategy = new BraceFoldingStrategy();
                        break;
                    default:
                        textEditor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.DefaultIndentationStrategy();
                        foldingStrategy = null;
                        break;
                }
            }
            if (foldingStrategy != null)
            {
                if (foldingManager == null)
                    foldingManager = FoldingManager.Install(textEditor.TextArea);
                foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);
            }
            else
            {
                if (foldingManager != null)
                {
                    FoldingManager.Uninstall(foldingManager);
                    foldingManager = null;
                }
            }
        }

        void foldingUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (foldingStrategy != null)
            {
                foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);
            }
        }
        #endregion
    }
}
