using System; 
  
public class Program 
{ 
	public static void Main(string[] args) 
	{ 
		string str = "Mexické hlavní město Mexico City se propadá do země. Důvodem je skutečnost, že město je postaveno na jezeře Texcoco, které bylo postupně vysušeno. Živelná porodnost a migrace dostávají Mexiko do začarovaného kruhu. Silná religiozita většiny populace způsobuje, že antikoncepce a interrupce jsou fenomény zapovězené. Počet obyvatel v Mexiku se každé čtvrtstoletí zdvojnásobuje.";
		string str2 = "";
		for(int i = str.Length-1; i >= 0; i--)
		{
			str2 += str[i];
		}
		Console.WriteLine(str);
		Console.WriteLine();
		Console.WriteLine(str2);
		Console.ReadKey();
	} 
} 
