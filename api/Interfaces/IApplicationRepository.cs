using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Application;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IApplicationRepository
    {
        Task<List<Application>> GetAllAsync(QueryObject query);  
        Task<Application?> GetByIdAsync(int id); // ? means nullable because FirstOrDefaultAsync can return null
        Task<Application> CreateAsync(Application application);
        Task<Application?> UpdateAsync(int id, UpdateApplicationRequestDto applicationDto);
        Task<Application?> DeleteAsync(int id);

    }
}