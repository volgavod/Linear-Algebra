namespace LinearAlgebra
{
	class Program
	{
		static void Main()
		{
			Matrix matrix = new double[,] { { 1 } };

			double[,] d = (double[,])matrix;

			//Zadanie_119b();
		}

		static void Zadanie_119b()
		{
			Matrix E = Matrix.GetIdentity(4);
			Matrix A = new double[,]
			{
				{ 1.00, 0.42, 0.54, 0.66 },
				{ 0.42, 1.00, 0.32, 0.44 },
				{ 0.54, 0.32, 1.00, 0.22 },
				{ 0.66, 0.44, 0.22, 1.00 }
			};

			Matrix Q = LAMath.GramSchmidtOrthogonalization(A);
			Console.WriteLine("Ортогонализированная матрица:");
			LAPrinter.Write(Q);

			Matrix X = LAMath.MatrixEquationAXB(Q, E);
			Console.WriteLine("\nРезультат:");
			LAPrinter.Write(X);

			Matrix R = LAMath.CalcResidualsMatrix(E, Q, X);
            Console.WriteLine("\nМатрица невязки: ");
			LAPrinter.Write(R, null);
        }

		static void Zadanie_115a()
		{
			Matrix A = new double[,]
			{
				{ 1.00, 0.42, 0.54, 0.66 },
				{ 0.42, 1.00, 0.32, 0.44 },
				{ 0.54, 0.32, 1.00, 0.22 },
				{ 0.66, 0.44, 0.22, 1.00 }
			};
			Vector f = new Vector(new double[] { 0.3, 0.5, 0.7, 0.9 });
			Console.WriteLine("Исходные данные:");
			LAPrinter.Write(A);
			LAPrinter.Write(f, Vector.Orientation.Vertical);

			(Matrix, Vector) ex = LAMath.GaussJordanElimination(A, f);
			Matrix E = ex.Item1;
			Vector x = ex.Item2;
			Console.WriteLine("\nРезультат:");
			LAPrinter.Write(E);
			LAPrinter.Write(x, Vector.Orientation.Vertical);

			Vector res = LAMath.CalcResidualsVector(A, f, x);
			Console.WriteLine("\nВектор невязки:");
			LAPrinter.Write(res);
			Console.WriteLine("\nЕго норма:\n" + res.Norm);
		}
	}
}
