using System;
using TBT.Business.Interfaces;

namespace TBT.Business.Models.BusinessModels
{
    public class TimeEntryModel : IModel
    {
        public int Id { get; set; }
        public UserModel User { get; set; }
        public ActivityModel Activity { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
        public bool IsRunning { get; set; }
        public string Comment { get; set; }
        public DateTime? LastUpdated { get; set; }

        public override string ToString()
        {
            return $"{{ Id={Id}, User={User}, Activity={Activity}, Duration={Duration}, Date={Date}, IsActive={IsActive}, LastUpdated={LastUpdated} }}";
        }
    }
}
