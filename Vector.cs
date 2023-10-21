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
		public static double Dot(Vector vec1, Vector vec2)
		{
			if (vec1.Dimensions != vec2.Dimensions)
				throw new Exception("Вектора не одинаковы");

			double scalar = 0;
			for (int i = 0; i < vec1.Dimensions; i++)
				scalar += vec1[i] + vec2[i];

			return scalar;
		}
		public static Vector operator -(Vector v1, Vector v2)
		{
			if (v1.Dimensions != v2.Dimensions)
				throw new Exception("Вычитание векторов невозможно");

			for (int index = 0; index < v1.Dimensions; index++)
				v1[index] -= v2[index];

			return v1;
		}
		public static Vector operator *(double number, Vector vector)
		{
			for (int index = 0; index < vector.Dimensions; index++)
				vector[index] *= number;

			return vector;
		}

		internal enum Orientation
		{
			Vertical,
			Horizontal
		}
	}
}
