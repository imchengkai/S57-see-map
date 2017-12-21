
#region Using

using System;
using System.IO;
using EasyMap.Converters.WellKnownText.IO;

#endregion

namespace EasyMap.Converters.WellKnownText
{
    internal class WktStreamTokenizer : StreamTokenizer
    {
        #region Constructors

        public WktStreamTokenizer(TextReader reader)
            : base(reader, true)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
        }

        #endregion

        #region Methods

        internal void ReadToken(string expectedToken)
        {
            NextToken();
            if (GetStringValue() != expectedToken)
            {
                throw new Exception(String.Format(Map.NumberFormatEnUs,
                                                  "Expecting ('{3}') but got a '{0}' at line {1} column {2}.",
                                                  GetStringValue(), LineNumber, Column, expectedToken));
            }
        }

        public string ReadDoubleQuotedWord()
        {
            string word = "";
            ReadToken("\"");
            NextToken(false);
            while (GetStringValue() != "\"")
            {
                word = word + GetStringValue();
                NextToken(false);
            }
            return word;
        }

        public void ReadAuthority(ref string authority, ref long authorityCode)
        {
            if (GetStringValue() != "AUTHORITY")
                ReadToken("AUTHORITY");
            ReadToken("[");
            authority = ReadDoubleQuotedWord();
            ReadToken(",");
            long.TryParse(ReadDoubleQuotedWord(), out authorityCode);
            ReadToken("]");
        }

        #endregion
    }
}
