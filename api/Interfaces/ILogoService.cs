using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface ILogoService
    {
        Task<string> GetLogoAsync(string companyName);
    }
}