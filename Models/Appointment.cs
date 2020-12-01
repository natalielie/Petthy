using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petthy.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int PetId { get; set; }
        public int ProfessionalId { get; set; }
        public DateTime Date { get; set; }


    }
}
