using System;
using System.IO;

class Program
{
    static void Main()
    {
        try
        {
            string[] lines = File.ReadAllLines("inf.txt");
            int N = int.Parse(lines[0]);

            double[,] G = new double[N, N];
            int lineIndex = 1;

            for (int i = 0; i < N; i++)
            {
                string[] row = lines[lineIndex].Split(' ');
                for (int j = 0; j < N; j++)
                {
                    G[i, j] = double.Parse(row[j]);
                }
                lineIndex++;
            }

            double[] x = new double[N];
            string[] vector = lines[lineIndex].Split(' ');
            for (int i = 0; i < N; i++)
            {
                x[i] = double.Parse(vector[i]);
            }

            if (!Symmetric(G, N))
            {
                Console.WriteLine("Ошибка: матрица G не симметрична!");
                return;
            }

            double length = VectorLength(G, x, N);

            Console.WriteLine($"Длина вектора: {length:F6}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static bool Symmetric(double[,] matrix, int size)
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = i + 1; j < size; j++)
            {
                if (Math.Abs(matrix[i, j] - matrix[j, i]) > 1e-10)
                {
                    return false;
                }
            }
        }
        return true;
    }

    static double VectorLength(double[,] G, double[] x, int N)
    {
        double[] t = new double[N];
        for (int i = 0; i < N; i++)
        {
            t[i] = 0;
            for (int j = 0; j < N; j++)
            {
                t[i] += G[i, j] * x[j];
            }
        }
        double result = 0;
        for (int i = 0; i < N; i++)
        {
            result += x[i] * t[i];
        }
        
        return Math.Sqrt(result);
    }
}