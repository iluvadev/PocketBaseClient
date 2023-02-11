﻿using pocketbase_csharp_sdk.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace pocketbase_csharp_sdk.Models.Log
{
    public class LogRequestStatistic
    {
        public int? Total { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Date { get; set; }
    }
}
