# ExploringMLNet

A small project to explore [ML.NET](https://dotnet.microsoft.com/apps/machinelearning-ai/ml-dotnet) library. This repo is a part of my Futurice's DotNeat talk on ML.NET.

## Setup

```bash
git clone [this repo]
cd ./ExploringMLNet.Sums
dotnet restore
```

## Usage

1. Setup a sample in the `main` method :
```csharp
var sample = new Input
{
    First = 123,
    Second = 300
};
```

2. Run the app:

```bash
dotnet run
```

3. The app is going to create a model and predict a sum of the sample's `First` and `Second`:
```
Prediction for 123 + 300: 422.5616
```