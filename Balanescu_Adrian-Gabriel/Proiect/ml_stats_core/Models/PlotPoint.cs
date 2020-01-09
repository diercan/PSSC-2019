using System;
using Newtonsoft.Json;

namespace ml_stats_core.Models
{
  public class PlotPoint : Entity
  {
    [JsonProperty("plotId")] public Guid PlotId { get; set; }
    [JsonProperty("xValue")] public Double XValue { get; set; }
    [JsonProperty("yValue")] public Double YValue { get; set; }
    [JsonProperty("timeStamp")] public DateTime TimeStamp { get; set; }
  }
}