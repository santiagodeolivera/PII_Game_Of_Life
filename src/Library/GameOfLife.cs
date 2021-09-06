using System;
using System.Text;

namespace PII_Game_Of_Life
{
	/// <summary>
	/// This class represents a Game of Life board.
	/// It follows the Expert pattern because its responsibilities are associated
	/// to the cell matrix, which it's an expert of.
	/// </summary>
	public class GameOfLife
	{
		private bool[,] matrix;

		public int Width { get => matrix.GetLength(0); }
		public int Height { get => matrix.GetLength(1); }

		/// <summary>
		/// Build a `GameOfLife` object given a string representing the cell matrix.
		/// It follows the SRP principle because it receives a string, instead of a file path, to do the job.
		/// </summary>
		/// <param name="data">The string representing the cell matrix</param>
		public GameOfLife(string data)
		{
			data = data.Replace("\r", "");
			// Divide the data string into lines
			string[] lines = data.Split('\n');

			// Determine height
			if (lines.Length == 0) throw new ArgumentException("The height of the matrix is 0");
			int height = lines.Length;

			// Determine width
			int width = lines[0].Length;
			foreach (string line in lines)
				if (line.Length != width)
					throw new ArgumentException("The rows must have equal width");
			if(width == 0) throw new ArgumentException("The height of the matrix is 0");

			this.matrix = new bool[width, height];

			// Set state of cells according to data
			for(int i = 0; i < this.Width; i++)
			{
				for(int j = 0; j < this.Height; j++)
				{
					char c = lines[j][i];
					switch (c)
					{
						case '0':
							matrix[i, j] = false;
							break;
						case '1':
							matrix[i, j] = true;
							break;
						default:
							throw new ArgumentException("The cells must be 0 or 1");
					}
				}
			}
		}

		/// <summary>
		/// Updates the cell matrix to the next generation.
		/// </summary>
		public void Update()
		{
			bool[,] buffer = new bool[this.Width, this.Height];

			for(int i = 0; i < this.Width; i++)
			{
				for(int j = 0; j < this.Height; j++)
				{
					byte aliveNeighbors = 0;
					for(int k = i - 1; k <= i + 1; k++)
					{
						for(int l = j - 1; l <= j + 1; l++)
						{
							if (
								k >= 0 && l >= 0
								&& k < this.Width
								&& l < this.Height
								&& this.matrix[k, l]
							) aliveNeighbors++;
						}
					}
					if (this.matrix[i, j]) aliveNeighbors--;

					buffer[i, j] = Utils.IsAliveInNextGeneration(this.matrix[i, j], aliveNeighbors);
				}
			}
			this.matrix = buffer;
		}

		/// <summary>
		/// Returns a representative string of the cell matrix's current state.
		/// </summary>
		/// <returns>A representative string of the cell matrix's current state.</returns>
		public string ToText()
		{
			StringBuilder builder = new StringBuilder("");
			for(int j = 0; j < this.Height; j++)
			{
				for(int i = 0; i < this.Width; i++)
				{
					builder.Append(this.matrix[i, j] ? "|X|" : "---");
				}
				builder.Append("\n");
			}
			return builder.ToString();
		}
	}
}
