using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Petthy.Models.Professional
{
    public class ProfessionalAppointment
    {
        [Key]
        public int PetId { get; set; }

        [Key]
        public int ProfessionalId { get; set; }

        [Key]
        public DateTime AppointmentDateTime { get; set; }
    }
}
