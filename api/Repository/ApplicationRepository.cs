using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Application;
using api.Helpers;
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
        public async Task<List<Application>> GetAllAsync(QueryObject query, string userId)
        {
            var applications = _context.Applications.Where(a => a.UserId == userId).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                applications = applications.Where(a => a.CompanyName.Contains(query.CompanyName));
            }

            return await applications.ToListAsync();
        }

        public async Task<Application?> GetByIdAsync(int id, string userId)
        {
            return await _context.Applications
                    .Where(a => a.Id == id && a.UserId == userId)
                    .FirstOrDefaultAsync(); 
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