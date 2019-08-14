using Microsoft.ML.Data;

namespace ExploringMLNet.Models
{
    public class Input
    {
        [LoadColumn(0)]
        public float A;
        [LoadColumn(1)]
        public float B;
        [LoadColumn(2)]
        public float C;
        [LoadColumn(3)]
        public float D;
        [LoadColumn(4)]
        public float E;
        [LoadColumn(5)]
        public float Result;
    }
}