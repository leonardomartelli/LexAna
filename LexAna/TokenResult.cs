using System;
namespace LexAna
{
	public record TokenResult
	{
		public TokenResult(Token token, string label, int line, int column)
		{
            Token = token;
            Label = label;
            Line = line;
            Column = column;
        }

        public Token Token { get; }
        public string Label { get; }
        public int Line { get; }
        public int Column { get; }
    }
}

