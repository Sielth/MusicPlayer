using Microsoft.ML.Data;

namespace MoodService.App.Models
{
  public class OnnxInput
  {
    [VectorType(7)]
    [ColumnName("feature_input")]
    public float[] Features { get; set; }
  }
}
