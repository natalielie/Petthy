using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petthy.Models.Pet
{
    public class MedNote
    {
        public string Illness { get; set; }
        public string Treatment { get; set; }
        public string Comment { get; set; }
        public Appointment Appointment { get; set; }
    }
}
