using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TBT.DAL.Repository.Interfaces;

namespace TBT.DAL.Entities
{
    public class Customer : IEntity
    {
        public int Id { get; set; }
        [Required]
        [StringLength(512)]
        [Index("Customer_Name_Index", IsUnique = true)]
        public string Name { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public bool IsActive { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
