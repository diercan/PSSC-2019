using System;
using FrBaschet.Domain.Models;
using Newtonsoft.Json;

namespace FrBaschet.Domain.Entities
{
    public class ScheduleEventModel : EntityModel
    {
        [JsonProperty(PropertyName = "title")] public string Title { get; set; }

        [JsonProperty(PropertyName = "start")] public DateTime? Start { get; set; }

        [JsonProperty(PropertyName = "end")] public DateTime? End { get; set; }

        [JsonProperty(PropertyName = "url")] public string Url { get; set; }

        public string Code { get; set; }
    }
}