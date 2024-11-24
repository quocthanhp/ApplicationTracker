using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Application
{
    public class UpdateApplicationRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Company Name cannot be over 10 over characters")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [MaxLength(20, ErrorMessage = "Job Title cannot be over 20 characters")]
        public string JobTitle { get; set; } = string.Empty;
        [Required]
        [MaxLength(10, ErrorMessage = "Location cannot be over 10 characters")]
        public string Location { get; set; } = string.Empty;
        [Required]
        [Range(0, 1000000000)]
        public decimal Salary { get; set; }
        public ApplicationStatus Status { get; set; }
        [Required]
        [MaxLength(280, ErrorMessage = "Notes cannot be over 280 characters")]
        public string Notes { get; set; } = string.Empty;
        [Required]
        [MaxLength(10000, ErrorMessage = "Job Description cannot be over 10000 characters")]
        public string JobDescription { get; set; } = string.Empty;
    }
}