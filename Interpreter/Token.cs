using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace Interpreter;

public class Token
{
	public TokenType Type { get; set; }
	public string Literal { get; set; }

	public Token(TokenType type, string literal)
	{
		Type = type;
		Literal = literal;
	}

	readonly static Dictionary<string, TokenType> Keywords = new(){
		{"fn", new TokenType(TokenTypes.FUNCTION)},
		{"let", new TokenType(TokenTypes.LET)},
	};

	public static TokenType LookupIdent(string ident)
	{
		if (Keywords.ContainsKey(ident))
		{
			return Keywords[ident];
		}
		return new TokenType(TokenTypes.IDENT);
	}
}

public record TokenType(string Value);

public static class TokenTypes
{
	public static readonly string ILLEGAL = "ILLEGAL";
	public static readonly string EOF = "EOF";

	//Identifiers + literals
	public static readonly string IDENT = "IDENT"; // add, foobar, x, y....
	public static readonly string INT = "INT";

	//Operators
	public static readonly string ASSIGN = "=";
	public static readonly string PLUS = "+";

	//Delimiters
	public static readonly string COMMA = ",";
	public static readonly string SEMICOLON = ";";

	public static readonly string LPAREN = "(";
	public static readonly string RPAREN = ")";
	public static readonly string LBRACE = "{";
	public static readonly string RBRACE = "}";

	//Keywords
	public static readonly string FUNCTION = "FUNCTION";
	public static readonly string LET = "LET";
}