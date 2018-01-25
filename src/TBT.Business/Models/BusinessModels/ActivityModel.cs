using TBT.Business.Interfaces;

namespace TBT.Business.Models.BusinessModels
{
    public class ActivityModel : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        //public ProjectModel Project { get; set; }

        public int? ProjectId { get; set; }

        public override string ToString()
        {
            return $"{{ Id={Id}, Name={Name}, IsActive={IsActive}";
        }
    }
}
