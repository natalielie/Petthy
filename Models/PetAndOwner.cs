using Petthy.Models.Pet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petthy.Models
{
    public class PetAndOwner
    {
        public Client Owner { get; set; }
        public Pet.Pet Pet { get; set; }

    }
}
