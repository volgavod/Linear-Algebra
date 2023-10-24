namespace LinearAlgebra
{
	class Matrix : ICloneable
	{ 
		public readonly int Rows;
		public readonly int Columns;
		protected double[,] _matrix;

		public double this[int row, int column]
		{
			get => _matrix[row, column];
			set => _matrix[row, column] = value;
		}
		public Matrix(int rows, int columns) : this(rows, columns, new double[rows, columns]) { }
		public Matrix(double[,] matrix) : this(matrix.GetLength(0), matrix.GetLength(1), matrix) { }
		protected Matrix(int rows, int columns, double[,] matrix)
		{
			Rows = rows > 0 ? rows : throw new Exception("Количество строк должно быть больше нуля");
			Columns = columns > 0 ? columns : throw new Exception("Количество столбцов должно быть больше нуля");
			_matrix = matrix;
		}

		public Matrix AddColumnToEnd(Vector vector)
		{
			Matrix matrix = new Matrix(Rows, Columns + 1);
			for (int row = 0; row < matrix.Rows; row++)
				for (int column = 0; column < matrix.Columns - 1; column++)
					matrix[row, column] = this[row, column];
			for (int row = 0; row < matrix.Rows; row++)
				matrix[row, Columns] = vector[row];
			return matrix;
		}
		public object Clone() =>
			new Matrix(_matrix);
		/// <summary> Матрица из алгебраических дополнений </summary>
		public Matrix GetAdjugatedMatrix()
		{
			Matrix m = new Matrix(Rows, Columns);
			for (int row = 0; row < Rows; row++)
				for (int column = 0; column < Columns; column++)
					m[row, column] = GetCofactor(row + 1, column + 1);
			return m;
		}
		/// <summary> Алгебраическое дополнение </summary>
		public double GetCofactor(int row, int column) =>
			Math.Pow(-1, row + column) * RemoveRowColumn(row, column).GetDeterminant();
		public Vector GetColumn(int column)
		{
			Vector vector = new Vector(Rows);
			for (int row = 0; row < Rows; row++)
				vector[row] = _matrix[row, column - 1];
			return vector;
		}
		public double GetDeterminant()
		{
			if (IsSquare() == false)
				throw new Exception("Det");

			if (Rows == 2)
				return GetMinor();
			else
			{
				double det = 0;
				for (int column = 0; column < Columns; column++)
					det += this[0, column] * Math.Pow(-1, column) *
						RemoveRowColumn(0, column + 1).GetDeterminant();
				return det;
			}
		}
		public Vector GetRow(int row)
		{
			Vector vector = new Vector(Columns);
			for (int column = 0; column < Columns; column++)
				vector[column] = _matrix[row - 1, column];
			return vector;
		}
		public double[] GetMainDiagonal()
		{
			if (IsSquare() == false)
				throw new Exception("Матрица не квадратная");

			int mainSize = Rows; // or Columns
			double[] mainElements = new double[mainSize];
			for (int index = 0; index < mainSize; index++)
				mainElements[index] = this[index, index];

			return mainElements;
		}
		public double GetMinor() =>
			IsSquare(2) ?
			this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0] :
			throw new Exception("Minor");
		/// <summary> Обратная матрица </summary>
		public Matrix Invert() =>
			Transpose().GetAdjugatedMatrix() / GetDeterminant();
		public bool IsSquare() =>
			Rows == Columns;
		public bool IsSquare(int size) =>
			Rows == size && Columns == size;
		public Matrix RemoveColumn(int column)
		{
			Vector[] vectors = new Vector[Columns - 1];

			for (int icolumn = 0; icolumn < vectors.Length; icolumn++)
				if (icolumn >= column - 1)
					vectors[icolumn] = GetColumn(icolumn + 2);
				else
					vectors[icolumn] = GetColumn(icolumn + 1);

			return vectors.ToMatrix(Vector.Orientation.Vertical);
		}
		public Matrix RemoveRow(int row)
		{
			Vector[] vectors = new Vector[Rows - 1];

			for (int irow = 0; irow < vectors.Length; irow++)
				if (irow >= row - 1)
					vectors[irow] = GetRow(irow + 2);
				else
					vectors[irow] = GetRow(irow + 1);

			return vectors.ToMatrix(Vector.Orientation.Horizontal);
		}
		public Matrix RemoveRowColumn(int row, int column) =>
			RemoveRow(row).RemoveColumn(column);
		public SquareMatrix ToSquareMatrix() =>
			IsSquare() ?
			new SquareMatrix(_matrix) :
			throw new Exception("Неквадратная матрица");
		public Matrix Transpose()
		{
			Matrix m = new Matrix(Columns, Rows);
			for (int row = 0; row < m.Rows; row++)
				for (int column = 0; column < m.Columns; column++)
					m[row, column] = this[column, row];
			return m;
		}
		public bool TryToSquareMatrix(out SquareMatrix matrix)
		{
			try
			{
				matrix = ToSquareMatrix();
				return true;
			}
			catch
			{
				matrix = null;
				return false;
			}
		}
		/// <summary> Единичная матрица </summary>
		public static SquareMatrix GetIdentity(int size)
		{
			SquareMatrix matrix = new SquareMatrix(size);
			for (int i = 0; i < size; i++)
				matrix[i, i] = 1;
			return matrix;
		}

		public static Matrix operator -(Matrix m1, Matrix m2)
		{
			if (m1 != m2)
				throw new Exception("Вычитание невозможно");

			Matrix matrix = (Matrix)m1.Clone();
			for (int row = 0; row < matrix.Rows; row++)
				for (int column = 0; column < matrix.Columns; column++)
					matrix[row, column] -= m2[row, column];

			return matrix;
		}
		public static Matrix operator *(Matrix m1, Matrix m2)
		{
			if (m1.Columns != m2.Rows)
				throw new Exception("Умножение невозможно");

			int mainSize = m1.Columns; // or m2.Rows
			Matrix matrix = new Matrix(m1.Rows, m2.Columns);

			for (int row = 0; row < matrix.Rows; row++)
				for (int column = 0; column < matrix.Columns; column++)
					for (int i = 0; i < mainSize; i++)
						matrix[row, column] += m1[row, i] * m2[i, column];

			return matrix;
		}
		public static Matrix operator *(Matrix m, double value)
		{
			Matrix matrix = (Matrix)m.Clone();

			for (int row = 0; row < matrix.Rows; row++)
				for (int column = 0; column < matrix.Columns; column++)
					matrix[row, column] *= value;

			return matrix;
		}
		public static Matrix operator *(double value, Matrix m) =>
			m * value;
		public static Matrix operator /(Matrix m, double value)
		{
			Matrix matrix = (Matrix)m.Clone();

			for (int row = 0; row < matrix.Rows; row++)
				for (int column = 0; column < matrix.Columns; column++)
					matrix[row, column] /= value;

			return matrix;
		}
		public static bool operator ==(Matrix m1, Matrix m2) =>
			m1.Rows == m2.Rows && m1.Columns == m2.Columns;
		public static bool operator !=(Matrix m1, Matrix m2) =>
			m1.Rows != m2.Rows || m1.Columns != m2.Columns;
		public static implicit operator Matrix(double[,] matrix) =>
			new Matrix(matrix);
		public static explicit operator double[,](Matrix matrix) =>
			matrix._matrix;
	}
}
