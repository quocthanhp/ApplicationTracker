using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface IKeyPhaseExtraction
    {
        public Task<List<string>> GetKeyPhaseAsync(string jobDescription);
    }
}