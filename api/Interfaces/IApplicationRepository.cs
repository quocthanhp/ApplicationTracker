using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IApplicationRepository
    {
        Task<List<Application>> GetAllAsync();  
        Task<Application?> GetByIdAsync(int id); // ? means nullable because FirstOrDefaultAsync can return null
       
    }
}