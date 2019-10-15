namespace ASmarty.Tools
{
    internal enum TokenType
    {
        SingleQuote,
        DoubleQuote,
        BackTick,
        OpenBrace,
        CloseBrace,
        OpenBraceForwardSlash,
        Equals,
        Asterisk,
        Whitespace,
        Pipe,
        Colon,
        Backslash,
        Identifier,
        Html
    }

    internal class Token
    {
        public TokenType Type { get; private set; }
        public string Value { get; private set; }

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}
