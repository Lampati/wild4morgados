﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FindReplace;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;
using System.IO;
using System.Xml;
using System.Windows.Input;
using Ragnarok;

namespace Ragnarok.ModoTexto.Configuracion
{
    public static class ModoTextoConfiguracion
    {


        public static void ConfigurarAvalonEdit(TextEditor textEditor)
        {
            // Load our custom highlighting definition
            IHighlightingDefinition customHighlighting;
            using (Stream s = typeof(Ragnarok.UserControls.Entorno.EsquemaCentral).Assembly.GetManifestResourceStream("Ragnarok.ModoTexto.Configuracion.Sintaxis.GarGar.xshd"))
            {
                if (s == null)
                    throw new InvalidOperationException("Could not find embedded resource");
                using (XmlReader reader = new XmlTextReader(s))
                {
                    customHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.
                        HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }
            // and register it in the HighlightingManager
            HighlightingManager.Instance.RegisterHighlighting("GarGar", new string[] { ".gar" }, customHighlighting);

            textEditor.ShowLineNumbers = true;
            textEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("GarGar");
            textEditor.Options.CutCopyWholeLine = true;
            textEditor.Options.EnableEmailHyperlinks = false;
            textEditor.Options.EnableHyperlinks = false;
            textEditor.TextArea.TextView.BackgroundRenderers.Add(
                new ICSharpCode.AvalonEdit.Rendering.SubrayadoRenderer(textEditor));
            textEditor.Document.Changed += (sender, e) =>
                {
                    ICSharpCode.AvalonEdit.Rendering.SubrayadoRenderer sr = textEditor.TextArea.TextView.BackgroundRenderers.First() as
                   ICSharpCode.AvalonEdit.Rendering.SubrayadoRenderer;
                    sr.ResetearLineas();
                };


            //textEditor.TextArea.IndentationStrategy = textEditor.TextArea.IndentationStrategy = new GarGarIndentationStrategy(textEditor.Options);


            //DispatcherTimer foldingUpdateTimer = new DispatcherTimer();
            //foldingUpdateTimer.Interval = TimeSpan.FromSeconds(2);
            //foldingUpdateTimer.Tick += foldingUpdateTimer_Tick;
            //foldingUpdateTimer.Start();


        }

       
    }
}
