using System;
using System.Text;

namespace PII_Game_Of_Life
{
	/// <summary>
	/// Esta clase almacena los métodos que no están asociados a ninguna clase.
	/// </summary>
	public static class Utils
	{
		/// <summary>
		/// Returns whether a cell is alive in the next generation.
		/// </summary>
		/// <param name="wasAlive">Whether the cell was alive in the previous generation.</param>
		/// <param name="aliveNeighbors">The number of alive neighbors of the cell in the previous generation.</param>
		/// <returns>Whether the cell is alive in the next generation.</returns>
		public static bool IsAliveInNextGeneration(bool wasAlive, byte aliveNeighbors)
			=> aliveNeighbors == 3 || (wasAlive && aliveNeighbors == 2);
		
		/// <summary>
		/// Generates a matrix of booleans from a string.
		/// This function could appear as a method of `GameOfLife`, but it'd break the SRP principle,
		/// because this responsibility is not directly associated with `GameOfLife`,
		/// but instead, it just takes the data string and transforms it into a matrix of booleans.
		/// Therefore, it's represented as a separate function.
		/// </summary>
		/// <param name="data">The string from which the matrix is created</param>
		/// <returns>The resulting matrix of booleans.</returns>
		public static bool[,] MatrixFromString(string data)
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

			bool[,] matrix = new bool[width, height];

			// Set state of cells according to data
			for(int i = 0; i < width; i++)
			{
				for(int j = 0; j < height; j++)
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

			return matrix;
		}

		/// <summary>
		/// Returns a representative string of a matrix of booleans.
		/// This function could appear as a method of `GameOfLife`, but it'd break the SRP principle,
		/// because this responsibility is not directly associated with `GameOfLife`,
		/// but instead, it just takes the matrix of booleans and transforms it into a string.
		/// Therefore, it's represented as a separate function.
		/// </summary>
		/// <param name="aliveStr">The string which represents an alive cell. "|X|" by default.</param>
		/// <param name="deadStr">The string which represents a dead cell. A concatenation of '-' characters equally as long as `aliveStr` by default.</param>
		/// <returns>A representative string of the matrix of booleans.</returns>
		public static string MatrixToString(bool[,] matrix, string aliveStr = "|X|", string deadStr = null)
		{
			if(string.IsNullOrEmpty(aliveStr))
				aliveStr ??= "|X|";
			deadStr ??= new String('-', aliveStr.Length);
			StringBuilder builder = new StringBuilder("");
			for(int j = 0; j < matrix.GetLength(1); j++)
			{
				for(int i = 0; i < matrix.GetLength(0); i++)
				{
					builder.Append(matrix[i, j] ? aliveStr : deadStr);
				}
				builder.Append("\n");
			}
			return builder.ToString();
		}
	}
}