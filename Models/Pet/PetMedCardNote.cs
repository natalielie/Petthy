﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petthy.Models.Pet
{
    public class PetMedCardNote
    {
        public int PetMedCardNoteId { get; set; }
        public int PetId { get; set; }
        public string Illness { get; set; }
        public string Treatment { get; set; }
        public string Comment { get; set; }
        public DateTime NoteDate { get; set; }

    }
}
