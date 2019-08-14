# ExploringMLNet

A set of small projects to explore [ML.NET](https://dotnet.microsoft.com/apps/machinelearning-ai/ml-dotnet) library. This repo is a part of my Futurice's DotNeat talk on ML.NET.

## Problem 1: Regression

Say, we have a set of numbers that participates in some expression. We don't know the actual formula of that expression, but we do know its final result. Based on a provided sample set, we are going to predict its result.

To run the program and see sample based predictions run the following commands:

```bash
cd ./ExploringMLNet.Expression
dotnet restore
dotnet run
```

## Problem 2: Binary Classification

In this scenario we have a lot of user reviews from [Rotten Tomatoes](https://www.rottentomatoes.com). Each review has either a positive or negative rating. What we want is to determine a rating based on a provided text.

To run the program and see sample based predictions run the following commands:

```bash
cd ./ExploringMLNet.RottenTomatoes
dotnet restore
dotnet run
```
