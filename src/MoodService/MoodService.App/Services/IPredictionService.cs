using Microsoft.ML;
using MoodService.App.Models;

namespace MoodService.App.Services
{
  public interface IPredictionService
  {
    MoodType Run(TrackFeatures features);
    ITransformer GetPredictionPipeline(MLContext mlContext);
  }
}