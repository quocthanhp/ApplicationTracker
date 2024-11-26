using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace api.Helpers
{
    public class StringListConverter
    {
        public static List<string>? ConvertStringToList(string input)
        {
            return JsonConvert.DeserializeObject<List<string>>(input);
        }
    }
}