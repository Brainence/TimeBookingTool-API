using System.ComponentModel.DataAnnotations;

namespace TBT.DAL.Repository.Interfaces
{
    public interface IEntity
    {
        [Key]
        int Id { get; set; }
    }
}
