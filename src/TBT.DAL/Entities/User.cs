using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TBT.DAL.Repository.Interfaces;

namespace TBT.DAL.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        [Required]
        [StringLength(512)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(512)]
        public string LastName { get; set; }
        [Required]
        [StringLength(512)]
        [Index("User_Username_Index", IsUnique = true)]
        public string Username { get; set; }
        [Required]
        [StringLength(512)]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<TimeEntry> TimeEntries { get; set; }
        public int TimeLimit { get; set; }
        public TimeSpan? CurrentTimeZone { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
        public decimal? MonthlySalary { get; set; } 
        public User()
        {
            Projects = new List<Project>();
            TimeEntries = new List<TimeEntry>();
        }
    }
}
