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
				double[] coef = new double[index];
				for (int j = 0; j < coef.Length; j++)
					coef[j] = Vector.Dot(a, b[j]) / Vector.Dot(b[j], b[j]);

				b[index] = a;
				for (int j = 0; j < index; j++)
					b[index] -= coef[j] * b[j];
			}

			return b.ToMatrix(Vector.Orientation.Vertical);
		}
		public static Matrix SolveMatrixEquationAXB(Matrix A, Matrix B) =>
			A.Invert() * B;
		public static Matrix SolveMatrixEquationXAB(Matrix A, Matrix B) =>
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
		/// <summary> r = b - Ax </summary>
		public static Vector CalcResidualsVector(Matrix A, Vector b, Vector x)
		{
			Vector residuals = new Vector(A.Rows);

			for (int row = 0; row < A.Rows; row++)
			{
				double sum = 0;

				for (int column = 0; column < A.Rows; column++)
					sum += A[row, column] * x[column];

				residuals[row] = b[row] - sum;
			}

			return residuals;
		}
		/// <summary> Calculate approximations </summary>
		public static double[,] IterativeMethod(AugmentedMatrix am, int iterations = 15)
		{
			if (iterations < 1)
				throw new Exception("Количество итераций должно быть больше нуля");

			AugmentedMatrix augMatrix = (AugmentedMatrix)am.Clone();
			double[] mainElements = augMatrix.GetMainDiagonal();
			double[,] approximations = new double[iterations + 1, mainElements.Length * 2];

			// Приведение матрицы
			for (int row = 0; row < am.Rows; row++)
			{
				for (int column = 0; column < am.Columns; column++)
					augMatrix[row, column] /= mainElements[row];

				augMatrix[row] /= mainElements[row];
			}

			// Нулевое приближение
			for (int index = 0; index < mainElements.Length; index++)
				approximations[0, index] = augMatrix[index];

			// Остальные приближения
			for (int index = 1; index < iterations + 1; index++)
			{
				for (int row = 0; row < augMatrix.Rows; row++)
				{
					approximations[index, row] += augMatrix[row];
					for (int column = 0; column < augMatrix.Columns; column++)
					{
						if (row == column)
							continue;

						approximations[index, row] -= augMatrix[row, column] * approximations[index - 1, column];
					}

					double xNow = approximations[index, row];
					double xBefore = approximations[index - 1, row];
					approximations[index, row + mainElements.Length] = Math.Abs(xNow) - Math.Abs(xBefore);
				}
			}

			return approximations;
		}
	}
}