// Copyright 2005-2013 Giacomo Stelluti Scala & Contributors. All rights reserved. See doc/License.md in the project root for license information.

using System;

namespace CommandLine.Text
{
    /// <summary>
    /// Exposes standard delegates to provide a mean to customize part of help screen generation.
    /// This type is consumed by <see cref="CommandLine.Text.HelpText"/>.
    /// </summary>
    public abstract class SentenceBuilder
    {
        /// <summary>
        /// Create the default instance of <see cref="CommandLine.Text.SentenceBuilder"/>,
        /// localized in English.
        /// </summary>
        /// <returns>The default <see cref="CommandLine.Text.SentenceBuilder"/> instance.</returns>
        public static SentenceBuilder CreateDefault()
        {
            return new DefaultSentenceBuilder();
        }

        /// <summary>
        /// Gets a delegate that returns the word 'required'.
        /// </summary>
        public abstract Func<string> RequiredWord { get; }

        /// <summary>
        /// Gets a delegate that returns that error heading text.
        /// </summary>
        public abstract Func<string> ErrorsHeadingText { get; }

        /// <summary>
        /// Get a delegate that returns the help text of helo command.
        /// The delegates should accept a boolean that is equal <value>true</value> for options; otherwise <value>false</value> for verbs.
        /// </summary>
        public abstract Func<bool, string> HelpCommandText { get; } 

        /// <summary>
        /// Gets a delegate that handle singular error formatting.
        /// The delegates should accept an <see cref="Error"/> instance as input.
        /// </summary>
        public abstract Func<Error, string> FormatError { get; }

        private class DefaultSentenceBuilder : SentenceBuilder 
        {
            public override Func<string> RequiredWord
            {
                get
                {
                    return () => "Requerido.";
                }
            }

            public override Func<string> ErrorsHeadingText
            {
                get
                {
                    return () => "ERROR(ES):";
                }
            }

            public override Func<bool, string> HelpCommandText
            {
                get
                {
                    return isOption => isOption
                        ? "Mostrar esta pantalla de ayuda."
                        : "Mostrar mas ayuda sobre un comando en particular.";
                }
            }

            public override Func<Error, string> FormatError
            {
                get
                {
                    return error =>
                        {
                            switch (error.Tag)
                            {
                                case ErrorType.BadFormatTokenError:
                                    return "El parámetro '" + ((BadFormatTokenError)error).Token + "' no es aceptado.";
                                case ErrorType.MissingValueOptionError:
                                    return "El parámetro '" + ((MissingValueOptionError)error).NameInfo.NameText
                                                      + "' no tiene valor asociado.";
                                case ErrorType.UnknownOptionError:
                                    return "El parámetro '" + ((UnknownOptionError)error).Token + "' es desconocida.";
                                case ErrorType.MissingRequiredOptionError:
                                    var errMisssing = ((MissingRequiredOptionError)error);
                                    return errMisssing.NameInfo == NameInfo.EmptyName
                                               ? " Esta faltando un valor asociado a un parámetro requerida" 
                                                 //"A required value not bound to option name is missing."
                                               : "El parámetro requerido '" + errMisssing.NameInfo.NameText + "' no fue encontrado.";
                                case ErrorType.MutuallyExclusiveSetError:
                                    return "El parámetro '" + ((MutuallyExclusiveSetError)error).NameInfo.NameText + "' fue definido junto a otro con el cual no es compatible.";
                                case ErrorType.BadFormatConversionError:
                                    var badFormat = ((BadFormatConversionError)error);
                                    return badFormat.NameInfo == NameInfo.EmptyName
                                               ? "A value not bound to option name is defined with a bad format."
                                               : "Option '" + badFormat.NameInfo.NameText + "' is defined with a bad format.";
                                case ErrorType.SequenceOutOfRangeError:
                                    var seqOutRange = ((SequenceOutOfRangeError)error);
                                    return seqOutRange.NameInfo == NameInfo.EmptyName
                                               ? "A sequence value not bound to option name is defined with few items than required."
                                               : "A sequence option '" + seqOutRange.NameInfo.NameText + "' is defined with few items than required.";
                                case ErrorType.BadVerbSelectedError:
                                    return "El verbo '" + ((BadVerbSelectedError)error).Token + "' no es reconocido.";
                                case ErrorType.NoVerbSelectedError:
                                    return "No se selecciono verbo.";
                            }
                            throw new InvalidOperationException();
                        };
                }
            }
        }
    }
}
