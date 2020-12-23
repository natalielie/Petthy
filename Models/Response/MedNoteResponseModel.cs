using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petthy.Models.Response
{
    public class MedNoteResponseModel
    {
        public MedNoteResponseModel(int petMedCardNoteId, int petId, string petName, string illness, 
            string treatment, string comment, DateTime noteDate)
        {
            PetMedCardNoteId = petMedCardNoteId;
            PetId = petId;
            PetName = petName;
            Illness = illness;
            Treatment = treatment;
            Comment = comment;
            NoteDate = noteDate;
        }

        public int PetMedCardNoteId { get; set; }
        public int PetId { get; set; }
        public string PetName { get; set; }
        public string Illness { get; set; }
        public string Treatment { get; set; }
        public string Comment { get; set; }
        public DateTime NoteDate { get; set; }
    }
}
