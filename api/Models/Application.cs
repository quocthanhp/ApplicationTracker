using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.Data.Enum;

namespace api.Models
{
    public class Application
    {
        public int Id { get; set; }
        public string Logo { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }
        public ApplicationStatus Status { get; set; }
        public string Notes { get; set; } = string.Empty;
        public string JobDescription { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public string UserId { get; set; } = string.Empty;
        public AppUser? User { get; set; }

        public List<string> Keywords { get; set; } = new List<string>();
    }
}