using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TBT.DAL.Repository.Interfaces;

namespace TBT.DAL.Entities
{
    public class Project : IEntity
    {
        public int Id { get; set; }
        [Required]
        [StringLength(512)]
        [Index("Project_Name_Index", IsUnique = false)]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Activity> Activities { get; set; }
        public ICollection<User> Users { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Project()
        {
            Activities = new List<Activity>();
            Users = new List<User>();
        }
    }
}
