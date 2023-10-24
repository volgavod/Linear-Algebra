namespace LinearAlgebra
{
	struct Vector
	{
		public readonly int Dimensions;
		private double[] _vector;

		public double Norm
		{
			get
			{
				double result = 0;
				for (int i = 0; i < Dimensions; i++)
					result += Math.Pow(_vector[i], 2);

				return Math.Sqrt(result);
			}
		}
		public double this[int index]
		{
			get => _vector[index];
			set => _vector[index] = value;
		}
		public Vector(int dimensions)
		{
			Dimensions = dimensions;
			_vector = new double[dimensions];
		}
		public Vector(double[] vector)
		{
			_vector = vector;
			Dimensions = vector.Length;
		}

		public Matrix ToMatrix(Orientation orientation)
		{
			Matrix matrix;
			switch (orientation)
			{
				case Orientation.Vertical:
					matrix = new Matrix(Dimensions, 1);
					for (int i = 0; i < Dimensions; i++)
						matrix[i, 0] = _vector[i];
					break;
				case Orientation.Horizontal:
					matrix = new Matrix(1, Dimensions);
					for (int i = 0; i < Dimensions; i++)
						matrix[0, i] = _vector[i];
					break;
				default:
					throw new Exception();
			}
			return matrix;
		}
		/// <summary> Scalar product </summary>
		public static double Dot(Vector v1, Vector v2)
		{
			if (v1 != v2)
				throw new Exception("Вектора не одинаковы");

			double scalar = 0;
			for (int index = 0; index < v1.Dimensions; index++)
				scalar += v1[index] * v2[index];

			return scalar;
		}

		public static Vector operator +(Vector v1, Vector v2)
		{
			if (v1 != v2)
				throw new Exception("Сложение векторов невозможно");

			for (int index = 0; index < v1.Dimensions; index++)
				v1[index] += v2[index];

			return v1;
		}
		public static Vector operator -(Vector v1, Vector v2)
		{
			if (v1 != v2)
				throw new Exception("Вычитание векторов невозможно");

			for (int index = 0; index < v1.Dimensions; index++)
				v1[index] -= v2[index];

			return v1;
		}
		public static Vector operator *(Vector vector, double value)
		{
			for (int index = 0; index < vector.Dimensions; index++)
				vector[index] *= value;

			return vector;
		}
		public static Vector operator *(double value, Vector vector) =>
			vector * value;
		public static Vector operator /(Vector vector, double value)
		{
			for (int index = 0; index < vector.Dimensions; index++)
				vector[index] /= value;

			return vector;
		}
		public static bool operator ==(Vector v1, Vector v2) =>
			v1.Dimensions == v2.Dimensions;
		public static bool operator !=(Vector v1, Vector v2) =>
			v1.Dimensions != v2.Dimensions;
		public static implicit operator Vector(double[] vector) =>
			new Vector(vector);
		public static explicit operator double[](Vector vector) =>
			vector._vector;

		internal enum Orientation
		{
			Vertical,
			Horizontal
		}
	}
}
