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
            
            var reviews = new[]
            {
                new Review {Text = "it's fun"},
                new Review {Text = "That was a really bad movie"},
                new Review {Text = "This movie was supposed to change my life, but meh..."},
                new Review {Text = "An entertaining adventure"},
                new Review {Text = "Probably ok-ish, but I'm not 100% sure.."}
            };

            foreach (var review in reviews)
            {
                var output = Predict(context, model, review);
                var prediction = output.Prediction ? "positive" : "negative"; 
                
                Console.WriteLine($"Review: '{review.Text}'\nPrediction: {prediction}\n");
            }
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
            context.BinaryClassification.Evaluate(predictions);
        }

        private static ReviewPrediction Predict(MLContext context, ITransformer model, Review review)
        {
            var predictionEngine = context.Model.CreatePredictionEngine<Review, ReviewPrediction>(model);
            
            return predictionEngine.Predict(review);
        }
    }
}