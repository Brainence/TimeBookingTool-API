using System.Collections.Generic;
using TBT.Business.Interfaces;

namespace TBT.Business.Models.BusinessModels
{
    public class CustomerModel : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProjectModel> Projects { get; set; }
        public bool IsActive { get; set; }
    }
}
