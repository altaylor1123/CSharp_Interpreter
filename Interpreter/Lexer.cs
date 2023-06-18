namespace Interpreter;

public class Lexer
{
	public string Input { get; set; }
	public int Position { get; set; } // current position in the input (points to current char)
	public int ReadPosition { get; set; } // current reading position in input (after current char)
	public char CurrentChar { get; set; } // current char under examination

	private Lexer(string input)
	{
		Input = input;
	}

	public static Lexer CreateFromInput(string input)
	{
		var lexer = new Lexer(input);
		lexer.ReadChar();
		return lexer;
	}

	public void ReadChar()
	{
		if (ReadPosition >= Input.Length)
		{
			CurrentChar = '\0';
		}
		else
		{
			CurrentChar = Input[ReadPosition];
		}
		Position = ReadPosition;
		ReadPosition++;
	}

	public Token NextToken()
	{
		Token tok = CurrentChar switch
		{
			'=' => new Token(new TokenType(TokenTypes.ASSIGN), CurrentChar.ToString()),
			';' => new Token(new TokenType(TokenTypes.SEMICOLON), CurrentChar.ToString()),
			'(' => new Token(new TokenType(TokenTypes.LPAREN), CurrentChar.ToString()),
			')' => new Token(new TokenType(TokenTypes.RPAREN), CurrentChar.ToString()),
			',' => new Token(new TokenType(TokenTypes.COMMA), CurrentChar.ToString()),
			'+' => new Token(new TokenType(TokenTypes.PLUS), CurrentChar.ToString()),
			'{' => new Token(new TokenType(TokenTypes.LBRACE), CurrentChar.ToString()),
			'}' => new Token(new TokenType(TokenTypes.RBRACE), CurrentChar.ToString()),
			'\0' => new Token(new TokenType(TokenTypes.EOF), ""),
			_ => throw new ArgumentOutOfRangeException()
		};

		ReadChar();
		return tok;

	}
}