using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ml_stats_core.Models
{
  public class Experiment : Entity
  {
    [JsonProperty("createdById")] public Guid CreatedById { get; set; }
    [JsonProperty("createdAt")] public DateTime CreatedAt { get; set; }
    [JsonProperty("tfModelArchitecture")] public JObject TfModelArchitecture { get; set; }
    [JsonProperty("trainingParams")] public JObject TrainingParams { get; set; }
    [JsonProperty("isRunning")] public bool IsRunning { get; set; }
  }
}