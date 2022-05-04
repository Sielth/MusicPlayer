using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Onnx;
using System;

namespace MoodService.App.Models
{
  public class OnnxOutput
  {
    [ColumnName("output_label"), OnnxMapType(typeof(Int64), typeof(Single))]
    public Int64[] Mood { get; set; }
  }
}
