namespace LinearAlgebra
{
	class AugmentedMatrix : Matrix, ICloneable
	{
		private Vector _vector;

		public double this[int row]
		{
			get => _vector[row];
			set => _vector[row] = value;
		}
		public AugmentedMatrix(double[,] matrix, Vector vector) : base(matrix)
		{
			_vector = vector;
		}
		public AugmentedMatrix(Matrix matrix, Vector vector) : base((double[,])matrix)
		{
			_vector = vector;
		}

		public object Clone() =>
			new AugmentedMatrix(_matrix, _vector);
	}
}
