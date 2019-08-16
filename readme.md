# ExploringMLNet

A set of small projects to explore [ML.NET](https://dotnet.microsoft.com/apps/machinelearning-ai/ml-dotnet) library. This repo is a part of my Futurice's DotNeat talk on ML.NET.

## Problem 1: Regression

Say, we have a huge list of sets of 5 numbers that participate in some expression and its result:

```
A,B,C,D,E,Result
25,76,60,75,34,14791
25,86,71,98,17,15194
21,29,20,15,49,4215
84,50,40,30,7,18446
...
```

The idea of this scenario is to train a model using this data, evaluate it, and guess a result based on a provided sample set. For example, feeding numbers like `19, 24, 64, 12, 10` should give us something close to `2609`.

Keep in mind that the machine doesn't know the actual formula of that expression. (Although if we do some math or check [`CalculateExpression`](https://github.com/timiskhakov/ExploringMLNet/blob/master/ExploringMLNet.Expression/DataGenerator.cs#L25) method we would know that the expression is `(a + b)ˆ2 + (c + d) * e`.)

To run the program and see sample based predictions run the following commands:

```bash
cd ./ExploringMLNet.Expression
dotnet restore
dotnet run
```

## Problem 2: Binary Classification

In this scenario we have a lot of user reviews from [Rotten Tomatoes](https://www.rottentomatoes.com). Each review has either a positive or negative rating. Then again, we build and evaluate a model, but this time round we need binary classification for that. What we want though is to determine a rating based on a provided text.

To run the program and see sample based predictions run the following commands:

```bash
cd ./ExploringMLNet.RottenTomatoes
dotnet restore
dotnet run
```
