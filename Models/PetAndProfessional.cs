using Petthy.Models.Pet;
using Petthy.Models.Professional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petthy.Models
{
    public class PetAndProfessional
    {
        public Professional.Professional Professional { get; set; }
        public Pet.Pet Pet { get; set; }
        public int AppointmentId { get; set; }
        public DateTime DateTimeBegin { get; set; }
    }
}
