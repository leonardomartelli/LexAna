using System;
namespace LexAna
{
    public record TokenResult
    {
        public TokenResult(Token token, string lexical, int line, int finalColumn)
        {
            Token = token;
            Lexical = lexical;
            Line = line;
            StartColumn = finalColumn - lexical.Length;
            FinalColumn = finalColumn;
        }

        public Token Token { get; }
        public string Lexical { get; }
        public int Line { get; }
        public int StartColumn { get; }
        public int FinalColumn { get; }

        public override string ToString()
        {
            var token = GetTokenString();
            var tokenSpacing = GetSpacing(30 - token.Length);

            var lexical = GetLexicalString();
            var lexicalSpacing = GetSpacing(30 - lexical.Length);

            var startColumn = GetStartColumnString();
            var startColumnSpacing = GetSpacing(25 - startColumn.Length);

            var finalColumn = GetFinalColumnString();
            var finalColumnSpacing = GetSpacing(25 - finalColumn.Length);

            var line = GetLineString();
            var lineSpacing = GetSpacing(15 - line.Length);

            return token + tokenSpacing +
                lexical + lexicalSpacing +
                startColumn + startColumnSpacing +
                finalColumn + finalColumnSpacing +
                line + lineSpacing;
        }

        private string GetTokenString() =>
            $"Token: {Token}";

        private string GetLexicalString() =>
            $"Lexical: {Lexical}";

        private string GetStartColumnString() =>
            $"Coluna Inicial: {StartColumn}";

        private string GetFinalColumnString() =>
            $"Coluna Final: {FinalColumn}";

        private string GetLineString() =>
            $"Linha: {Line}";

        private string GetSpacing(int count) =>
            count > 0
            ? string.Join(string.Empty, Enumerable.Range(0, count).Select(x => ' '))
            : "\t";
    }
}

