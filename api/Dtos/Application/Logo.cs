using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Application
{
    public class Logo
    {
        public string name { get; set; } = String.Empty;
        public string domain { get; set; } = String.Empty;
        public string logo_url { get; set; } = String.Empty;
    }
}