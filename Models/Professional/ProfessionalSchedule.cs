using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Petthy.Models.Professional
{
    public class ProfessionalSchedule
    {
        [Key]
        public int ProfessionalId { get; set; }
        [Key]
        public string Weekday { get; set; }
        [Key]
        public DateTime DateTimeBegin { get; set; }
        [Key]
        public DateTime DateTimeEnd { get; set; }

    }
}
