using System;
using System.IO;
using System.Text;

namespace ASmarty.Tools
{
    internal class Tokenizer
    {
        private readonly TextReader source;
        private char current;

        public Tokenizer(TextReader source)
        {
            if (source == null) throw new ArgumentNullException("source");
            this.source = source;

            // read the first character
            ReadNextChar();
        }

        private void ReadNextChar()
        {
            var character = source.Read();
            if (character > 0)
            {
                current = (char)character;
            }
            else
            {
                current = '\0';
            }
        }

        private bool AtEndOfSource 
        { 
            get { return current == '\0'; } 
        }

        public Token ReadNextToken(bool couldBeHtml)
        {
            if (AtEndOfSource)
            {
                return null;
            }

            var savedCurrent = current;
            switch (current)
            {
                case '\'': ReadNextChar(); return new Token(TokenType.SingleQuote, savedCurrent.ToString());
                case '"': ReadNextChar(); return new Token(TokenType.DoubleQuote, savedCurrent.ToString());
                case '`': ReadNextChar(); return new Token(TokenType.BackTick, savedCurrent.ToString());
                case '{': 
                    ReadNextChar();
                    if (current == '/')
                    {
                        ReadNextChar();
                        return new Token(TokenType.OpenBraceForwardSlash, "{/");
                    }
                    else
                    {
                        return new Token(TokenType.OpenBrace, savedCurrent.ToString());    
                    }
                case '}': ReadNextChar(); return new Token(TokenType.CloseBrace, savedCurrent.ToString());
                case '=': ReadNextChar(); return new Token(TokenType.Equals, savedCurrent.ToString());
                case '*': ReadNextChar(); return new Token(TokenType.Asterisk, savedCurrent.ToString());
                case ' ': ReadNextChar(); return new Token(TokenType.Whitespace, savedCurrent.ToString());
                case '|': ReadNextChar(); return new Token(TokenType.Pipe, savedCurrent.ToString());
                case ':': ReadNextChar(); return new Token(TokenType.Colon, savedCurrent.ToString());
                case '\\': ReadNextChar(); return new Token(TokenType.Backslash, savedCurrent.ToString());
            }

            var buffer = new StringBuilder();
            if (couldBeHtml)
            {
                while (current != '{' && !AtEndOfSource)
                {
                    buffer.Append(current);
                    ReadNextChar();
                }

                return new Token(TokenType.Html, buffer.ToString());
            }
            else
            {
                while (current != '\'' &&
                        current != '"' &&
                        current != '`' &&
                        current != '{' &&
                        current != '}' &&
                        current != '=' &&
                        current != '*' &&
                        current != ' ' &&
                        current != '|' &&
                        current != ':' &&
                        current != '\\')
                {
                    buffer.Append(current);
                    ReadNextChar();
                }

                return new Token(TokenType.Identifier, buffer.ToString());
            }
        }
    }
}
