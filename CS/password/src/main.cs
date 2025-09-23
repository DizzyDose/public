using System; 
  
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
					Console.SetCursorPosition(posx - 1, posy);
					Console.Write(" ");
					Console.SetCursorPosition(posx - 1, posy);
					for (int i = 0; i < password.Length - 1; i++)
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
	} 
} 
