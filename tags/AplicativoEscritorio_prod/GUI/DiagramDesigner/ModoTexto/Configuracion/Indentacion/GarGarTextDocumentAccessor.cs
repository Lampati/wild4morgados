// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Collections.Generic;
using System.IO;

using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Indentation.CSharp;

namespace Ragnarok.ModoTexto.Configuracion.Indentacion
{
	
	
	/// <summary>
	/// Adapter IDocumentAccessor -> TextDocument
	/// </summary>
	public sealed class GarGarTextDocumentAccessor : IDocumentAccessor
	{
        readonly TextDocument doc;
		readonly int minLine;
		readonly int maxLine;

        public static bool quitarTabGlobal = false;

       
		
		/// <summary>
		/// Creates a new TextDocumentAccessor.
		/// </summary>
		public GarGarTextDocumentAccessor(TextDocument document)
		{
			if (document == null)
				throw new ArgumentNullException("document");
			doc = document;
			this.minLine = 1;
			this.maxLine = doc.LineCount;
            
		}
		
		/// <summary>
		/// Creates a new TextDocumentAccessor that indents only a part of the document.
		/// </summary>
		public GarGarTextDocumentAccessor(TextDocument document, int minLine, int maxLine)
		{
			if (document == null)
				throw new ArgumentNullException("document");
			doc = document;
			this.minLine = minLine;
			this.maxLine = maxLine;
		}
		
		int num;
		string text;
		DocumentLine line;
		
		/// <inheritdoc/>
		public bool IsReadOnly {
			get {
				return num < minLine;                
			}
		}
		
		/// <inheritdoc/>
		public int LineNumber {
			get {
				return num;
			}
		}
		
		bool lineDirty;
		
		/// <inheritdoc/>
		public string Text {
			get { return text; }
			set {
				if (num < minLine) return;
				text = value;
				lineDirty = true;
			}
		}
		
		/// <inheritdoc/>
		public bool MoveNext()
		{
			if (lineDirty) {
                //if (quitarTabGlobal)
                //{
                //    if (text.Length > 0)
                //    {
                //        if (text[0] == '\t')
                //        {
                //            text = text.Remove(0, 1);
                //        }
                //    }
                //    quitarTabGlobal = false;
                //}
				doc.Replace(line, text);
				lineDirty = false;
			}
			++num;
			if (num > maxLine) return false;
			line = doc.GetLineByNumber(num);
			text = doc.GetText(line);
			return true;
		}
	}

   
	
}
