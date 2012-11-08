// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Indentation;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Indentation.CSharp;
using System.Collections.Generic;
using System.Text;

namespace Ragnarok.ModoTexto.Configuracion.Indentacion
{
	/// <summary>
	/// Smart indentation for GarGar.
	/// </summary>
	public class GarGarIndentationStrategy : DefaultIndentationStrategy
	{

        public static Stack<BloqueIdentacion> bloquesParaIdentacion = new Stack<BloqueIdentacion>();
        public static Stack<BloqueIdentacion> bloquesAbiertosParaIdentacion = new Stack<BloqueIdentacion>();
		/// <summary>
		/// Creates a new CSharpIndentationStrategy.
		/// </summary>
		public GarGarIndentationStrategy()
		{
		}
		
		/// <summary>
		/// Creates a new CSharpIndentationStrategy and initializes the settings using the text editor options.
		/// </summary>
        public GarGarIndentationStrategy(TextEditorOptions options)
		{
			this.IndentationString = options.IndentationString;
		}
		
		string indentationString = "\t";
		
		/// <summary>
		/// Gets/Sets the indentation string.
		/// </summary>
		public string IndentationString {
			get { return indentationString; }
			set {
				if (string.IsNullOrEmpty(value))
					throw new ArgumentException("Indentation string must not be null or empty");
				indentationString = value;
			}
		}
		
		/// <summary>
		/// Performs indentation using the specified document accessor.
		/// </summary>
		/// <param name="document">Object used for accessing the document line-by-line</param>
		/// <param name="keepEmptyLines">Specifies whether empty lines should be kept</param>
		public void Indent(IDocumentAccessor document, bool keepEmptyLines)
		{
            bloquesParaIdentacion = new Stack<BloqueIdentacion>();
            bloquesAbiertosParaIdentacion = new Stack<BloqueIdentacion>();
			if (document == null)
				throw new ArgumentNullException("document");
			
            IndentationSettings settings = new IndentationSettings();
			settings.IndentString = this.IndentationString;
			settings.LeaveEmptyLines = keepEmptyLines;
			
			GarGarIndentationReformatter r = new GarGarIndentationReformatter();
			r.Reformat(document, settings);

            
		}
		
		/// <inheritdoc cref="IIndentationStrategy.IndentLine"/>
		public override void IndentLine(TextDocument document, DocumentLine line)
		{
            bloquesParaIdentacion = new Stack<BloqueIdentacion>();
			int lineNr = line.LineNumber;
            GarGarTextDocumentAccessor acc = new GarGarTextDocumentAccessor(document, lineNr, lineNr);
			Indent(acc, false);
			
			string t = acc.Text;
			if (t.Length == 0) {
				// use AutoIndentation for new lines in comments / verbatim strings.
				base.IndentLine(document, line);
			}


            List<int> tabsPorLinea = new List<int>(document.LineCount);
            for (int i = 0; i < document.LineCount - 1; i++)
            {
                tabsPorLinea.Add(0);
            }

            BloqueIdentacion bloq;

            while (bloquesParaIdentacion.Count > 0)
            {
                bloq = bloquesParaIdentacion.Pop();

                for (int j = bloq.StartLine - 1; j < bloq.EndLine - 1; j++)
                {
                    tabsPorLinea[j]++;
                }
            }

            while (bloquesAbiertosParaIdentacion.Count > 0)
            {
                bloq = bloquesAbiertosParaIdentacion.Pop();

                for (int j = bloq.StartLine - 1; j < document.LineCount - 1; j++)
                {
                   tabsPorLinea[j]++;
                }
            }

            for (int num = 1; num < document.LineCount; num++)
            {
                
                line = document.GetLineByNumber(num);
                string aux = document.GetText(line);
                aux = aux.TrimStart('\t');
                StringBuilder strBlder = new StringBuilder();
                for (int i = 0; i < tabsPorLinea[num - 1]; i++)
                {
                    strBlder.Append('\t');
                }
                strBlder.Append(aux);
                document.Replace(line, strBlder.ToString());
                
            }


		}
		
		/// <inheritdoc cref="IIndentationStrategy.IndentLines"/>
		public override void IndentLines(TextDocument document, int beginLine, int endLine)
		{
            Indent(new GarGarTextDocumentAccessor(document, beginLine, endLine), true);
		}
	}

    public class BloqueIdentacion
    {
        public int StartLine;
        public int EndLine;
        public int TabsBeforeInnerBlock;

        public override string ToString()
        {
            return string.Format("Comienzo:{0};Fin:{1};CantTabs:{2}",StartLine,EndLine,TabsBeforeInnerBlock);
        }
    }
}
