using System;
using System.IO;
using ExploringMLNet.RottenTomatoes.Models;
using Microsoft.ML;

namespace ExploringMLNet.RottenTomatoes
{
    internal static class Program
    {
        private static readonly string ReviewsFile =
            Path.Combine(Environment.CurrentDirectory, "Data", "reviews.txt");
        
        private static void Main()
        {
            var context = new MLContext();
            var dataView = context.Data.LoadFromTextFile<Review>(ReviewsFile, hasHeader: true, separatorChar: ',');
            var split = context.Data.TrainTestSplit(dataView, 0.2);
            var trainSet = split.TrainSet;
            var testSet = split.TestSet;

            var model = Train(context, trainSet);
            Evaluate(context, model, testSet);
            
            var sample = new Review    
            {
                Text = "it's fun"
            };
            var output = Predict(context, model, sample);
            
            Console.WriteLine(output.Prediction);
        }

        private static ITransformer Train(MLContext context, IDataView trainSet)
        {
            var pipeline = context.Transforms.Text.FeaturizeText("Features", "Text")
                .Append(context.Transforms.CopyColumns("Label", "Freshness"))
                .Append(context.BinaryClassification.Trainers.SdcaLogisticRegression());
            
            return pipeline.Fit(trainSet);
        }
        
        private static void Evaluate(MLContext context, ITransformer model, IDataView testSet)
        {
            var predictions = model.Transform(testSet);
            var metrics = context.BinaryClassification.Evaluate(predictions);
            
            Console.WriteLine("Evaluation results:");
            Console.WriteLine($"Score: {metrics.F1Score:0.##}");
        }

        private static ReviewPrediction Predict(MLContext context, ITransformer model, Review sample)
        {
            var predictionEngine = context.Model.CreatePredictionEngine<Review, ReviewPrediction>(model);
            
            return predictionEngine.Predict(sample);
        }
    }
}