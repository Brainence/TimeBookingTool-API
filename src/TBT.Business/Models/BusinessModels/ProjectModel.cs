using System.Collections.Generic;
using TBT.Business.Interfaces;

namespace TBT.Business.Models.BusinessModels
{
    public class ProjectModel : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<ActivityModel> Activities { get; set; }
        public List<UserModel> Users { get; set; }
        public CustomerModel Customer { get; set; }

        public override string ToString()
        {
            return $"{{ Id={Id}, Name={Name}, IsActive={IsActive}, Customer={Customer} }}";
        }
    }
}
