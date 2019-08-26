using System;

namespace DeepCloneSelfTest
{
    class Program
    {

        static void Main(string[] args)
        {

            int[,] a = new int[1, 2];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    a[i, j] = j;
                    Console.WriteLine($"i{i}j{j}:{j}");
                }
            }
            int[,] b= new int[1,2];
            Array.Copy(a, b, a.Length);
            for (int i = 0; i < b.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    a[i, j] = j;
                    Console.WriteLine($"i{i}j{j}:{j}");
                }
            }

            int[][] c = new int[1][];
            for (int i = 0; i < c.Length; i++)
            {
                c[i] = new int[2];
                for (int j = 0; j < 2; j++)
                {
                    c[i][j] = j+10;
                }
            }
            int[][] d = new int[1][];
            Array.Copy(c, d, c.Length);
            for (int i = 0; i < d.Length; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Console.WriteLine(d[i][j]);
                }
            }
            Console.ReadKey();

        }

    }
}
