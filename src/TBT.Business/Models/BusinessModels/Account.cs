using TBT.Business.Interfaces;

namespace TBT.Business.Models.BusinessModels
{
    public class Account : IModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public override string ToString()
        {
            return $"{{ Id={Id}, Username={Username}, Password={Password}, IsActive={IsActive} }}";
        }
    }
}
