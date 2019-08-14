using System;
using System.IO;
using ExploringMLNet.Models;
using Microsoft.ML;

namespace ExploringMLNet
{
    internal static class Program
    {
        private static readonly string TrainData = Path.Combine(Environment.CurrentDirectory, "Data", "train.txt");
        private static readonly string TestData = Path.Combine(Environment.CurrentDirectory, "Data", "test.txt");
        
        private static void Main()
        {
            //DataGenerator.GenerateData();
            
            var context = new MLContext();
            var model = Train(context);
            Evaluate(context, model);

            var inputs = new[]
            {
                new Input {A = 78, B = 10, C = 56, D = 45, E = 20},
                new Input {A = 22, B = 59, C = 7, D = 99, E = 99},
                new Input {A = 11, B = 38, C = 15, D = 76, E = 66},
                new Input {A = 26, B = 26, C = 8, D = 67, E = 34},
                new Input {A = 47, B = 26, C = 77, D = 14, E = 94},
            };

            foreach (var input in inputs)
            {
                var output = Predict(context, model, input);

                var expression = $"({input.A} + {input.B})ˆ2 + ({input.C} + {input.D}) * {input.E}";
                var prediction = output.Result;
                var actual = DataGenerator.CalculateExpression(input);
                
                Console.WriteLine($"{expression}\nprediction: {prediction}\nactual: {actual}\n");
            }
        }

        private static ITransformer Train(MLContext context)
        {
            var dataView = context.Data.LoadFromTextFile<Input>(TrainData, hasHeader: true, separatorChar: ',');
            var pipeline = context.Transforms.CopyColumns("Label", "Result")
                .Append(context.Transforms.Concatenate("Features", "A", "B", "C", "D", "E"))
                .Append(context.Regression.Trainers.FastTree());

            return pipeline.Fit(dataView);
        }

        private static void Evaluate(MLContext context, ITransformer model)
        {
            var dataView = context.Data.LoadFromTextFile<Input>(TestData, hasHeader: true, separatorChar: ',');
            var predictions = model.Transform(dataView);
            context.Regression.Evaluate(predictions);
        }

        private static Output Predict(MLContext context, ITransformer model, Input sample)
        {
            var predictionEngine = context.Model.CreatePredictionEngine<Input, Output>(model);
            
            return predictionEngine.Predict(sample);
        }
    }
}