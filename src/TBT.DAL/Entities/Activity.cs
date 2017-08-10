using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TBT.DAL.Repository.Interfaces;

namespace TBT.DAL.Entities
{
    public class Activity : IEntity
    {
        public int Id { get; set; }
        [Required]
        [StringLength(512)]
        [Index("Activity_Name_Index", IsUnique = false)]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int? ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
