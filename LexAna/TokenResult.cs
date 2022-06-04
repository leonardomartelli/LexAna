using System;
namespace LexAna
{
	public record TokenResult
	{
		public TokenResult(Token token, string label, int line, int finalColumn)
		{
            Token = token;
            Label = label;
            Line = line;
            StartColumn = finalColumn - label.Length;
            FinalColumn = finalColumn;
        }

        public Token Token { get; }
        public string Label { get; }
        public int Line { get; }
        public int StartColumn { get; }
        public int FinalColumn { get; }

        public override string ToString() =>
            $"{Token}\t{Label}\t" +
            $"Coluna Inicio: {StartColumn} Coluna Fim: {FinalColumn}\t" +
            $"Linha: {Line}\t";
    }
}

