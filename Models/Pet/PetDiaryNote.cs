using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petthy.Models.Pet
{
    public class PetDiaryNote
    {
        public int PetDiaryNoteId { get; set; }
        public int PetId { get; set; }
        public string LearntCommands { get; set; }
        public string Advice { get; set; }
        public string Comment { get; set; }
        public DateTime NoteDate { get; set; }
    }
}
