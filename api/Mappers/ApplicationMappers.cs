using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Application;
using api.Models;

namespace api.Mappers
{
    public static class ApplicationMappers
    {
        public static ApplicationDto ToApplicationDto(this Application application)
        {
            return new ApplicationDto
            {
                Id = application.Id,
                Logo = application.Logo,
                CompanyName = application.CompanyName,
                JobTitle = application.JobTitle,
                Location = application.Location,
                Salary = application.Salary,
                Status = application.Status,
                Notes = application.Notes,
                JobDescription = application.JobDescription
            };
        }

        public static Application ToApplicationFromCreateDto(this CreateApplicationRequestDto createApplicationRequestDto)
        {
            return new Application
            {
                // Logo = createApplicationRequestDto.Logo,
                CompanyName = createApplicationRequestDto.CompanyName,
                JobTitle = createApplicationRequestDto.JobTitle,
                Location = createApplicationRequestDto.Location,
                Salary = createApplicationRequestDto.Salary,
                Status = Data.Enum.ApplicationStatus.Applied,
                Notes = string.Empty,
                JobDescription = createApplicationRequestDto.JobDescription
            };
        }
    }
}