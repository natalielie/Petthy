using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petthy.Models.Response
{
    public class AppointmentAddingResponseModel
    {
        public AppointmentAddingResponseModel(int petId, int professionalId, DateTime appointmentDateTime)
        {
            PetId = petId;
            ProfessionalId = professionalId;
            AppointmentDateTime = appointmentDateTime;
        }

        public int PetId { get; }

        public int ProfessionalId { get; }
        public DateTime AppointmentDateTime { get; }
    }
}
