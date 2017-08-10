using System;
using System.ComponentModel.DataAnnotations;
using TBT.DAL.Repository.Interfaces;

namespace TBT.DAL.Entities
{
    public class ResetTicket : IEntity
    {
        public int Id { get; set; }
        [Required]
        [StringLength(512)]
        public string Username { get; set; }
        [Required]
        [StringLength(64)]
        public string Token { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        [Required]
        public bool IsUsed { get; set; }
    }
}
