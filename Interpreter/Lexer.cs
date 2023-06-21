using System.Diagnostics;
using System.Text;

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

	private void ReadChar()
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
		SkipWhitespace();

		Token tok = CurrentChar switch
		{

			'=' when PeekChar().Equals('=') => GetTwoCharToken(TokenTypes.EQ),
			'=' => GetToken(TokenTypes.ASSIGN, CurrentChar.ToString()),
			'+' => GetToken(TokenTypes.PLUS, CurrentChar.ToString()),
			'-' => GetToken(TokenTypes.MINUS, CurrentChar.ToString()),
			'!' when PeekChar().Equals('=') => GetTwoCharToken(TokenTypes.NOT_EQ),
			'!' => GetToken(TokenTypes.BANG, CurrentChar.ToString()),
			'/' => GetToken(TokenTypes.SLASH, CurrentChar.ToString()),
			'*' => GetToken(TokenTypes.ASTERISK, CurrentChar.ToString()),
			'<' => GetToken(TokenTypes.LESSTHAN, CurrentChar.ToString()),
			'>' => GetToken(TokenTypes.GREATERTHAN, CurrentChar.ToString()),
			';' => GetToken(TokenTypes.SEMICOLON, CurrentChar.ToString()),
			'(' => GetToken(TokenTypes.LPAREN, CurrentChar.ToString()),
			')' => GetToken(TokenTypes.RPAREN, CurrentChar.ToString()),
			',' => GetToken(TokenTypes.COMMA, CurrentChar.ToString()),
			'{' => GetToken(TokenTypes.LBRACE, CurrentChar.ToString()),
			'}' => GetToken(TokenTypes.RBRACE, CurrentChar.ToString()),
			'\0' => GetToken(TokenTypes.EOF, ""),
			_ when IsLetter(CurrentChar) => GetIdentToken(),
			_ when IsDigit(CurrentChar) => GetNumberToken(),
			_ => new Token(new TokenType(TokenTypes.ILLEGAL), CurrentChar.ToString())
		};

		// ReadChar();
		return tok;

	}

	private Token GetToken(string toktype, string tokString)
	{
		ReadChar();
		var tokType = new TokenType(toktype);
		var tokLiteral = tokString;
		return new Token(tokType, tokLiteral);
	}

	private Token GetTwoCharToken(string tokenType)
	{
		var ch = CurrentChar;
		ReadChar();
		var tokType = new TokenType(tokenType);
		var tokLiteral = $"{ch}{CurrentChar}";
		ReadChar();
		return new Token(tokType, tokLiteral);
	}
	private Token GetIdentToken()
	{
		var tokLiteral = ReadIdentifier();
		var tokType = Token.LookupIdent(tokLiteral);
		return new Token(tokType, tokLiteral);
	}
	private Token GetNumberToken()
	{
		var tokenType = new TokenType(TokenTypes.INT);
		var tokLiteral = ReadNumber();
		return new Token(tokenType, tokLiteral);
	}

	// reads in an identifier and advances the lexer's position until it encounters 
	// a non-letter-character
	private string ReadIdentifier()
	{
		var builder = new StringBuilder();
		while (IsLetter(CurrentChar))
		{
			builder.Append(CurrentChar);
			ReadChar();
		}
		return builder.ToString();
	}

	private string ReadNumber()
	{
		var builder = new StringBuilder();
		while (IsDigit(CurrentChar))
		{
			builder.Append(CurrentChar);
			ReadChar();
		}
		return builder.ToString();
	}

	// helper function checks whether given argument is a letter
	private static bool IsLetter(char ch)
	{
		return 'a' <= ch && ch <= 'z' || 'A' <= ch && ch <= 'Z' || ch == '_';
	}

	private static bool IsDigit(char ch)
	{
		return '0' <= ch && ch <= '9';
	}
	// helper function common in parsers, eatWhitespace or consumeWhitespace
	private void SkipWhitespace()
	{
		while (CurrentChar == ' ' || CurrentChar == '\t' || CurrentChar == '\r' || CurrentChar == '\n')
		{
			ReadChar();
		}
	}

	private char PeekChar()
	{
		if (ReadPosition >= Input.Length)
		{
			return '\0';
		}
		else
		{
			return Input[ReadPosition];
		}
	}
}