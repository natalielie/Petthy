using System;
using System.ComponentModel.DataAnnotations;

namespace Petthy.Models.Pet
{
    public class PetAssignment
    {
        [Key]
        public int PetId { get; set; }

        [Key]
        public int ProfessionalId { get; set; }

    }
}
