using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandonApi.Models;
using Newtonsoft.Json;

namespace LandonAPI.Models
{
    public abstract class Resource : Link
    {
        [JsonIgnore]
        public Link Self { get; set; }  
    }
}
