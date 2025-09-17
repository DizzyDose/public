using System;

public class Dice
{
	private Random r;
	private int m_sides;
	public int lastRoll {get;private set;}
	
	public bool Kept { get; private set;}
	public Dice(int sides, int seed)
	{
		r = new Random(seed);
		m_sides = sides;
		Kept = false;
	}
	public int Roll(int RemainingRolls)
	{
		if(!Kept && RemainingRolls > 0)
		{
			lastRoll = r.Next(1, m_sides+1);
		}
		return lastRoll;
	}
}

public class DiceSet
{
	private Random rand = new Random();
	public Dice[] diceset = new Dice[6];
	
	public static int RemainingRolls {get;set;}
	public DiceSet(int sides)
	{
		for(int i = 0; i < 6; i++)
		{
			diceset[i] = new Dice(sides, rand.Next(1,1000000));
		}
		RemainingRolls = 5;
		
	}

	public int[] Roll()
	{
		int[] myintarr = new int[6];
		for(int i = 0; i < 6; i++)
		{
			myintarr[i] = diceset[i].Roll(RemainingRolls);
		}
		RemainingRolls = RemainingRolls-1;
		return myintarr;
	}
	





}