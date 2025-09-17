using System; 
  
public class Program 
{ 
	public static void Main(string[] args) 
	{
		Test(); //Remove when done
		Game g = new Game();
		g.NewGame();
	}
	public static void Test()
	{
		DiceSet dice = new DiceSet(6);
		for(int i = 0; i<5; i++)
		{
			if(DiceSet.RemainingRolls > 0)
			{
				dice.Roll();
				for(int j = 0; j < 6; j++)
				{
					if(j != 5) {Console.Write("{0},", dice.diceset[j].lastRoll); continue;}
					Console.WriteLine("{0}", dice.diceset[j].lastRoll);
				}
			}
			else
			{
				Console.WriteLine("nemůžeš házet touto kostkou");
			}
		}
		Console.ReadKey();		
	}
} 
