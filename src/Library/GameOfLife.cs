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

		public bool[,] Matrix { get => matrix; }
		public int Width { get => matrix.GetLength(0); }
		public int Height { get => matrix.GetLength(1); }

		/// <summary>
		/// Build a `GameOfLife` object given a matrix of booleans as the initial state.
		/// It follows the SRP principle because it receives a string, instead of a file path, to do the job.
		/// </summary>
		/// <param name="data">The initial matrix of booleans.</param>
		public GameOfLife(bool[,] matrix)
		{
			this.matrix = matrix;
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
		/// <param name="aliveStr">The string which represents an alive cell. "|X|" by default.</param>
		/// <param name="deadStr">The string which represents a dead cell. A concatenation of '-' chars equally as long as `aliveStr` by default.</param>
		/// <returns>A representative string of the cell matrix's current state.</returns>
		public string ToText(string aliveStr = "|X|", string deadStr = null)
		{
			deadStr ??= new String('-', aliveStr.Length);
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
