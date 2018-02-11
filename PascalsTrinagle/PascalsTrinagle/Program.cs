using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PascalsTrinagle
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintPascalsTrinagle.Print(new PascalGenerator(new PascalUsingMemoization()));
            PrintPascalsTrinagle.Print(new PascalGenerator(new PascalUsingFactorialMemoization()));
            KristoffersDiamond.Print(new PascalGenerator(new PascalUsingMemoization()));
        }
    }

    public interface IPascalGenerator
    {
        int Generate(int row, int col);
    }

    public class PascalGenerator
    {
        private IPascalGenerator strategy;
        public PascalGenerator(IPascalGenerator iPublicGenerator)
        {
            strategy = iPublicGenerator;
        }

        public int Generate(int row, int col)
        {
            return strategy.Generate(row, col);
        }
    }

    public class PascalUsingMemoization : IPascalGenerator
    {
        private static Dictionary<string, int> cache = new Dictionary<string, int>();

        public int Generate(int row, int col)
        {
            if (row == 0 || col == 0 || row == col)
            {
                return 1;
            }
            var k = $"{row},{col}";
            if (!cache.ContainsKey(k))
            {
                cache[k] = Generate(row - 1, col - 1) + Generate(row - 1, col);
            }
            return cache[k];
        }
    }

    public class PascalUsingFactorialMemoization : IPascalGenerator
    {
        private static FactorialCalculator factorialCalculator = new FactorialCalculator();
        public int Generate(int row, int col)
        {
            var nFac = factorialCalculator.Generate(row);
            var kFac = factorialCalculator.Generate(col);
            var nkFac = factorialCalculator.Generate(row - col);
            return nFac / (kFac * nkFac);
        }

        private class FactorialCalculator
        {
            private static int[] memoization = { 1, 1 };
            public int Generate(int n)
            {
                if (memoization.Length <= n)
                {
                    var oldMemoization = memoization;
                    memoization = new int[n + 1];
                    for (int i = 0; i < oldMemoization.Length; i++)
                    {
                        memoization[i] = oldMemoization[i];
                    }
                    memoization[n] = n * Generate(n - 1);
                }
                return memoization[n];
            }
        }
    }

    internal static class PrintPascalsTrinagle
    {
        public static void Print(PascalGenerator pg)
        {
            var lineLength = 15;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    sb.Append(pg.Generate(i, j) + " ");
                }
                var content = sb.ToString().Trim();
                for (var j = content.Length / 2; j < lineLength; j++)
                {
                    content = " " + content;
                }
                Console.WriteLine(content);
                sb.Clear();
            }
            Console.WriteLine();
        }
    }

    internal class KristoffersDiamond
    {
        public static void Print(PascalGenerator pg)
        {
            var rows = 13;
            var lineLength = 25;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    sb.Append(pg.Generate(i, j) + " ");
                }
                var content = sb.ToString().Trim();
                for (var j = content.Length / 2; j < lineLength; j++)
                {
                    content = " " + content;
                }
                Console.WriteLine(content);
                sb.Clear();
            }
            for (int i = rows - 2; i >= 0; i--)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    sb.Append(pg.Generate(i, j) + " ");
                }
                var content = sb.ToString().Trim();
                for (var j = content.Length / 2; j < lineLength; j++)
                {
                    content = " " + content;
                }
                Console.WriteLine(content);
                sb.Clear();
            }
            Console.WriteLine();
        }
    }
}