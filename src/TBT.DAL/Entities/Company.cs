using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBT.DAL.Repository.Interfaces;

namespace TBT.DAL.Entities
{
    public class Company : IEntity
    {
        public int Id { get; set; }
        [Required]
        [StringLength(512)]
        [Index("Company_CompanyName_Index", IsUnique = true)]
        public string CompanyName { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
