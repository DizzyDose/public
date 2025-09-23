using System;
using System.Collections.Generic;
using System.Threading;

public class Program
{
	public static void Main(string[] args)
	{		
		ConsoleSetup();

		List<string> TokenList = new List<string>();
		
		while (true)
		{
			Console.WriteLine("Console Calculator, please write the expression below to be calculated:");
			string inputStream = Console.ReadLine();
			if (inputStream != null)
				TokenList = Tokenize(inputStream);
			foreach (string token in TokenList)
				Console.WriteLine(token);
			if (CheckSyntax(TokenList))
				Console.WriteLine(ExpressionCalculate(TokenList, 0, TokenList.Count));
			else {
				Console.WriteLine("Syntax error");
			}
		}

	}
	public static double Add(double a, double b)
	{
		return a + b;
	}
	public static double Subtract(double a, double b)
	{
		return a - b;
	}
	public static double Multiply(double a, double b)
	{
		return a * b;
	}
	public static double Divide(double a, double b)
	{
		return a / b;
	}
	public static double Exponent(double a, double b)
	{
		return Math.Pow(a, b);
	}
	public static void ConsoleSetup()
	{
		Console.SetWindowSize(1, 1);
		Console.SetBufferSize(120, 120);
		Console.SetWindowSize(75, 18);
		Console.BackgroundColor = ConsoleColor.DarkBlue;
		Console.ForegroundColor = ConsoleColor.White;
		Console.Title = "Calculator";
		Console.Clear();
	}
	public static List<string> Tokenize(string inputStream)
	{
		string temp = "";
		List<char> tokenList = new List<char>();
		
		foreach(char character in inputStream)
		{
			if(character == 32)
				continue;
			tokenList.Add(character);
		}
		
		List<string> tokenListConCat = new List<string>();
		foreach(char character in tokenList)
		{
			switch(character)
			{
				case '+':
				case '-':
				case '/':
				case '*':
				case '(':
				case ')':
				case '{':
				case '}':
				case '[':
				case ']':
				case '^':
					if(temp != "") tokenListConCat.Add(temp);
					temp = "";
					tokenListConCat.Add(character.ToString());
					break;
				default:
					temp += character.ToString();
					break;
				
			}
		}
		tokenListConCat.Add(temp); //add last missing token
		
		return tokenListConCat;
	}
	public static double ExpressionCalculate(List<string> tokenizedList, int startIndex, int lastIndex)
	{
		double result = 0;

		//

		int[] positions = SearchForPriorityTokens(tokenizedList);

		
		
		//

		return result;
	}
	public static int[] SearchForPairedTokens(List<string> tokenizedList)
	{
		int[] positions = new int[tokenizedList.Count+1]; for (int i = 0; i< tokenizedList.Count; i++) { positions[i] = 0; } 
		string[] keys = { "(", "[", "{" , ")", "]", "}" }; //1, 2, 3, 4, 5, 6
		int[] keyPairs = new int[6];
		for (int i = 0; i < tokenizedList.Count; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				if (tokenizedList[i] == keys[j]) { positions[i] = j + 1; keyPairs[j]++; }
			}
		}
		if (keyPairs[0] != keyPairs[3]) { Console.WriteLine("Syntax error, missing )?"); positions[positions.Length - 1] = -1; }
		if (keyPairs[1] != keyPairs[4]) { Console.WriteLine("Syntax error, missing ]?"); positions[positions.Length - 1] = -1; }
		if (keyPairs[2] != keyPairs[5]) { Console.WriteLine("Syntax error, missing }?"); positions[positions.Length - 1] = -1; }

		//

		for (int i = 0; i < positions.Length; i++)
		{
			if (positions[i] != 0)
			{
				Console.WriteLine(i + " " + positions[i]);
			}
		}

		//

		return positions;
	}

	public static int[] SearchForPriorityTokens(List<string> tokenizedList) //PEMDAS Parantheses Exponents Multiplication/Division Addition/Subtraction
	{                                                                       //BODMAS Brackets Orders Division/Multiplication Addition/Subtraction
		int[] PriorityTokenPositions = new int[tokenizedList.Count];
		for (int i = 0; i < tokenizedList.Count; i++)
		{
			switch (tokenizedList[i])
			{
				case "*":
				case "/":
					PriorityTokenPositions[i] = 1;
					break;
				case "^":
					PriorityTokenPositions[i] = 2;
					break;
			}
		}
		int[] PairedTokenPositions = SearchForPairedTokens(tokenizedList);
		for (int i = 0; i < tokenizedList.Count; i++)
		{
			switch (PairedTokenPositions[i])
			{
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
					PriorityTokenPositions[i] = 3;
					break;
			}
		}
		return PriorityTokenPositions;
	}

	public static bool CheckSyntax(List<string> tokenizedList)
	{
		//TODO>add pair check
		string[] keys = { "+", "^", "*", "/", "-" };
		for (int i = 0; i < tokenizedList.Count - 1; i++)
		{
			for (int j = 0; j < keys.Length; j++)
			{
				for (int n = 0; n < keys.Length - 1; n++)
				{
					if (tokenizedList[i] == keys[j] && tokenizedList[i + 1] == keys[n])
						return false;
				}
			}
		}
		if (SearchForPairedTokens(tokenizedList)[tokenizedList.Count] == -1) return false;
		return true;
	}
}