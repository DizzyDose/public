#pragma warning disable CA1416, CS8600
using System;
using System.Collections.Generic;
using System.Threading;

public class Program
{
	public static void Main(string[] args)
	{
		
		int posx = 10;
		int posy = 0;
		while (true)
		{			
			posx = 10;
			Console.Write("password: ");
			string password = null;
			while (true)
			{
				var key = Console.ReadKey(true);
				if (key.Key == ConsoleKey.Enter)
				{
					posy += 2;
					break;
				}
				if (key.Key == ConsoleKey.Backspace && password != null)
				{
					string tempPass = null;
					Console.SetCursorPosition(posx-1, posy);
					Console.Write(" ");
					Console.SetCursorPosition(posx-1, posy);
					for(int i = 0; i < password.Length - 1; i++)
					{
						tempPass += password[i];
					}
					password = tempPass;
					posx--;
					continue;
				}				
				Console.Write('*');
				password += key.KeyChar;
				posx++;
			}

			string secret = "password1";
			if (password != null && password == secret)
			{
				Console.WriteLine("\nStarting application...");
				Thread.Sleep(400);
				break;
			}
			else
			{
				Console.WriteLine("\nWrong password");
			}
		}
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
			if(CheckSyntax(TokenList);
				Console.WriteLine(ExpressionCalculate(TokenList, 0, TokenList.Count));
			else
				Console.WriteLine("Syntax error");
			
        	}
	
		
	}
	public static double Add(double a, double b)
	{
		return a+b;	
	}
	public static double Sub(double a, double b)
	{
		return a-b;
	}
	public static double Mult(double a, double b)
	{
		return a*b;
	}
	public static double Div(double a, double b)
	{
		return a/b;
	}
	public static void ConsoleSetup()
	{
		Console.SetWindowSize(1,1);
		Console.SetBufferSize(120,120);
		Console.SetWindowSize(75,18);
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
		
		return tokenListConCat;
	}
	public static double ExpressionCalculate(List<string> tokenizedList, int startIndex, int lastIndex)
	{
		double result = 0;

		//
		
			
	
		//

		return result;
	}
	public static int[] SearchForSpecificTokens(List<string> tokenizedList, List<string> seekedTokens, bool pairedToken = false)
	{
		for(int i = 0; i < tokenizedList.Count; i++)
		{
			
		}
	}

	public static bool CheckSyntax(List<string> tokenizedList)
	{
		string[] keys = { "+", "^", "*", "/", "-" };
		for(int i = 0; i < tokenizedList.Count-1; i++)
		{
			for(int j = 0; j < keys.Length; j++)
			{
				for(int n = 0; n < keys.Length-1; n++)
				{
					if(tokenizedList[i] == keys[j] && tokenizedList[i+1] == keys[n])
						return false;
				}
			}
		}
		return true;
	}
}
