using Microsoft.ML.Data;

namespace ExploringMLNet.Models
{
    public class Output
    {
        [ColumnName("Score")]
        public float Result;
    }
}