using System;
using System.IO;
using System.Text;

namespace ExploringMLNet
{
    public static class DataGenerator
    {
        private const int NumberOfSums = 10000;
        private const int Limit = 1000;
        
        private static readonly Random Random = new Random();
        
        public static void GenerateSums()
        {
            var sb = new StringBuilder();
            sb.AppendLine("first,second,result");
            
            for (var i = 0; i < NumberOfSums; i++)
            {
                var first = Random.Next(Limit);
                var second = Random.Next(Limit);
                var result = first + second;

                sb.AppendLine($"{first},{second},{result}");
            }
            
            File.WriteAllText("./sums.txt", sb.ToString(), Encoding.UTF8);
        }
    }
}