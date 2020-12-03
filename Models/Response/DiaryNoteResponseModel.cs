using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petthy.Models.Response
{
    public class DiaryNoteResponseModel
    {
        public DiaryNoteResponseModel(int petMedCardNoteId, int petId, string learntCommands,
            string advice, string comment, DateTime noteDate)
        {
            PetMedCardNoteId = petMedCardNoteId;
            PetId = petId;
            LearntCommands = learntCommands;
            Advice = advice;
            Comment = comment;
            NoteDate = noteDate;
        }

        public int PetMedCardNoteId { get; set; }
        public int PetId { get; set; }
        public string LearntCommands { get; set; }
        public string Advice { get; set; }
        public string Comment { get; set; }
        public DateTime NoteDate { get; set; }
    }
}
