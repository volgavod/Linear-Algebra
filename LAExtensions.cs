namespace LinearAlgebra
{
	static class LAExtensions
	{
		public static Matrix ToMatrix(this Vector[] vectors, Vector.Orientation orientation)
		{
			int dimensions = vectors[0].Dimensions;
			for (int index = 1; index < vectors.Length; index++)
				if (vectors[index].Dimensions != dimensions)
					throw new Exception("Векторы разной длины");

			Matrix matrix;
			switch (orientation)
			{
				case Vector.Orientation.Horizontal:
					matrix = new Matrix(vectors.Length, vectors[0].Dimensions);
					for (int row = 0; row < matrix.Rows; row++)
						for (int column = 0; column < matrix.Columns; column++)
							matrix[row, column] = vectors[row][column];
					return matrix;

				case Vector.Orientation.Vertical:
					matrix = new Matrix(vectors[0].Dimensions, vectors.Length);
					for (int row = 0; row < matrix.Rows; row++)
						for (int column = 0; column < matrix.Columns; column++)
							matrix[row, column] = vectors[column][row];
					return matrix;

				default:
					throw new Exception();
			}
		}
	}
}