using System;

namespace cost
{
    class Program
    {
        const int StoneStartingCost = 15;
        const int StoneAdditionalCost = 5;
        const int RuneStartingCost = 150;
        const int RuneAdditionalCost = 50;
        static int SkullLevel = 15;
        //const int Stones = 10;
        //const int Runes = 5;
        static void Main(string[] args)
        {
            int[] Stones = { 10, 10, 100, 100, 10, 10, 10, 10, 84, 100 };
            int[] Runes = { 1, 10, 1, 1, 2 };
            Console.WriteLine(GetNextSkullCost(Stones, Runes));
            Console.ReadKey();
            Console.WriteLine(GetRuneCost(0, 150));
            Console.WriteLine(GetStoneCost(0, 1500));
        }
        static int GetStoneCost(int StartingLevel, int EndingLevel)
        {
            int level = 0;
            int LocalCost = StoneStartingCost;
            int Totalcost = 0;
            while (level < StartingLevel)
            {
                level++;
                LocalCost += StoneAdditionalCost;
            }
            while(level < EndingLevel)
            {
                level++;
                LocalCost += StoneAdditionalCost;
                Totalcost += LocalCost;
            }

            return Totalcost;
        }
        static int GetRuneCost(int StartingLevel, int EndingLevel)
        {
            int level = 0;
            int LocalCost = RuneStartingCost;
            int Totalcost = 0;
            while (level < StartingLevel)
            {
                level++;
                LocalCost += RuneAdditionalCost;
            }
            while (level < EndingLevel)
            {
                level++;
                LocalCost += RuneAdditionalCost;
                Totalcost += LocalCost;
            }

            return Totalcost;
        }
        static int GetNextStoneLevel(int Stone)
        {
            int nextLevel;
            if (Stone / 100 > SkullLevel)
                nextLevel = (Stone / 100) * 100;
            else
                nextLevel = (Stone / 100 + 1 ) * 100 * (SkullLevel + 1);
            return nextLevel;
        }
        static int GetNextRuneLevel(int Rune)
        {
            int nextLevel;
            if (Rune / 10 > SkullLevel)
                nextLevel = (Rune / 10) * 10;
            else
                nextLevel = (Rune / 10 + 1) * 10 * (SkullLevel + 1);
            return nextLevel;
        }
        static int GetNextSkullCost(int[] Stones, int[] Runes)
        {
            int NextSkullCost = 0;

            foreach(int stone in Stones)
            {
                NextSkullCost += GetStoneCost(stone, GetNextStoneLevel(stone));
            }
            foreach(int rune in Runes)
            {
                NextSkullCost += GetRuneCost(rune, GetNextRuneLevel(rune));
            }

            return NextSkullCost;
        }
    }
}
