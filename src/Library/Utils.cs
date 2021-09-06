using System;

namespace PII_Game_Of_Life
{
	/// <summary>
	/// Esta clase almacena los métodos que no están asociados a ninguna clase.
	/// </summary>
	public static class Utils
	{
		public static bool IsAliveInNextGeneration(bool wasAlive, byte aliveNeighbors)
			=> aliveNeighbors == 3 || (wasAlive && aliveNeighbors == 2);
	}
}