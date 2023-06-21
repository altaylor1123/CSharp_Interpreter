using Interpreter;

Console.WriteLine("Hello {0}! This is the Monkey programming language!", Environment.UserName);
Console.WriteLine("Feel free to type in commands");

string? input = Console.ReadLine() ?? throw new ArgumentException("Input is null");

Lexer lex = Lexer.CreateFromInput(input);

Token tok = lex.NextToken();
while (tok.Type != new TokenType(TokenTypes.EOF))
{
	Console.WriteLine("Type: {0} Literal: {1}", tok.Type, tok.Literal);
	tok = lex.NextToken();
}
