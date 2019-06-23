using System;
using System.IO;
using ExploringMLNet.Models;
using Microsoft.ML;

namespace ExploringMLNet
{
    internal static class Program
    {
        private static readonly string TrainData = Path.Combine(Environment.CurrentDirectory, "Data", "sums-test.txt");
        private static readonly string TestData = Path.Combine(Environment.CurrentDirectory, "Data", "sums-test.txt");
        
        private static void Main()
        {
            var context = new MLContext();
            var model = Train(context);
            Evaluate(context, model);

            var sample = new Input
            {
                First = 123,
                Second = 300
            };
            var output = Predict(context, model, sample);
            
            Console.WriteLine($"Prediction for {sample.First} + {sample.Second}: {output.Result}");
        }

        private static ITransformer Train(MLContext context)
        {
            var dataView = context.Data.LoadFromTextFile<Input>(TrainData, hasHeader: true, separatorChar: ',');
            var pipeline = context.Transforms.CopyColumns("Label", "Result")
                .Append(context.Transforms.Concatenate("Features", "First", "Second"))
                .Append(context.Regression.Trainers.FastTree());

            return pipeline.Fit(dataView);
        }

        private static void Evaluate(MLContext context, ITransformer model)
        {
            var dataView = context.Data.LoadFromTextFile<Input>(TestData, hasHeader: true, separatorChar: ',');
            var predictions = model.Transform(dataView);
            var metrics = context.Regression.Evaluate(predictions);
            
            Console.WriteLine("Evaluation results:");
            Console.WriteLine($"RSquared score: {metrics.RSquared:0.##}");
            Console.WriteLine($"RMS error: {metrics.RootMeanSquaredError:#.##}");
        }

        private static Output Predict(MLContext context, ITransformer model, Input sample)
        {
            var predictionEngine = context.Model.CreatePredictionEngine<Input, Output>(model);
            
            return predictionEngine.Predict(sample);
        }
    }
}