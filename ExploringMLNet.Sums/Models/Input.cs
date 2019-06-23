using Microsoft.ML.Data;

namespace ExploringMLNet.Models
{
    public class Input
    {
        [LoadColumn(0)]
        public float First;
        [LoadColumn(1)]
        public float Second;
        [LoadColumn(2)]
        public float Result;
    }
}