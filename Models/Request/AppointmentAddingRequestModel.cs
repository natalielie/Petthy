using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petthy.Models.Request
{
    public class AppointmentAddingRequestModel
    {
        public int PetId { get; set; }
        public int ProfessionalId { get; set; }

        public DateTime AppointmentDateTime { get; set; }
        public DateTime NewAppointmentDateTime { get; set; }
        
    }
}
