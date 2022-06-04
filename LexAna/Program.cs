using LexAna;

using var input = new StreamReader("testfile.c");

var lexicalAnalyser = new Lexical(input, "results");

lexicalAnalyser.Analyse();

