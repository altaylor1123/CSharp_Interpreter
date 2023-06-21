# Monkey Langauge C# Interpreter

Using ['Writing an Interpreter in Go'](https://interpreterbook.com/) as a reference, building a interpreter for a fictional Monkey language with C#. As the book is written for Golang, the challenge is learning about programming interpreters while reading Go and implementing the concepts with C#.

As described in the book, Monkey languge has the following:

- C-like syntax
- variable bindings
- intergers and booleans
- arithmetic expressions
- built-in functions
- first-class and higher-order functions
- closures
- a string data structure
- an array data strucutre
- a hash data structure

The interpreter will have a few major parts:

- the lexer 🚧
- the parser
- the Abstract Syntax Tree (AST)
- the internal object system
- the evaluator
