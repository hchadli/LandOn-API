using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandonAPI.Models;

namespace LandonApi.Models
{
    public class RootResponse : Resource
    {
        public Link Info { get; set; }

        public Link Rooms { get; set; }
    }
}
