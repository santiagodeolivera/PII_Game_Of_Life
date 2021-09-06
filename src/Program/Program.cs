using System;
using System.IO;

namespace PII_Game_Of_Life
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = @"../../assets/board.txt";
            string content = File.ReadAllText(url);
            bool[,] initialState = Utils.MatrixFromString(content);
            GameOfLife game = new GameOfLife(initialState);
            Console.WriteLine(game.ToText());
            while(true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Q) break;
                for(byte i = 0; i < 8; i++) Console.WriteLine("");
                game.Update();
                Console.WriteLine(game.ToText());
            }
        }
    }
}
