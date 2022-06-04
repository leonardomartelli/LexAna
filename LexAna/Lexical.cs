using System;
namespace LexAna
{
    public class Lexical
    {
        private readonly IDictionary<string, Token> _reservedWords =
            new Dictionary<string, Token>
            {
                { "int", Token.Int },
                { "float", Token.Float },
                { "double", Token.Double },
                { "void", Token.Void },
                { "return", Token.Return },
                { "continue", Token.Continue },
                { "break", Token.Break },
                { "if", Token.If },
                { "else", Token.Else },
                { "for", Token.For },
                { "struct", Token.Struct }
            };

        private StreamReader _inputStream;
        private StreamWriter _outputStream;

        public Lexical(StreamReader inputStream, string resultsFolder)
        {
            _inputStream = inputStream;

            SetupResultsWriting(resultsFolder);
        }

        private void SetupResultsWriting(string resultsFolder)
        {
            var resultFile = Path.Combine(resultsFolder, $"{DateTime.Now:yyyy_MM_dd_hh_mm_ss}.lexi");

            _outputStream = File.CreateText(resultFile);
        }

        private void WriteResult(TokenResult tokenResult) =>
            _outputStream.WriteLine(tokenResult.ToString());
    }
}

