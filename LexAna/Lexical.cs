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

        private readonly StreamReader _inputStream;
        private readonly StreamWriter _outputStream;

        private char _character;
        private string _line;
        private int _currentColumn;
        private int _currentLine;
        private string _lexical;
        private State _state;

        private bool _hasPoint;
        private bool _eof;


        public Lexical(StreamReader inputStream, string resultsFolder)
        {
            if (inputStream is null)
                throw new ArgumentNullException(nameof(inputStream));

            _character = default;
            _line = _lexical = string.Empty;
            _currentColumn = _currentLine = 0;
            _state = State.Initial;

            _hasPoint = _eof = false;

            _inputStream = inputStream;

            _outputStream = SetupResultsWriting(resultsFolder);
        }

        public void Analyse()
        {
            ReadChar();

            while (!_eof)
            {
                var result = ReadToken();

                if (result is null)
                    continue;

                CleanLexical();
                WriteResult(result);
            }

            var finalResult = new TokenResult(Token.EOF, string.Empty, _currentLine, _currentColumn);
            WriteResult(finalResult);

            _inputStream.Close();
            _outputStream.Close();
        }

        private void ReadLine()
        {
            _line = _inputStream.ReadLine();

            if (_line is null)
                _eof = true;

            _line += '\n';
            _currentLine++;
        }

        private void ReadChar()
        {
            if (string.IsNullOrEmpty(_line) || _currentColumn >= _line.Length)
            {
                ReadLine();
                _currentColumn = 0;
            }

            _character = _line[_currentColumn++];
        }

        private TokenResult ReadToken()
        {
            AppendCharacter();

            switch (_state)
            {
                case State.Initial:
                    return ReadInitialToken();

                case State.Word:
                    return ReadWordToken();

                case State.Number:
                    return ReadNumberToken();

                default:
                    break;
            }

            return default;
        }

        private TokenResult ReadInitialToken()
        {
            if (_character == ' ' || _character == '\t' || _character == '\n')
            {
                CleanLexical();
                ReadChar();
            }

            else if (char.IsLetter(_character) || _character == '_')
            {
                ReadChar();
                _state = State.Word;
            }

            else if (char.IsDigit(_character))
            {
                ReadChar();
                _state = State.Number;
            }

            else if (_character == '(')
                return ComputeToken(Token.ParenthesisOpen);

            else if (_character == ')')
                return ComputeToken(Token.ParenthesisClose);

            else if (_character == '[')
                return ComputeToken(Token.BracketOpen);

            else if (_character == ']')
                return ComputeToken(Token.BracketClose);

            else if (_character == '(')
                return ComputeToken(Token.BraceOpen);

            else if (_character == ')')
                return ComputeToken(Token.BraceClose);

            else if (_character == ';')
                return ComputeToken(Token.SemiCollon);

            else if (_character == ',')
                return ComputeToken(Token.Comma);

            else if (_character == '=')
                return AnalysePossibleCases(Token.Assign, ('=', Token.Equals));

            else if (_character == '!')
                return AnalysePossibleCases(Token.LogicalNot, ('=', Token.NotEquals));

            else if (_character == '>')
                return AnalysePossibleCases(Token.Greater,
                    ('=', Token.GreaterOrEqual),
                    ('>', Token.ShiftRight));

            else if (_character == '<')
                return AnalysePossibleCases(Token.Less,
                    ('=', Token.LessOrEqual),
                    ('<', Token.ShiftLeft));

            else if (_character == '+')
                return AnalysePossibleCases(Token.Plus,
                    ('=', Token.PlusAssign),
                    ('+', Token.Increment));

            else if (_character == '-')
                return AnalysePossibleCases(Token.Minus,
                    ('=', Token.MinusAssign),
                    ('-', Token.Decrement));

            else if (_character == '/')
                return AnalysePossibleCases(Token.Division,
                    ('=', Token.DivisionAssign));

            else if (_character == '*')
                return AnalysePossibleCases(Token.Product,
                    ('=', Token.ProductAssign));

            else if (_character == '%')
                return AnalysePossibleCases(Token.Module,
                    ('=', Token.ModuleAssign));

            else if (_character == '|')
                return AnalysePossibleCases(Token.Or,
                    ('|', Token.LogicalOr));

            else if (_character == '&')
                return AnalysePossibleCases(Token.And,
                    ('&', Token.LogicalAnd));

            return default;
        }

        private TokenResult ReadWordToken() => default;

        private TokenResult ReadNumberToken() => default;

        private TokenResult ComputeToken(Token token)
        {
            GetContext(out int lineNumber, out int columnNumber, out string lexical);
            ReadChar();
            CleanLexical();

            return new TokenResult(token, lexical, lineNumber, columnNumber);
        }

        private void GetContext(out int line, out int column, out string lexical)
        {
            line = _currentLine;
            column = _currentColumn;
            lexical = _lexical;
        }

        private StreamWriter SetupResultsWriting(string resultsFolder)
        {
            var resultFile = Path.Combine(resultsFolder, $"{DateTime.Now:yyyy_MM_dd_hh_mm_ss}.lexi");

            return File.CreateText(resultFile);
        }

        private TokenResult AnalysePossibleCases(Token defaultToken, params (char character, Token token)[] cases)
        {
            ReadChar();

            foreach (var (character, token) in cases)
                if (_character == character)
                {
                    AppendCharacter();
                    return ComputeToken(token);
                }

            UnreadChar();
            return ComputeToken(defaultToken);
        }

        private void AppendCharacter() =>
            _lexical += _character;

        private void WriteResult(TokenResult tokenResult) =>
            _outputStream.WriteLine(tokenResult.ToString());

        private void CleanLexical() =>
            _lexical = string.Empty;

        private void UnreadChar() =>
            _currentColumn--;
    }
}

