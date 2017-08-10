using System;
using System.ComponentModel.DataAnnotations;
using TBT.DAL.Repository.Interfaces;

namespace TBT.DAL.Entities
{
    public class TimeEntry : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
        public bool IsRunning { get; set; }
        [StringLength(2048)]
        public string Comment { get; set; }
        public DateTime? TimeLimit { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
