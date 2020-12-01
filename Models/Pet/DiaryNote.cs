using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petthy.Models.Pet
{
    public class DiaryNote
    {
        public string[] LearntCommands { get; set; }
        public string Advice { get; set; }
        public string Comment { get; set; }
        public Appointment Appointment { get; set; }
    }
}
