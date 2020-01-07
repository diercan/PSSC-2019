﻿using System;
using System.Collections.Generic;
using System.Text;
 using ml_stats_core.Models;
 using Newtonsoft.Json;

namespace ml_stats_core.Models
{
	public class User : Entity
	{
		[JsonProperty("userName")] public string UserName { get; set; }
	}
}
