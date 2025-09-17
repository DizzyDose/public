using System;

namespace mean
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Random rand = new Random();
            double drop_chance = 0.15;
            int required_books = 36;
            double book_chance = 1 / 6;

            double highest = 0;
            double lowest = 300000;

            double treasures = 0;
            int books = 0;
            for (int j = 0; j < 100; j++)
            {
                for (int i = 0; i < 1000; i++)
                {

                    while (books < required_books)
                    {
                        treasures += 1;
                        if (rand.Next(1, 101) <= 15 && rand.Next(1,11) == 1)
                        {
                            books++;
                        }
                    }
                    books = 0;
                    
                }

                //Console.WriteLine(treasures / 1_000);
                if (treasures/1000 > highest)
                    highest = treasures/1000;
                if (treasures/1000 < lowest)
                    lowest = treasures/1000;
                treasures = 0;
            }

            Console.WriteLine(lowest);
            Console.WriteLine(highest);
        }
    }
}
