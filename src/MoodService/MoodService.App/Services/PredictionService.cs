using Microsoft.ML;
using MoodService.App.Models;
using System.Linq;

namespace MoodService.App.Services
{
  public class PredictionService : IPredictionService
  {
    static string ONNX_MODEL_PATH = "model_mood.onnx";

    public MoodType Run(TrackFeatures features)
    {
      MLContext mlContext = new MLContext();

      var onnxPredictionPipeline = GetPredictionPipeline(mlContext);

      var onnxPredictionEngine = mlContext.Model.CreatePredictionEngine<OnnxInput, OnnxOutput>(onnxPredictionPipeline);

      var testInput = new OnnxInput
      {
        Features = new float[]
        {
          features.Danceability,
          features.Acousticness,
          features.Energy,
          features.Instrumentalness,
          features.Valence,
          features.Loudness,
          features.Speechiness
        }
      };

      var prediction = onnxPredictionEngine.Predict(testInput);

      return (MoodType)prediction.Mood.First();
    }

    public ITransformer GetPredictionPipeline(MLContext mlContext)
    {
      var inputColumns = new string[] { "feature_input" };

      var outputColumns = new string[] { "output_label" };

      var onnxPredictionPipeline = mlContext
        .Transforms
        .ApplyOnnxModel(outputColumnNames: outputColumns, inputColumnNames: inputColumns, ONNX_MODEL_PATH);

      var emptyDv = mlContext.Data.LoadFromEnumerable(new OnnxInput[] { });

      return onnxPredictionPipeline.Fit(emptyDv);
    }
  }
}
