using System;

public class Game
{
	public int Round = 1;
	public Game()
	{
		NewGame();
	}
	
	private void NextRound()
	{
		switch(Round)
		{
			case 1:
				Round++;
				break;
			case 2:
				Round--;
				//NewGame();
				break;
			default:
				Console.WriteLine("Nìco je špatnì");
				break;
		}
	}
	private string testString;
	public void NewGame()
	{
		Restart();
	}
	public void Restart()
	{
		//reset all variables to default state
	}
	public void Gaming()
	{

	}
	private void Test()
	{
		
	}
}