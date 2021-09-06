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
            GameOfLife game = new GameOfLife(content);
            Console.WriteLine(game.ToText());
            while(true)
            {
                Console.ReadKey();
                for(byte i = 0; i < 8; i++) Console.WriteLine("");
                game.Update();
                Console.WriteLine(game.ToText());
            }
        }
    }
}
