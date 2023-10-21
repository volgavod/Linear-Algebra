namespace LinearAlgebra
{
	static class LAMath
	{
		public static Matrix GramSchmidtOrthogonalization(Matrix matrix)
		{
			Vector[] b = new Vector[matrix.Columns];

			for (int index = 0; index < b.Length; index++)
			{
				Vector a = matrix.GetColumn(index + 1);
				double[] coefficient = new double[index];
				for (int j = 0; j < coefficient.Length; j++)
					coefficient[j] = Vector.Dot(a, b[j]) / Vector.Dot(b[j], b[j]);

				b[index] = a;
				for (int j = 0; j < index; j++)
					b[index] -= coefficient[j] * b[j];
			}

			return b.ToMatrix(Vector.Orientation.Vertical);
		}
		public static Matrix MatrixEquationAXB(Matrix A, Matrix B) =>
			A.Invert() * B;
		public static Matrix MatrixEquationXAB(Matrix A, Matrix B) =>
			B * A.Invert();
		public static (Matrix, Vector) GaussJordanElimination(Matrix m, Vector f)
		{
			if (m.IsSquare() == false)
				throw new Exception("Матрица не квадратная");

			Matrix matrix = m.AddColumnToEnd(f);
			for (int index = 0; index < matrix.Rows; index++)
			{
				// Приводим элемент на главной диагонали к 1 и делим строку
				for (int column = 0; column < matrix.Columns; column++)
					matrix[index, column] /= matrix[index, index];

				for (int row = 0; row < matrix.Rows; row++)
				{
					if (index == row)
						continue;

					// Приводим строки к нулю
					double c = matrix[row, index];
					for (int column = index; column < matrix.Columns; column++)
						matrix[row, column] -= c * matrix[index, column];
				}
			}

			Matrix identity = matrix.RemoveColumn(matrix.Columns);
			Vector x = matrix.GetColumn(matrix.Columns);
			return (identity, x);
		}
		/// <summary> R = B - AX </summary>
		public static Matrix CalcResidualsMatrix(Matrix B, Matrix A, Matrix X) =>
			B - A * X;
		public static Vector CalcResidualsVector(Matrix m, Vector b, Vector x)
		{
			Vector residuals = new Vector(m.Rows);

			for (int row = 0; row < m.Rows; row++)
			{
				double sum = 0;

				for (int column = 0; column < m.Rows; column++)
					sum += m[row, column] * x[column];

				residuals[row] = b[row] - sum;
			}

			return residuals;
		}
		public static void IterativeMethod(Matrix m, Vector f)
		{
			if (IsThereSolutionForIterativeMethod(m) == false)
				throw new Exception("Эта матрица не подходит");


		}
		private static bool IsThereSolutionForIterativeMethod(Matrix m)
		{
			if (m.IsSquare() == false)
				return false;

			for (int column = 0; column < m.Columns; column++)
			{
				double main = 0;
				double other = 0;
				for (int row = 0; row < m.Rows; row++)
				{
					double value = Math.Abs(m[row, column]);
					if (column == row)
						main = value;
					else
						other += value;
				}
				if (main <= other)
					return false;
			}

			return true;
		}
	}
}