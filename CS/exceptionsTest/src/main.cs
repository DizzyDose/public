using System;

public class Program
    {
        public static void Main(string[] args)
        {
            try
            {

                if (args.Length == 0)
                {
                    throw new Exception("no input has been given");
                }
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
            }

        }
    }

