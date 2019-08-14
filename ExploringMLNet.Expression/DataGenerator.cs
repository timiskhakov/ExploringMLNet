using System;
using System.IO;
using System.Text;
using ExploringMLNet.Models;

namespace ExploringMLNet
{
    public static class DataGenerator
    {
        private const int NumberLimit = 100;
        
        private static readonly Random Random = new Random();

        public static void GenerateData()
        {
            File.WriteAllText("./train.txt", Generate(80_000), Encoding.UTF8);
            File.WriteAllText("./test.txt", Generate(20_000), Encoding.UTF8);
        }

        public static int CalculateExpression(Input input)
        {
            return CalculateExpression((int)input.A, (int)input.B, (int)input.C, (int)input.D, (int)input.E);
        }

        private static int CalculateExpression(int a, int b, int c, int d, int e)
        {
            return (int) Math.Pow(a + b, 2) + (c + d) * e;
        }

        private static string Generate(int number)
        {
            var sb = new StringBuilder();
            sb.AppendLine("a,b,c,d,e,result");
            
            for (var i = 0; i < number; i++)
            {
                var a = Random.Next(NumberLimit);
                var b = Random.Next(NumberLimit);
                var c = Random.Next(NumberLimit);
                var d = Random.Next(NumberLimit);
                var e = Random.Next(NumberLimit);

                var result = CalculateExpression(a, b, c, d, e);

                sb.AppendLine($"{a},{b},{c},{d},{e},{result}");
            }

            return sb.ToString();
        }
    }
}