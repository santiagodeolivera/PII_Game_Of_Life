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
		public bool[,] Matrix { get; private set; }
		public int Width { get => Matrix.GetLength(0); }
		public int Height { get => Matrix.GetLength(1); }

		/// <summary>
		/// Build a GameOfLife object given a matrix of booleans as the initial state.
		/// </summary>
		/// <param name="data">The initial matrix of booleans.</param>
		public GameOfLife(bool[,] matrix)
		{
			this.Matrix = matrix;
		}

		/// <summary>
		/// Updates the cell matrix to the next generation.
		/// It follows the Expert pattern because it needs to be able to read and write on the matrix of booleans,
		/// which is an attribute of GameOfLife.
		/// </summary>
		public void Update()
		{
			bool[,] buffer = new bool[this.Width, this.Height];

			for(int i = 0; i < this.Width; i++)
				for(int j = 0; j < this.Height; j++)
				{
					byte aliveNeighbors = 0;
					for(int k = i - 1; k <= i + 1; k++)
						for(int l = j - 1; l <= j + 1; l++)
							if (
								k >= 0 && l >= 0
								&& k < this.Width
								&& l < this.Height
								&& this.Matrix[k, l]
							) aliveNeighbors++;

					if (this.Matrix[i, j]) aliveNeighbors--;

					buffer[i, j] = CellUtils.IsAliveInNextGeneration(this.Matrix[i, j], aliveNeighbors);
				}
				
			this.Matrix = buffer;
		}
	}
}
