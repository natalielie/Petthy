using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petthy.Models.Response
{
    public class ProfessionalScheduleResponseModel
    {
        public ProfessionalScheduleResponseModel(int professionalId, string weekday, DateTime dateTimeBegin, DateTime dateTimeEnd)
        {
            ProfessionalId = professionalId;
            Weekday = weekday;
            DateTimeBegin = dateTimeBegin;
            DateTimeEnd = dateTimeEnd;
        }
        public int ProfessionalId { get; }
        public string Weekday { get; set; }
        public DateTime DateTimeBegin { get; set; }
        public DateTime DateTimeEnd { get; set; }
    }
}
