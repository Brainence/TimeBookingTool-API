using System.Collections.Generic;
using TBT.Business.Interfaces;

namespace TBT.Business.Models.BusinessModels
{
    public class CompanyModel: IModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public bool IsActive { get; set; }
        public List<ProjectModel> Projects { get; set; }
        public List<UserModel> Users { get; set; }
        public List<CustomerModel> Customers { get; set; }

        public override string ToString()
        {
            return $"{{ Id={Id}, CompanyName={CompanyName} }}";
        }
    }
}
