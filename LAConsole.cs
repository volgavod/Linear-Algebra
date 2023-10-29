namespace LinearAlgebra
{
	static class LAConsole
	{
		public static void Write(Matrix matrix, int? round = 6)
		{
			for (int row = 0; row < matrix.Rows; row++)
			{
				for (int column = 0; column < matrix.Columns; column++)
				{
					if (round == null)
						Console.Write(matrix[row, column] + "\t\t");
					else
						Console.Write(Math.Round(matrix[row, column], (int)round) + "\t");
				}
				Console.WriteLine();
			}
		}

		public static void Write(Vector vector, Vector.Orientation orientation = Vector.Orientation.Horizontal, int round = 6)
		{
			switch (orientation)
			{
				case Vector.Orientation.Horizontal:
					for (int i = 0; i < vector.Dimensions; i++)
						Console.Write(Math.Round(vector[i], round) + "\t");
					break;

				case Vector.Orientation.Vertical:
					for (int i = 0; i < vector.Dimensions; i++)
						Console.WriteLine(Math.Round(vector[i], round));
					break;

				default:
					throw new Exception();
			}
		}
	}
}