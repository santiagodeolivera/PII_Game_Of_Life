using System;
using System.IO;

namespace PII_Game_Of_Life
{
	class Program
	{
		static void Main(string[] args)
		{
			// Get the content of the file
			string url = @"../../assets/board.txt";
			string content = File.ReadAllText(url);

			// Transform the content into a string
			bool[,] initialState = Utils.MatrixFromString(content);

			// Initialize the game
			GameOfLife game = new GameOfLife(initialState);
			Console.WriteLine(Utils.MatrixToString(game.Matrix));
			while(true)
			{
				// The game of life advances every time a key is pressed
				ConsoleKeyInfo keyInfo = Console.ReadKey(true);

				// If the key is 'q', the program stops
				if (keyInfo.Key == ConsoleKey.Q) break;
				
				game.Update();
				for(byte i = 0; i < 8; i++) Console.WriteLine("");
				Console.WriteLine(Utils.MatrixToString(game.Matrix));
			}
		}
	}
}
