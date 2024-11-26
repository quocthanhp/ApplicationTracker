using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Data.Enum;
using api.Dtos.Application;
using api.Helpers;
using api.Models;
using api.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace api.Tests.Repository
{
    public class ApplicationRepositoryTests
    {
        private async Task<ApplicationDBContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDBContext(options);
            databaseContext.Database.EnsureCreated();
            
            for (int i = 0; i < 10; i++)
            {
                databaseContext.Applications.Add(new Application
                {
                    CompanyName = $"Company 1",
                    JobTitle = $"Job 1",
                    Location = $"Location 1",
                    Salary = 1000,
                    Status = ApplicationStatus.Applied,
                    Notes = $"Notes 1",
                    JobDescription = $"Job Description 1",
                    UserId = "user1"
                });
                await databaseContext.SaveChangesAsync();
            }
        
            return databaseContext;
        }


        [Fact]
        public async Task GetAllAsync_ShouldReturnApplicationsForUser()
        {
            var dbContext = await GetDbContext();
            var repository = new ApplicationRepository(dbContext);

            var result = await repository.GetAllAsync(new QueryObject(), "user1");

            Assert.Equal(10, result.Count);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddApplication()
        {
            var dbContext = await GetDbContext();
            var repository = new ApplicationRepository(dbContext);

            var application = new Application
            {
                CompanyName = "Company 1",
                JobTitle = "Job 1",
                Location = "Location 1",
                Salary = 1000,
                Status = ApplicationStatus.Applied,
                Notes = "Notes 1",
                JobDescription = "Job Description 1",
                UserId = "user1"
            };

            var result = await repository.CreateAsync(application);

            Assert.Equal(11, await dbContext.Applications.CountAsync());
            Assert.Equal(application, result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnApplicationForUser()
        {
            var dbContext = await GetDbContext();
            var repository = new ApplicationRepository(dbContext);

            var result = await repository.GetByIdAsync(1, "user1");

            Assert.NotNull(result);
            Assert.IsType<Application>(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNullIfApplicationNotFound()
        {
            var dbContext = await GetDbContext();
            var repository = new ApplicationRepository(dbContext);

            var result = await repository.GetByIdAsync(100, "user1");

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateApplication()
        {
            var dbContext = await GetDbContext();
            var repository = new ApplicationRepository(dbContext);

            var application = new UpdateApplicationRequestDto
            {
                CompanyName = "Company 2",
                JobTitle = "Job 2",
                Location = "Location 2",
                Salary = 2000,
                Notes = "Notes 2",
                JobDescription = "Job Description 2"
            };

            var result = await repository.UpdateAsync(1, application);

            Assert.NotNull(result);
            Assert.Equal("Company 2", result.CompanyName);
            Assert.Equal("Job 2", result.JobTitle);
            Assert.Equal("Location 2", result.Location);
            Assert.Equal(2000, result.Salary);
            Assert.Equal("Notes 2", result.Notes);
            Assert.Equal("Job Description 2", result.JobDescription);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnNullIfApplicationNotFound()
        {
            var dbContext = await GetDbContext();
            var repository = new ApplicationRepository(dbContext);

            var application = new UpdateApplicationRequestDto
            {
                CompanyName = "Company 2",
                JobTitle = "Job 2",
                Location = "Location 2",
                Salary = 2000,
                Notes = "Notes 2",
                JobDescription = "Job Description 2"
            };

            var result = await repository.UpdateAsync(100, application);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteApplication()
        {
            var dbContext = await GetDbContext();
            var repository = new ApplicationRepository(dbContext);

            var result = await repository.DeleteAsync(1);

            Assert.NotNull(result);
            Assert.Equal(9, await dbContext.Applications.CountAsync());
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnNullIfApplicationNotFound()
        {
            var dbContext = await GetDbContext();
            var repository = new ApplicationRepository(dbContext);

            var result = await repository.DeleteAsync(100);

            Assert.Null(result);
        }
    }
}