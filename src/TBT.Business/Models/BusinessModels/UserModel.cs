using System;
using System.Collections.Generic;
using TBT.Business.Interfaces;

namespace TBT.Business.Models.BusinessModels
{
    public class UserModel : IModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public List<ProjectModel> Projects { get; set; }
        public List<TimeEntryModel> TimeEntries { get; set; }
        public int TimeLimit { get; set; }
        public TimeSpan? CurrentTimeZone { get; set; }
        public CompanyModel Company { get; set; }

        public decimal? MonthlySalary { get; set; }
        public bool IsBlocked { get; set; }
        public override string ToString()
        {
            return $"{{ Id={Id}, Username={Username}, Password={Password}, IsAdmin={IsAdmin}, IsActive={IsActive}, Projects=[ {string.Join(";", Projects ?? new List<ProjectModel>())} ], Company={Company}, MonthlySalary={MonthlySalary}, IsBlocked={IsBlocked}}}";
        }
    }
}
