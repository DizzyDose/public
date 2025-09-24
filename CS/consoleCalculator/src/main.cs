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
			if (CheckSyntax(ref TokenList))
				ExpressionCalculate(TokenList);
			else
			{
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

		foreach (char character in inputStream)
		{
			if (character == 32)
				continue;
			tokenList.Add(character);
		}

		List<string> tokenListConCat = new List<string>();
		foreach (char character in tokenList)
		{
			switch (character)
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
					if (temp != "") tokenListConCat.Add(temp);
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
	public static void ExpressionCalculate(List<string> tokenizedList)
	{
		double result = 0;

		//
		int[] PriorityTokenPositions = SearchForPriorityTokens(tokenizedList);
		int[] OperandPositions = SearchForOperands(tokenizedList);
		int pos1 = 0; int pos2 = 0;
		double temp;
		while (tokenizedList.Count != 1 && !(tokenizedList.Count == 2 && tokenizedList[1] == "" && tokenizedList[0] != ""))
		{
			for (int i = 0; i < tokenizedList.Count; i++)
			{
				if (PriorityTokenPositions[i] == 3)
				{
					pos1 = i;
				}
				if (PriorityTokenPositions[i] == 4)
				{
					pos2 = i;
					break;
				}
			}

			if (pos1 == pos2) // no brackets
			{
				for (int i = tokenizedList.Count - 1; i > 0; i--)
				{
					if (PriorityTokenPositions[i] == 2)
					{
						temp = Exponent(Convert.ToDouble(tokenizedList[i - 1]), Convert.ToDouble(tokenizedList[i + 1]));
						RemoveAndUpdate(temp, ref tokenizedList, i, ref PriorityTokenPositions, ref OperandPositions);
					}
				}
				OperandPositions = SearchForOperands(tokenizedList);
				for (int i = 0; i < tokenizedList.Count; i++)
				{
					if (PriorityTokenPositions[i] == 1)
					{
						switch (OperandPositions[i])
						{
							case 3:
								temp = Multiply(Convert.ToDouble(tokenizedList[i - 1]), Convert.ToDouble(tokenizedList[i + 1]));
								RemoveAndUpdate(temp, ref tokenizedList, i, ref PriorityTokenPositions, ref OperandPositions);
								break;
							case 4:
								if (Convert.ToDouble(tokenizedList[i + 1]) != 0)
								{
									temp = Divide(Convert.ToDouble(tokenizedList[i - 1]), Convert.ToDouble(tokenizedList[i + 1]));
									RemoveAndUpdate(temp, ref tokenizedList, i, ref PriorityTokenPositions, ref OperandPositions);
								}
								else
								{
									Console.WriteLine("Math Error");
									return;
								}
								break;
						}
					}
				}
				OperandPositions = SearchForOperands(tokenizedList);
				for (int i = 0; i < tokenizedList.Count; i++)
				{
					switch (OperandPositions[i])
					{
						case 1:
							temp = Add(Convert.ToDouble(tokenizedList[i - 1]), Convert.ToDouble(tokenizedList[i + 1]));
							RemoveAndUpdate(temp, ref tokenizedList, i, ref PriorityTokenPositions, ref OperandPositions);
							break;
						case 2:
							temp = Subtract(Convert.ToDouble(tokenizedList[i - 1]), Convert.ToDouble(tokenizedList[i + 1]));
							RemoveAndUpdate(temp, ref tokenizedList, i, ref PriorityTokenPositions, ref OperandPositions);
							break;
					}
				}
			}
			else // with brackets
			{
				bool stop = false;
				CheckIsolatedNegative(ref tokenizedList, ref PriorityTokenPositions, ref OperandPositions);
				for (int i = pos2 - 1; i > pos1 && !stop; i--)
				{
					if (PriorityTokenPositions[i] == 2)
					{
						temp = Exponent(Convert.ToDouble(tokenizedList[i - 1]), Convert.ToDouble(tokenizedList[i + 1]));
						RemoveAndUpdate(temp, ref tokenizedList, i, ref PriorityTokenPositions, ref OperandPositions);
						stop = true;
					}
				}
				OperandPositions = SearchForOperands(tokenizedList);
				for (int i = pos1; i < pos2 && !stop; i++)
				{
					if (PriorityTokenPositions[i] == 1)
					{
						switch (OperandPositions[i])
						{
							case 3:
								temp = Multiply(Convert.ToDouble(tokenizedList[i - 1]), Convert.ToDouble(tokenizedList[i + 1]));
								RemoveAndUpdate(temp, ref tokenizedList, i, ref PriorityTokenPositions, ref OperandPositions);
								stop = true;
								break;
							case 4:
								if (Convert.ToDouble(tokenizedList[i + 1]) != 0)
								{
									temp = Divide(Convert.ToDouble(tokenizedList[i - 1]), Convert.ToDouble(tokenizedList[i + 1]));
									RemoveAndUpdate(temp, ref tokenizedList, i, ref PriorityTokenPositions, ref OperandPositions);
									stop = true;
								}
								else
								{
									Console.WriteLine("Math Error");
									return;
								}
								break;
						}
					}
				}
				OperandPositions = SearchForOperands(tokenizedList);
				for (int i = pos1; i < pos2 && !stop; i++)
				{
					switch (OperandPositions[i])
					{
						case 1:
							temp = Add(Convert.ToDouble(tokenizedList[i - 1]), Convert.ToDouble(tokenizedList[i + 1]));
							RemoveAndUpdate(temp, ref tokenizedList, i, ref PriorityTokenPositions, ref OperandPositions);
							stop = true;
							break;
						case 2:
							temp = Subtract(Convert.ToDouble(tokenizedList[i - 1]), Convert.ToDouble(tokenizedList[i + 1]));
							RemoveAndUpdate(temp, ref tokenizedList, i, ref PriorityTokenPositions, ref OperandPositions);
							stop = true;
							break;
					}
				}
				for (int i = 0; i < tokenizedList.Count; i++)
				{
					if (PriorityTokenPositions[i] == 3)
					{
						pos1 = i;
					}
					if (PriorityTokenPositions[i] == 4)
					{
						pos2 = i;
						break;
					}
				}
				if (pos2 - pos1 == 2) RemoveAndUpdateBrackets(ref tokenizedList, ref PriorityTokenPositions, ref OperandPositions, pos1, pos2);
				pos1 = 0; pos2 = 0;
			}
		}
		result = Convert.ToDouble(tokenizedList[0]);

		//

		Console.WriteLine(result);
	}
	public static int[] SearchForOperands(List<string> tokenizedList)
	{
		int[] OperandPositions = new int[tokenizedList.Count];
		for (int i = 0; i < tokenizedList.Count; i++)
		{
			switch (tokenizedList[i])
			{
				case "+":
					OperandPositions[i] = 1;
					break;
				case "-":
					OperandPositions[i] = 2;
					break;
				case "*":
					OperandPositions[i] = 3;
					break;
				case "/":
					OperandPositions[i] = 4;
					break;
				case "^":
					OperandPositions[i] = 5;
					break;
			}
		}
		return OperandPositions;
	}
	public static int[] SearchForPairedTokens(List<string> tokenizedList)
	{
		int[] positions = new int[tokenizedList.Count + 1]; for (int i = 0; i < tokenizedList.Count; i++) { positions[i] = 0; }
		string[] keys = { "(", "[", "{", ")", "]", "}" }; //1, 2, 3, 4, 5, 6
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
					PriorityTokenPositions[i] = 3;
					break;
				case 4:
				case 5:
				case 6:
					PriorityTokenPositions[i] = 4;
					break;
			}
		}
		return PriorityTokenPositions;
	}

	public static bool CheckSyntax(ref List<string> tokenizedList)
	{
		string[] keys = { "+", "^", "*", "/", "-" };

		for (int i = 0; i < keys.Length; i++) // check if ends with +-*/^
		{
			if (tokenizedList[tokenizedList.Count - 2] == keys[i] && tokenizedList[tokenizedList.Count - 1] == "")
			{
				return false;
			}
		}



		for (int i = 0; i < tokenizedList.Count - 1; i++)
		{
			for (int j = 0; j < keys.Length; j++)
			{
				for (int n = 0; n < keys.Length; n++)
				{
					if (tokenizedList[i] == keys[j] && tokenizedList[i + 1] == keys[n])
						if (n == 4)
						{
							string temp = "-" + tokenizedList[i + 2];
							tokenizedList.RemoveRange(i + 1, 2);
							tokenizedList.Insert(i + 1, temp);
						}
				}
			}
		}
		if (SearchForPairedTokens(tokenizedList)[tokenizedList.Count] == -1) return false;
		return true;
	}
	public static void RemoveAndUpdate(double temp, ref List<string> tokenizedList, int i, ref int[] PriorityTokenPositions, ref int[] OperandPositions)
	{
		tokenizedList.RemoveRange(i - 1, 3);
		tokenizedList.Insert(i - 1, Convert.ToString(temp));
		PriorityTokenPositions = SearchForPriorityTokens(tokenizedList);
		OperandPositions = SearchForOperands(tokenizedList);
	}
	public static void RemoveAndUpdateBrackets(ref List<string> tokenizedList, ref int[] PriorityTokenPositions, ref int[] OperandPositions, int pos1, int pos2)
	{
		tokenizedList.RemoveAt(pos2);
		tokenizedList.RemoveAt(pos1);
		PriorityTokenPositions = SearchForPriorityTokens(tokenizedList);
		OperandPositions = SearchForOperands(tokenizedList);

	}
	public static void CheckIsolatedNegative(ref List<string> tokenizedList, ref int[] PriorityTokenPositions, ref int[] OperandPositions)
	{
		for (int i = 0; i < tokenizedList.Count - 3; i++)
		{
			if (PriorityTokenPositions[i] == 3 && tokenizedList[i + 1] == "-" && PriorityTokenPositions[i + 3] == 4)
			{
				string temp = tokenizedList[i + 2];
				tokenizedList.RemoveRange(i + 1, 2);
				tokenizedList.Insert(i + 1, "-" + temp);
				PriorityTokenPositions = SearchForPriorityTokens(tokenizedList);
				OperandPositions = SearchForOperands(tokenizedList);
			}
		}
	}
}