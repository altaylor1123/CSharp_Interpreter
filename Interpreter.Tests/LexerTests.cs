using Interpreter;
using Shouldly;


namespace Interpreter.Tests;

public class LexerTests
{

	private class Expected
	{
		public required TokenType expectedType;
		public required string expectedLiteral;
	}

	[Fact]
	public void Lexer_Returns_Type_and_Literal()
	{
		//Arrange

		string input = @"=+(){},;";

		List<Expected> expectedResults = new() {
			new Expected { expectedType = new TokenType(TokenTypes.ASSIGN), expectedLiteral = "=" },
			new Expected { expectedType = new TokenType(TokenTypes.PLUS), expectedLiteral = "+" },
			new Expected { expectedType = new TokenType(TokenTypes.LPAREN), expectedLiteral = "(" },
			new Expected { expectedType = new TokenType(TokenTypes.RPAREN), expectedLiteral = ")" },
			new Expected { expectedType = new TokenType(TokenTypes.LBRACE), expectedLiteral = "{" },
			new Expected { expectedType = new TokenType(TokenTypes.RBRACE), expectedLiteral = "}" },
			new Expected { expectedType = new TokenType(TokenTypes.COMMA), expectedLiteral = "," },
			new Expected { expectedType = new TokenType(TokenTypes.SEMICOLON), expectedLiteral = ";" },
			new Expected { expectedType = new TokenType(TokenTypes.EOF), expectedLiteral = "" },
			};

		Lexer lex = Lexer.CreateFromInput(input);

		foreach (Expected er in expectedResults)
		{
			var tok = lex.NextToken();

			tok.Type.ShouldBe(er.expectedType);
			tok.Literal.ShouldBe(er.expectedLiteral);
		}


	}
}