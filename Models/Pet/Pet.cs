using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Petthy.Models.Pet
{
    public class Pet
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int PetId { get; set; }
        public string PetName { get; set; }
        public string AnimalKind { get; set; }
        public string PetSex { get; set; }
        public int PetAge { get; set; }

        [ForeignKey("ClientId")]
        public int ClientId { get; set; }
        public PetDiaryNote[] PetDiaryNotes { get; set; }
        public PetMedCardNote[] PetMedCardNotes { get; set; }

        //public Pet(int id, string name, string animalKind, string sex, int age)
        //{
        //    this.id = id;
        //    this.name = name;
        //    this.animalKind = animalKind;
        //    this.sex = sex;
        //    this.age = age;
        //}
    }
}
