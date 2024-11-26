using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data.Enum;

namespace api.Dtos.Application
{
    public class ApplicationDto
    {
        public int Id { get; set; }
        public string Logo { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public ApplicationStatus Status { get; set; }
        public string Notes { get; set; } = string.Empty;
        public string JobDescription { get; set; } = string.Empty;
        public List<string> Keywords { get; set; } = new List<string>();
    }
}