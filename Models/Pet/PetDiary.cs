using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petthy.Models.Pet
{
    public class PetDiary
    {
        public int PetDiaryId { get; set; }
        public int PetId { get; set; }
        public DiaryNote[] PetDiaryNote { get; set; }
    }
}
