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
			bool[,] initialState = CellUtils.MatrixFromString(content);

			// Initialize the game
			GameOfLife game = new GameOfLife(initialState);
			
			// Print initial state
			Console.Clear();
			Console.WriteLine(CellUtils.MatrixToString(game.Matrix));
			
			while(true)
			{
				// The game of life advances every time a key is pressed
				ConsoleKeyInfo keyInfo = Console.ReadKey(true);

				// If the key is 'q', the program stops
				if (keyInfo.Key == ConsoleKey.Q) break;
				
				// Update the game
				game.Update();

				Console.Clear();

				// Print the next generation of cells
				Console.WriteLine(CellUtils.MatrixToString(game.Matrix));
			}
		}
	}
}
