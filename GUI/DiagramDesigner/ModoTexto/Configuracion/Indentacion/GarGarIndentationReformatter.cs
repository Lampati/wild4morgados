// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using ICSharpCode.AvalonEdit.Indentation.CSharp;

namespace DiagramDesigner.ModoTexto.Configuracion.Indentacion
{
	sealed class IndentationSettings
	{
		public string IndentString = "\t";
		/// <summary>Leave empty lines empty.</summary>
		public bool LeaveEmptyLines = true;
	}
	
	sealed class GarGarIndentationReformatter
	{



		/// <summary>
		/// An indentation block. Tracks the state of the indentation.
		/// </summary>
		struct Block
		{
			/// <summary>
			/// The indentation outside of the block.
			/// </summary>
			public string OuterIndent;
			
			/// <summary>
			/// The indentation inside the block.
			/// </summary>
			public string InnerIndent;
			
			/// <summary>
			/// The last word that was seen inside this block.
			/// Because parenthesis open a sub-block and thus don't change their parent's LastWord,
			/// this property can be used to identify the type of block statement (if, while, switch)
			/// at the position of the '{'.
			/// </summary>
			public string LastWord;
			
			/// <summary>
			/// The type of bracket that opened this block (, [ or {
            /// En este caso, seria comenzar, hacer, entonces, sino
			/// </summary>
			public string Comienzo;
			
			/// <summary>
			/// Gets whether there's currently a line continuation going on inside this block.
			/// </summary>
			public bool Continuation;
			
			
			
			/// <summary>
			/// Gets the line number where this block started.
			/// </summary>
			public int StartLine;

            
			
			public void Indent(IndentationSettings set)
			{
				Indent(set.IndentString);
			}
			
			public void Indent(string indentationString)
			{
				OuterIndent = InnerIndent;
				InnerIndent += indentationString;
				Continuation = false;
               
				
				LastWord = "";
			}
			
			public override string ToString()
			{
				return string.Format(
					CultureInfo.InvariantCulture,
					"[Block StartLine={0}, LastWord='{1}', Continuation={2}]",
					this.StartLine, this.LastWord, this.Continuation);
			}
		}
		
		StringBuilder wordBuilder;
		Stack<Block> blocks; // blocks contains all blocks outside of the current
		Block block;  // block is the current block
		
		bool inString;
		bool inChar;
		bool verbatim;
		bool escape;

        bool finBloque;
		
		bool lineComment;
		bool blockComment;
		
		char lastRealChar; // last non-comment char
		
		public void Reformat(IDocumentAccessor doc, IndentationSettings set)
		{
			Init();
			
			while (doc.MoveNext()) {
				Step(doc, set);
			}
		}
		
		public void Init()
		{
			wordBuilder = new StringBuilder();
			blocks = new Stack<Block>();
			block = new Block();
			block.InnerIndent = "";
			block.OuterIndent = "";
			//block.Bracket = '{';
            block.Comienzo = "comenzar";
			block.Continuation = false;
			block.LastWord = "";

			block.StartLine = 0;
			
			inString = false;
			inChar   = false;
			verbatim = false;
			escape   = false;
			
			lineComment  = false;
			blockComment = false;

            finBloque = false;
			
			lastRealChar = ' '; // last non-comment char
		}
		
		public void Step(IDocumentAccessor doc, IndentationSettings set)
		{
			string line = doc.Text;
			if (set.LeaveEmptyLines && line.Length == 0) return; // leave empty lines empty
			line = line.TrimStart();
			
			StringBuilder indent = new StringBuilder();
			if (line.Length == 0) {
				// Special treatment for empty lines:
				if (blockComment || (inString && verbatim))
					return;
				indent.Append(block.InnerIndent);
                
				if (block.Continuation)
					indent.Append(set.IndentString);
				if (doc.Text != indent.ToString())
					doc.Text = indent.ToString();
				return;
			}
			
			if (TrimEnd(doc))
				line = doc.Text.TrimStart();
			
			Block oldBlock = block;
			bool startInComment = blockComment;
			bool startInString = (inString && verbatim);
			
			#region Parse char by char
			lineComment = false;
			inChar = false;
			escape = false;
			if (!verbatim) inString = false;
			
			lastRealChar = '\n';
			
			char lastchar = ' ';
			char c = ' ';
			char nextchar = line[0];
			for (int i = 0; i < line.Length; i++) {
				if (lineComment) break; // cancel parsing current line
				
				lastchar = c;
				c = nextchar;
				if (i + 1 < line.Length)
					nextchar = line[i + 1];
				else
					nextchar = '\n';
				
				if (escape) {
					escape = false;
					continue;
				}
				
				#region Check for comment/string chars
				switch (c) {
                    case '{':
                        blockComment = true;
                        break;
                    case '}':
                        if (blockComment && lastchar == '*')
                            blockComment = false;
                        break;

	
					case '\'':
                        if (!(inChar || lineComment || blockComment))
                        {
                            inString = !inString;
                            if (!inString && verbatim)
                            {
                                if (nextchar == '\'')
                                {
                                    escape = true; // skip escaped quote
                                    inString = true;
                                }
                                else
                                {
                                    verbatim = false;
                                }
                                //} else if (inString && lastchar == '@') {
                                //    verbatim = true;
                                //}
                            }
                           
                        }
                        break;
				}
				#endregion
				
				if (lineComment || blockComment || inString || inChar) {
					if (wordBuilder.Length > 0)
						block.LastWord = wordBuilder.ToString();
					wordBuilder.Length = 0;
					continue;
				}
				
				
				
				if (Char.IsLetterOrDigit(c)) {
					wordBuilder.Append(c);
				} else {
					if (wordBuilder.Length > 0)
						block.LastWord = wordBuilder.ToString();
					wordBuilder.Length = 0;
				}

                //if (!Char.IsWhiteSpace(c) && c != '[' && c != '/') {
                //    if (block.Bracket == '{')
                //        block.Continuation = true;
                //}
                BloqueIdentacion b;

                switch (wordBuilder.ToString().ToLower())
                {
                    case "comenzar":
                        
                        blocks.Push(block);
                        block.StartLine = doc.LineNumber;
                
                        block.Indent(set);

                        //Le sumo 1 pq sino me va a tomar la primer linea como si tuviese un tab.
                          b = new BloqueIdentacion(){ StartLine= block.StartLine+1, EndLine = int.MaxValue, TabsBeforeInnerBlock = block.OuterIndent.Length};
                        GarGarIndentationStrategy.bloquesAbiertosParaIdentacion.Push(b);

                        
                        block.Comienzo = "comenzar";
                        break;
                    case "finproc":
                    case "finfunc":
                        while (block.Comienzo != "comenzar")
                        {
                            if (blocks.Count == 0) break;
                            block = blocks.Pop();
                        }
                        if (blocks.Count == 0) break;

                        b = new BloqueIdentacion() { StartLine = block.StartLine + 1, EndLine = doc.LineNumber, TabsBeforeInnerBlock = block.OuterIndent.Length };
                        GarGarIndentationStrategy.bloquesParaIdentacion.Push(b);
                        if (GarGarIndentationStrategy.bloquesAbiertosParaIdentacion.Count > 0)
                        {
                            GarGarIndentationStrategy.bloquesAbiertosParaIdentacion.Pop();
                        }

                        block = blocks.Pop();
                        block.Continuation = false;
                        finBloque = true;
                        GarGarTextDocumentAccessor.quitarTabGlobal = true;

        


                        break;

                    case "hacer":
                        
                        blocks.Push(block);
                        block.StartLine = doc.LineNumber;

                        block.Indent(set);

                        b = new BloqueIdentacion() { StartLine = block.StartLine + 1, EndLine = int.MaxValue, TabsBeforeInnerBlock = block.OuterIndent.Length };
                        GarGarIndentationStrategy.bloquesAbiertosParaIdentacion.Push(b);


                        block.Comienzo = "hacer";
                        break;
                    case "finmientras":
                        while (block.Comienzo != "hacer")
                        {
                            if (blocks.Count == 0) break;
                            block = blocks.Pop();
                        }
                        if (blocks.Count == 0) break;

                        b = new BloqueIdentacion(){ StartLine= block.StartLine+1, EndLine = doc.LineNumber, TabsBeforeInnerBlock = block.OuterIndent.Length};
                        GarGarIndentationStrategy.bloquesParaIdentacion.Push(b);

                        if (GarGarIndentationStrategy.bloquesAbiertosParaIdentacion.Count > 0)
                        {
                            GarGarIndentationStrategy.bloquesAbiertosParaIdentacion.Pop();
                        }

                        block = blocks.Pop();
                        block.Continuation = false;
                        finBloque = true;
                        GarGarTextDocumentAccessor.quitarTabGlobal = true;

            

                        break;

                    case "entonces":
                        
                        blocks.Push(block);
                        block.StartLine = doc.LineNumber;

                        block.Indent(set);

                        b = new BloqueIdentacion() { StartLine = block.StartLine + 1, EndLine = int.MaxValue, TabsBeforeInnerBlock = block.OuterIndent.Length };
                        GarGarIndentationStrategy.bloquesAbiertosParaIdentacion.Push(b);

                        block.Comienzo = "entonces";
                        break;
                    case "finsi":
                        while (block.Comienzo != "entonces")
                        {
                            if (blocks.Count == 0) break;
                            block = blocks.Pop();
                        }
                        if (blocks.Count == 0) break;


                        b = new BloqueIdentacion() { StartLine = block.StartLine + 1, EndLine = doc.LineNumber, TabsBeforeInnerBlock = block.OuterIndent.Length };
                        GarGarIndentationStrategy.bloquesParaIdentacion.Push(b);

                        if (GarGarIndentationStrategy.bloquesAbiertosParaIdentacion.Count > 0)
                        {
                            GarGarIndentationStrategy.bloquesAbiertosParaIdentacion.Pop();
                        }

                        block = blocks.Pop();
                        block.Continuation = false;
                        finBloque = true;

                        GarGarTextDocumentAccessor.quitarTabGlobal = true;

                        
                        break;

                }
				
                //#region Push/Pop the blocks
                //switch (c) {
                //    case '{':
                //        block.ResetOneLineBlock();
                //        blocks.Push(block);
                //        block.StartLine = doc.LineNumber;
                //        if (block.LastWord == "switch") {
                //            block.Indent(set.IndentString + set.IndentString);
                //            /* oldBlock refers to the previous line, not the previous block
                //             * The block we want is not available anymore because it was never pushed.
                //             * } else if (oldBlock.OneLineBlock) {
                //            // Inside a one-line-block is another statement
                //            // with a full block: indent the inner full block
                //            // by one additional level
                //            block.Indent(set, set.IndentString + set.IndentString);
                //            block.OuterIndent += set.IndentString;
                //            // Indent current line if it starts with the '{' character
                //            if (i == 0) {
                //                oldBlock.InnerIndent += set.IndentString;
                //            }*/
                //        } else {
                //            block.Indent(set);
                //        }
                //        block.Bracket = '{';
                //        break;
                //    case '}':
                //        while (block.Bracket != '{') {
                //            if (blocks.Count == 0) break;
                //            block = blocks.Pop();
                //        }
                //        if (blocks.Count == 0) break;
                //        block = blocks.Pop();
                //        block.Continuation = false;
                //        block.ResetOneLineBlock();
                //        break;
                //    case '(':
                //    case '[':
                //        blocks.Push(block);
                //        if (block.StartLine == doc.LineNumber)
                //            block.InnerIndent = block.OuterIndent;
                //        else
                //            block.StartLine = doc.LineNumber;
                //        block.Indent(Repeat(set.IndentString, oldBlock.OneLineBlock) +
                //                     (oldBlock.Continuation ? set.IndentString : "") +
                //                     (i == line.Length - 1 ? set.IndentString : new String(' ', i + 1)));
                //        block.Bracket = c;
                //        break;
                //    case ')':
                //        if (blocks.Count == 0) break;
                //        if (block.Bracket == '(') {
                //            block = blocks.Pop();
                //            if (IsSingleStatementKeyword(block.LastWord))
                //                block.Continuation = false;
                //        }
                //        break;
                //    case ']':
                //        if (blocks.Count == 0) break;
                //        if (block.Bracket == '[')
                //            block = blocks.Pop();
                //        break;
                //    case ';':
                //    case ',':
                //        block.Continuation = false;
                //        block.ResetOneLineBlock();
                //        break;
                //    case ':':
                //        if (block.LastWord == "case" 
                //            || line.StartsWith("case ", StringComparison.Ordinal) 
                //            || line.StartsWith(block.LastWord + ":", StringComparison.Ordinal)) 
                //        {
                //            block.Continuation = false;
                //            block.ResetOneLineBlock();
                //        }
                //        break;
                //}
				
                if (!Char.IsWhiteSpace(c)) {
                    // register this char as last char
                    lastRealChar = c;
                }
                
			}
			#endregion
			
			if (wordBuilder.Length > 0)
				block.LastWord = wordBuilder.ToString();
			wordBuilder.Length = 0;
			
			if (startInString) return;
			if (startInComment && line[0] != '*') return;
			if (doc.Text.StartsWith("//\t", StringComparison.Ordinal) || doc.Text == "//")
				return;
			
			//if (line[0] == '}') {
            if (finBloque)
            {
                
				indent.Append(oldBlock.OuterIndent);
                
				oldBlock.Continuation = false;
			} else {
				indent.Append(oldBlock.InnerIndent);
			}

            //if (indent.Length > 0 && oldBlock.Bracket == '(' && line[0] == ')')
            //{
            //    indent.Remove(indent.Length - 1, 1);
            //}
            //else if (indent.Length > 0 && oldBlock.Bracket == '[' && line[0] == ']')
            //{
            //    indent.Remove(indent.Length - 1, 1);
            //}

            if (line[0] == ':')
            {
                oldBlock.Continuation = true;
            }
            else if (lastRealChar == ':' && indent.Length >= set.IndentString.Length)
            {
                if (block.LastWord == "case" || line.StartsWith("case ", StringComparison.Ordinal) || line.StartsWith(block.LastWord + ":", StringComparison.Ordinal))
                    indent.Remove(indent.Length - set.IndentString.Length, set.IndentString.Length);
            }
            
            else if (lastRealChar == 'o' && block.LastWord == "sino")
            {                
                block.Continuation = false;                
            }
			
			if (doc.IsReadOnly) {
				// We can't change the current line, but we should accept the existing
				// indentation if possible (if the current statement is not a multiline
				// statement).

                if (finBloque)
                {
                    finBloque = false;

                }


				if (!oldBlock.Continuation &&
				    oldBlock.StartLine == block.StartLine &&
				    block.StartLine < doc.LineNumber && lastRealChar != ':')
				{
					// use indent StringBuilder to get the indentation of the current line
					indent.Length = 0;
					line = doc.Text; // get untrimmed line
					for (int i = 0; i < line.Length; ++i) {
						if (!Char.IsWhiteSpace(line[i]))
							break;
						indent.Append(line[i]);
					}
					// /* */ multiline comments have an extra space - do not count it
					// for the block's indentation.
					if (startInComment && indent.Length > 0 && indent[indent.Length - 1] == ' ') {
						indent.Length -= 1;
					}
					block.InnerIndent = indent.ToString();
				}
				return;
			}
			
            //if (line[0] != '{') {
            //    if (line[0] != ')' && oldBlock.Continuation && oldBlock.Comienzo == "comenzar" && oldBlock.Comienzo == "entonces" && oldBlock.Comienzo == "hacer")
            //        indent.Append(set.IndentString);
            //    indent.Append(Repeat(set.IndentString, oldBlock.OneLineBlock));
            //}
			
			// this is only for blockcomment lines starting with *,
			// all others keep their old indentation
			if (startInComment)
				indent.Append(' ');
			
			if (indent.Length != (doc.Text.Length - line.Length) ||
			    !doc.Text.StartsWith(indent.ToString(), StringComparison.Ordinal) ||
			    Char.IsWhiteSpace(doc.Text[indent.Length]))
			{
				doc.Text = indent.ToString() + line;
			}
		}
		
	

       
		
		static bool TrimEnd(IDocumentAccessor doc)
		{
			string line = doc.Text;
			if (!Char.IsWhiteSpace(line[line.Length - 1])) return false;
			
			// one space after an empty comment is allowed
			if (line.EndsWith("// ", StringComparison.Ordinal) || line.EndsWith("* ", StringComparison.Ordinal))
				return false;
			
			doc.Text = line.TrimEnd();
			return true;
		}
	}
}
