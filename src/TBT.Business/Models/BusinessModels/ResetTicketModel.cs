using System;
using TBT.Business.Interfaces;

namespace TBT.Business.Models.BusinessModels
{
    public class ResetTicketModel : IModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsUsed { get; set; }

        public override string ToString()
        {
            return $"{{ Id={Id}, Username={Username}, Token={Token}, ExpirationDate={ExpirationDate}, IsUsed={IsUsed} }}";
        }
    }
}
