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

		//Act
		foreach (Expected er in expectedResults)
		{
			var tok = lex.NextToken();
			//Assert
			tok.Type.ShouldBe(er.expectedType);
			tok.Literal.ShouldBe(er.expectedLiteral);
		}


	}

	[Fact]
	public void Lexer_GivenMonkeyCode_ReturnsCorrect()
	{
		//Arrange
		string input = @"let five = 5;
		let ten = 10;

		let add = fn(x, y) {
			x + y;
		};

		let result = add(five, ten);
		";

		List<Expected> expectedResults = new() {
			new Expected { expectedType = new TokenType(TokenTypes.LET), expectedLiteral = "let" },
			new Expected { expectedType = new TokenType(TokenTypes.IDENT), expectedLiteral = "five" },
			new Expected { expectedType = new TokenType(TokenTypes.ASSIGN), expectedLiteral = "=" },
			new Expected { expectedType = new TokenType(TokenTypes.INT), expectedLiteral = "5" },
			new Expected { expectedType = new TokenType(TokenTypes.SEMICOLON), expectedLiteral = ";" },
			new Expected { expectedType = new TokenType(TokenTypes.LET), expectedLiteral = "let" },
			new Expected { expectedType = new TokenType(TokenTypes.IDENT), expectedLiteral = "ten" },
			new Expected { expectedType = new TokenType(TokenTypes.ASSIGN), expectedLiteral = "=" },
			new Expected { expectedType = new TokenType(TokenTypes.INT), expectedLiteral = "10" },
			new Expected { expectedType = new TokenType(TokenTypes.SEMICOLON), expectedLiteral = ";" },
			new Expected { expectedType = new TokenType(TokenTypes.LET), expectedLiteral = "let" },
			new Expected { expectedType = new TokenType(TokenTypes.IDENT), expectedLiteral = "add" },
			new Expected { expectedType = new TokenType(TokenTypes.ASSIGN), expectedLiteral = "=" },
			new Expected { expectedType = new TokenType(TokenTypes.FUNCTION), expectedLiteral = "fn" },
			new Expected { expectedType = new TokenType(TokenTypes.LPAREN), expectedLiteral = "(" },
			new Expected { expectedType = new TokenType(TokenTypes.IDENT), expectedLiteral = "x" },
			new Expected { expectedType = new TokenType(TokenTypes.COMMA), expectedLiteral = "," },
			new Expected { expectedType = new TokenType(TokenTypes.IDENT), expectedLiteral = "y" },
			new Expected { expectedType = new TokenType(TokenTypes.RPAREN), expectedLiteral = ")" },
			new Expected { expectedType = new TokenType(TokenTypes.LBRACE), expectedLiteral = "{" },
			new Expected { expectedType = new TokenType(TokenTypes.IDENT), expectedLiteral = "x" },
			new Expected { expectedType = new TokenType(TokenTypes.PLUS), expectedLiteral = "+" },
			new Expected { expectedType = new TokenType(TokenTypes.IDENT), expectedLiteral = "y" },
			new Expected { expectedType = new TokenType(TokenTypes.SEMICOLON), expectedLiteral = ";" },
			new Expected { expectedType = new TokenType(TokenTypes.RBRACE), expectedLiteral = "}" },
			new Expected { expectedType = new TokenType(TokenTypes.SEMICOLON), expectedLiteral = ";" },
			new Expected { expectedType = new TokenType(TokenTypes.LET), expectedLiteral = "let" },
			new Expected { expectedType = new TokenType(TokenTypes.IDENT), expectedLiteral = "result" },
			new Expected { expectedType = new TokenType(TokenTypes.ASSIGN), expectedLiteral = "=" },
			new Expected { expectedType = new TokenType(TokenTypes.IDENT), expectedLiteral = "add" },
			new Expected { expectedType = new TokenType(TokenTypes.LPAREN), expectedLiteral = "(" },
			new Expected { expectedType = new TokenType(TokenTypes.IDENT), expectedLiteral = "five" },
			new Expected { expectedType = new TokenType(TokenTypes.COMMA), expectedLiteral = "," },
			new Expected { expectedType = new TokenType(TokenTypes.IDENT), expectedLiteral = "ten" },
			new Expected { expectedType = new TokenType(TokenTypes.RPAREN), expectedLiteral = ")" },
			new Expected { expectedType = new TokenType(TokenTypes.SEMICOLON), expectedLiteral = ";" },
			new Expected { expectedType = new TokenType(TokenTypes.EOF), expectedLiteral = "" },
			};

		Lexer lex = Lexer.CreateFromInput(input);

		//Act
		foreach (Expected er in expectedResults)
		{
			var tok = lex.NextToken();
			//Assert
			tok.Type.ShouldBe(er.expectedType);
			tok.Literal.ShouldBe(er.expectedLiteral);
		}

	}

	[Fact]
	public void Lexer_GivenSingleSymbols_ReturnsCorrect()
	{
		//Arrange
		string input = @"!-/*5;
		5 < 10 > 5;
		";

		List<Expected> expectedResults = new() {
			new Expected { expectedType = new TokenType(TokenTypes.BANG), expectedLiteral = "!" },
			new Expected { expectedType = new TokenType(TokenTypes.MINUS), expectedLiteral = "-" },
			new Expected { expectedType = new TokenType(TokenTypes.SLASH), expectedLiteral = "/" },
			new Expected { expectedType = new TokenType(TokenTypes.ASTERISK), expectedLiteral = "*" },
			new Expected { expectedType = new TokenType(TokenTypes.INT), expectedLiteral = "5" },
			new Expected { expectedType = new TokenType(TokenTypes.SEMICOLON), expectedLiteral = ";" },
			new Expected { expectedType = new TokenType(TokenTypes.INT), expectedLiteral = "5" },
			new Expected { expectedType = new TokenType(TokenTypes.LESSTHAN), expectedLiteral = "<" },
			new Expected { expectedType = new TokenType(TokenTypes.INT), expectedLiteral = "10" },
			new Expected { expectedType = new TokenType(TokenTypes.GREATERTHAN), expectedLiteral = ">" },
			new Expected { expectedType = new TokenType(TokenTypes.INT), expectedLiteral = "5" },
			new Expected { expectedType = new TokenType(TokenTypes.SEMICOLON), expectedLiteral = ";" },
			new Expected { expectedType = new TokenType(TokenTypes.EOF), expectedLiteral = "" },
			};

		Lexer lex = Lexer.CreateFromInput(input);

		//Act
		foreach (Expected er in expectedResults)
		{
			var tok = lex.NextToken();
			//Assert
			tok.Type.ShouldBe(er.expectedType);
			tok.Literal.ShouldBe(er.expectedLiteral);
		}

	}

	[Fact]
	public void Lexer_GivenKeywords_ReturnsCorrect()
	{
		//Arrange
		string input = @"if (5 < 10) {
			return true;
		} else {
			return false;
		}
		";

		List<Expected> expectedResults = new() {
			new Expected { expectedType = new TokenType(TokenTypes.IF), expectedLiteral = "if" },
			new Expected { expectedType = new TokenType(TokenTypes.LPAREN), expectedLiteral = "(" },
			new Expected { expectedType = new TokenType(TokenTypes.INT), expectedLiteral = "5" },
			new Expected { expectedType = new TokenType(TokenTypes.LESSTHAN), expectedLiteral = "<" },
			new Expected { expectedType = new TokenType(TokenTypes.INT), expectedLiteral = "10" },
			new Expected { expectedType = new TokenType(TokenTypes.RPAREN), expectedLiteral = ")" },
			new Expected { expectedType = new TokenType(TokenTypes.LBRACE), expectedLiteral = "{" },
			new Expected { expectedType = new TokenType(TokenTypes.RETURN), expectedLiteral = "return" },
			new Expected { expectedType = new TokenType(TokenTypes.TRUE), expectedLiteral = "true" },
			new Expected { expectedType = new TokenType(TokenTypes.SEMICOLON), expectedLiteral = ";" },
			new Expected { expectedType = new TokenType(TokenTypes.RBRACE), expectedLiteral = "}" },
			new Expected { expectedType = new TokenType(TokenTypes.ELSE), expectedLiteral = "else" },
			new Expected { expectedType = new TokenType(TokenTypes.LBRACE), expectedLiteral = "{" },
			new Expected { expectedType = new TokenType(TokenTypes.RETURN), expectedLiteral = "return" },
			new Expected { expectedType = new TokenType(TokenTypes.FALSE), expectedLiteral = "false" },
			new Expected { expectedType = new TokenType(TokenTypes.SEMICOLON), expectedLiteral = ";" },
			new Expected { expectedType = new TokenType(TokenTypes.RBRACE), expectedLiteral = "}" },
			new Expected { expectedType = new TokenType(TokenTypes.EOF), expectedLiteral = "" },
			};

		Lexer lex = Lexer.CreateFromInput(input);

		//Act
		foreach (Expected er in expectedResults)
		{
			var tok = lex.NextToken();
			//Assert
			tok.Type.ShouldBe(er.expectedType);
			tok.Literal.ShouldBe(er.expectedLiteral);
		}

	}

	[Fact]
	public void Lexer_GivenCompoundSymbols_ReturnsCorrect()
	{
		//Arrange
		string input = @"10 == 10;
		10 != 9;
		";

		List<Expected> expectedResults = new() {
			new Expected { expectedType = new TokenType(TokenTypes.INT), expectedLiteral = "10" },
			new Expected { expectedType = new TokenType(TokenTypes.EQ), expectedLiteral = "==" },
			new Expected { expectedType = new TokenType(TokenTypes.INT), expectedLiteral = "10" },
			new Expected { expectedType = new TokenType(TokenTypes.SEMICOLON), expectedLiteral = ";" },
			new Expected { expectedType = new TokenType(TokenTypes.INT), expectedLiteral = "10" },
			new Expected { expectedType = new TokenType(TokenTypes.NOT_EQ), expectedLiteral = "!=" },
			new Expected { expectedType = new TokenType(TokenTypes.INT), expectedLiteral = "9" },
			new Expected { expectedType = new TokenType(TokenTypes.SEMICOLON), expectedLiteral = ";" },
			new Expected { expectedType = new TokenType(TokenTypes.EOF), expectedLiteral = "" },
		};

		Lexer lex = Lexer.CreateFromInput(input);

		//Act
		foreach (Expected er in expectedResults)
		{
			var tok = lex.NextToken();
			//Assert
			tok.Type.ShouldBe(er.expectedType);
			tok.Literal.ShouldBe(er.expectedLiteral);
		}

	}
}