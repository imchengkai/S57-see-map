
#region Using

using System;
using System.IO;
using System.Text;

#endregion


namespace EasyMap.Converters.WellKnownText.IO
{
    internal class StreamTokenizer
    {
        private int _colNumber = 1;
        private StringBuilder _currentToken;
        private TokenType _currentTokenType;
        private bool _ignoreWhitespace = false;
        private int _lineNumber = 1;
        private TextReader _reader;

        #region Constructors

        public StreamTokenizer(TextReader reader, bool ignoreWhitespace)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            _reader = reader;
            _ignoreWhitespace = ignoreWhitespace;
            _currentToken = new StringBuilder();
        }

        #endregion

        #region Properties

        public int LineNumber
        {
            get { return _lineNumber; }
        }

        public int Column
        {
            get { return _colNumber; }
        }

        #endregion

        #region Methods

        public double GetNumericValue()
        {
            string number = GetStringValue();
            if (GetTokenType() == TokenType.Number)
            {
                return double.Parse(number, Map.NumberFormatEnUs);
            }
            throw new Exception(String.Format(Map.NumberFormatEnUs,
                                              "The token '{0}' is not a number at line {1} column {2}.", number,
                                              LineNumber, Column));
            ;
        }

        public string GetStringValue()
        {
            return _currentToken.ToString();
        }

        public TokenType GetTokenType()
        {
            return _currentTokenType;
        }

        public TokenType NextToken(bool ignoreWhitespace)
        {
            TokenType nextTokenType;
            if (ignoreWhitespace)
            {
                nextTokenType = NextNonWhitespaceToken();
            }
            else
            {
                nextTokenType = NextTokenAny();
            }
            return nextTokenType;
        }

        public TokenType NextToken()
        {
            return NextToken(_ignoreWhitespace);
        }

        private TokenType NextTokenAny()
        {
            TokenType nextTokenType = TokenType.Eof;
            char[] chars = new char[1];
            _currentToken = new StringBuilder();
            _currentTokenType = TokenType.Eof;
            int finished = _reader.Read(chars, 0, 1);

            bool isNumber = false;
            bool isWord = false;
            Char currentCharacter;
            Char nextCharacter;
            while (finished != 0)
            {
                currentCharacter = chars[0];
                nextCharacter = (char)_reader.Peek();
                _currentTokenType = GetType(currentCharacter);
                nextTokenType = GetType(nextCharacter);

                if (isWord && currentCharacter == '_')
                {
                    _currentTokenType = TokenType.Word;
                }
                if (isWord && _currentTokenType == TokenType.Number)
                {
                    _currentTokenType = TokenType.Word;
                }

                if (_currentTokenType == TokenType.Word && nextCharacter == '_')
                {
                    nextTokenType = TokenType.Word;
                    isWord = true;
                }
                if (_currentTokenType == TokenType.Word && nextTokenType == TokenType.Number)
                {
                    nextTokenType = TokenType.Word;
                    isWord = true;
                }

                if (currentCharacter == '-' && nextTokenType == TokenType.Number) // && isNumber == false)
                {
                    _currentTokenType = TokenType.Number;
                    nextTokenType = TokenType.Number;
                }

                if (isNumber && (nextCharacter.Equals('E') || nextCharacter.Equals('e')))
                {
                    nextTokenType = TokenType.Number;
                }

                if (isNumber && (currentCharacter.Equals('E') || currentCharacter.Equals('e')) && (nextTokenType == TokenType.Number || nextTokenType == TokenType.Symbol))
                {
                    _currentTokenType = TokenType.Number;
                    nextTokenType = TokenType.Number;
                }


                if (isNumber && nextTokenType == TokenType.Number && currentCharacter == '.')
                {
                    _currentTokenType = TokenType.Number;
                }
                if (_currentTokenType == TokenType.Number && nextCharacter == '.' && isNumber == false)
                {
                    nextTokenType = TokenType.Number;
                    isNumber = true;
                }


                _colNumber++;
                if (_currentTokenType == TokenType.Eol)
                {
                    _lineNumber++;
                    _colNumber = 1;
                }

                _currentToken.Append(currentCharacter);
                if (_currentTokenType != nextTokenType)
                {
                    finished = 0;
                }
                else if (_currentTokenType == TokenType.Symbol && currentCharacter != '-')
                {
                    finished = 0;
                }
                else
                {
                    finished = _reader.Read(chars, 0, 1);
                }
            }
            return _currentTokenType;
        }

        private TokenType GetType(char character)
        {
            if (Char.IsDigit(character))
            {
                return TokenType.Number;
            }
            else if (Char.IsLetter(character))
            {
                return TokenType.Word;
            }
            else if (character == '\n')
            {
                return TokenType.Eol;
            }
            else if (Char.IsWhiteSpace(character) || Char.IsControl(character))
            {
                return TokenType.Whitespace;
            }
            else //(Char.IsSymbol(character))
            {
                return TokenType.Symbol;
            }
        }

        private TokenType NextNonWhitespaceToken()
        {
            TokenType tokentype = NextTokenAny();
            while (tokentype == TokenType.Whitespace || tokentype == TokenType.Eol)
            {
                tokentype = NextTokenAny();
            }

            return tokentype;
        }

        #endregion
    }
}
