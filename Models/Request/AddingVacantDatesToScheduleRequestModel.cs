using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petthy.Models.Request
{
    public class AddingVacantDatesToScheduleRequestModel
    {
        //public int ProfessionalId { get; set; }
        public string Weekday { get; set; }
        public DateTime DateTimeBegin { get; set; }
        public DateTime DateTimeEnd { get; set; }
    }
}
