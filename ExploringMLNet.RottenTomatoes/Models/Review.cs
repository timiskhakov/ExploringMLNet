using Microsoft.ML.Data;

namespace ExploringMLNet.RottenTomatoes.Models
{
    public class Review
    {
        [LoadColumn(0)]
        public bool Freshness { get; set; }
        [LoadColumn(1)]
        public string Text { get; set; }
    }
}