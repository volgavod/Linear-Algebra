// Этот файл является служебным - для проверки работы программы
// Он никак не затрагивает бизнес-логику проекта

using LinearAlgebra;

class Program
{
	static void Main()
	{
		Zadanie._121g_1();
	}
}

static class Zadanie
{
	public static Vector F =>
		new double[] { 0.3, 0.5, 0.7, 0.9 };
	public static Matrix A1 => new double[,]
	{
		{  1.00, 0.17, -0.25,  0.54 },
		{  0.47, 1.00,  0.67, -0.32 },
		{ -0.11, 0.35,  1.00, -0.74 },
		{  0.55, 0.43,  0.36,  1.00 }
	};
	public static Matrix A2 => new double[,]
	{
		{  4.33, -1.12, -1.08,  1.14 },
		{ -1.12,  4.33,  0.24, -1.22 },
		{ -1.08,  0.24,  7.21, -3.22 },
		{  1.14, -1.22, -3.22,  5.43 }
	};
	public static Matrix A3 => new double[,]
	{
		{ 1.00, 0.42, 0.54, 0.66 },
		{ 0.42, 1.00, 0.32, 0.44 },
		{ 0.54, 0.32, 1.00, 0.22 },
		{ 0.66, 0.44, 0.22, 1.00 }
	};

	public static void _121g_1()
	{
		AugmentedMatrix M = new AugmentedMatrix(A1, F);
		AugmentedMatrix H = new AugmentedMatrix(A2, F);
		AugmentedMatrix A = new AugmentedMatrix(A3, F);

		Console.WriteLine("Матрица M");
		double[,] apprM = LAMath.IterativeMethod(M, 14);
		PrintApproximations(apprM);

		Console.WriteLine("\nМатрица H");
		double[,] apprH = LAMath.IterativeMethod(H, 13);
		PrintApproximations(apprH);

		Console.WriteLine("\nМатрица A");
		double[,] apprA = LAMath.IterativeMethod(A);
		PrintApproximations(apprA);
	}
	public static void _119b()
	{
		Matrix E = Matrix.GetIdentity(4);
		Matrix A = A3;

		Matrix Q = LAMath.GramSchmidtOrthogonalization(A);
		Console.WriteLine("Ортогонализированная матрица:");
		LAPrinter.Write(Q);

		Matrix X = LAMath.SolveMatrixEquationAXB(Q, E);
		Console.WriteLine("\nРезультат:");
		LAPrinter.Write(X);

		Matrix R = LAMath.CalcResidualsMatrix(E, Q, X);
		Console.WriteLine("\nМатрица невязки: ");
		LAPrinter.Write(R, null);
	}
	public static void _115a()
	{
		Matrix A = A3;
		Vector f = F;
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
	public static void Test()
	{
		AugmentedMatrix A = new AugmentedMatrix(A3, F);
		double[,] approximations = LAMath.IterativeMethod(A);
		PrintApproximations(approximations);
	}
	public static void PrintApproximations(double[,] approximations)
	{
		int length0 = approximations.GetLength(0);
		int length1 = approximations.GetLength(1);
		for (int i = 0; i < length0; i++)
		{
			Console.Write(i + ") ");
			for (int j = 0; j < length1; j++)
			{
				if (i == 0 && j >= length1 / 2)
					Console.Write("—\t\t");
				else
					Console.Write(string.Format("{0:F6}\t", Math.Round(approximations[i, j], 6)));
			}
			Console.WriteLine();
			if (i == length0 - 1)
			{
				Console.WriteLine();
				for (int j = 0; j < length1 / 2; j++)
					Console.WriteLine($"X{j + 1}: {string.Format("{0:F6}", approximations[length0 - 1, j])}  " +
								  $"Погрешность: {string.Format("{0:F6}", approximations[length0 - 1, j + length1 / 2])}" );
			}
		}

	}
}
