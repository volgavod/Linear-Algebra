namespace LinearAlgebra
{
	class SquareMatrix : Matrix, ICloneable
	{
		public readonly int Size;

		public SquareMatrix(int size) : base(size, size)
		{
			Size = size;
		}
		public SquareMatrix(double[,] matrix) : base(matrix)
		{
			if (Rows != Columns)
				throw new Exception("Не квадратная матрица");
			Size = Rows;
		}

		public object Clone() =>
			new SquareMatrix(_matrix);
	}
}
