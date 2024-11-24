using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Application;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationDBContext _context;
        public ApplicationRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Application>> GetAllAsync()
        {
            return await _context.Applications.ToListAsync();
        }

        public async Task<Application?> GetByIdAsync(int id)
        {
            return await _context.Applications.FindAsync(id);
        }

        public async Task<Application> CreateAsync(Application application)
        {
            await _context.Applications.AddAsync(application);
            await _context.SaveChangesAsync();
            return application;
        }

        public async Task<Application?> UpdateAsync(int id, UpdateApplicationRequestDto applicationDto)
        {
            var application = await _context.Applications.FirstOrDefaultAsync(a => a.Id == id);
            if (application == null)
            {
                return null;
            }
            application.CompanyName = applicationDto.CompanyName;
            application.JobTitle = applicationDto.JobTitle;
            application.Location = applicationDto.Location;
            application.Salary = applicationDto.Salary;
            application.Notes = applicationDto.Notes;
            application.JobDescription = applicationDto.JobDescription;
            await _context.SaveChangesAsync();
            return application;
        }

        public async Task<Application?> DeleteAsync(int id)
        {
            var application = await _context.Applications.FirstOrDefaultAsync(a => a.Id == id);
            if (application == null)
            {
                return null;
            }
            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();
            return application;
        }
    }
}