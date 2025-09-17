using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Forms;
  
public class Program 
{ 
	public static void Main(string[] args) 
	{ 
		
		Point punto;
		do {
			if(GetCursorPos(out punto) && !punto.Previous())
				Console.Write("X: " + punto.X + ", Y: " + punto.Y + "\r");
		} while(!Console.KeyAvailable);
		Process p = Process.GetProcessesByName("Firefox")[0];
		if(p != null)
		{
			IntPtr h = p.MainWindowHandle;
			SetForegroundWindow(h);
			SendKeys.SendWait("k");
		}
	} 
	public struct Point
	{
		public int X;
		public int Y;
 		private int pY; private int pX;
		public bool Previous()
		{
			if( pX == X && pY == Y ) return true;
			return false;
		}
	}
	[DllImport("user32.dll")]
	public static extern bool GetCursorPos(out Point lpPoint);
	[DllImport("User32-dll")]
	public static extern bool SetForegroundWindow(IntPtr point);
} 
