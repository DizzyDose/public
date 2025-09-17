using System; 
  
public class Program 
{ 
	public static void Main(string[] args) 
	{
        double buy; double sell; double tax = 0.05; double recieve;
        string input;
        bool end = false;
        do
        {
            Console.Write("enter the price of an item: ");
            input = Console.ReadLine();
            if (input == "Q") break;
            buy = double.Parse(input); sell = buy;
            while (sell - sell * tax < buy) sell++;
            recieve = sell - sell * tax;
            Console.WriteLine("you must sell at minimum of: " + sell + ", " + "to recieve: " + recieve);
        } while (!end);

    }
} 
