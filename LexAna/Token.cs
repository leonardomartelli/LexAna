﻿namespace LexAna
{
    public enum Token
	{
		LexicalError = -1,
		EOF,
		ReservedWord,
		Identifier,
		IntegerConstant,
		FloatingPointConstant,

		//Aritmethical Operators
		Plus,				// +
		Minus,				// -
		Product,			// *
		Division,			// /
		Power,				// ˆ
		Increment,			// ++
		Decrement,			// --
		Module,				// %

		//Logical Operators
		Equals,				// ==
		NotEquals,			// !=
		Less,				// <
		Greater,			// >
		LessOrEqual,			// <=
		GreaterOrEqual,		// >=
		LogicalAnd,			// &&
		LogicalOr,			// ||
		LogicalNot,			// !

		//Bitwise Operators
		ShiftLeft,			// <<
		ShiftRight,			// >>
		And,				// &
		Or,					// |

		//Assignment Operators
		Assign,				// =
		PlusAssign,			// +=
		MinusAssign,		// -=
		ProductAssign,		// *=
		DivisionAssign,		// /=
		ModuleAssign,       // %=

        //Language Commands
        For,                // for
        DoWhile,			// do {} while
		While,				// while
		If,					// if
		ElseIf,				// else if
		Else,				// else
		Continue,			// continue
		Break,				// break
		Return,				// return

		//Switch-case Commands
		Switch,				// switch
		Case,				// case
		Default,			// default

		//Primitive types
		Int,				// int
		Char,				// char
		Double,				// double
		Float,				// float
		Struct,				// struct
		Void,				// void

		//Pointers
		Accessor,			// ->

		//Characters
		Underscore,			// _
		Dot,				// .
		Comma,				// ,
		SemiCollon,			// ;
		Collon,				// :
		ParenthesisOpen,	// (
		ParenthesisClose,	// )
		BracketOpen,		// [
		BracketClose,		// ]
		BraceOpen,			// {
		BraceClose,			// }
	}
}

