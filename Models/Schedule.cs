using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petthy.Models
{
    public class Schedule
    {
        public Professional.Professional Professional { get; set; }
        public DateTime DateTimeBegin { get; set; }
        public DateTime DateTimeEnd { get; set; }

    }
}
