﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ml_stats_core.Models
{
  public class Plot : Entity
  {
    [JsonProperty("title")] public string Title { get; set; }
    [JsonProperty("xAxisTitle")] public string XAxisTitle { get; set; }
    [JsonProperty("yAxisTitle")] public string YAxisTitle { get; set; }
  }
}