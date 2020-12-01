using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Petthy.Models.Professional
{
    public class Professional : IdentityUser
    {
        public int ProfessionalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Workplace { get; set; }
       // public string Email { get; set; }
        //public string Password { get; set; }
        public int ProfessionalRoleId { get; set; }

        public int? RoleId { get; set; }
        public Role Role { get; set; }

        public Professional() : base() { }
        public Professional(int professionalId, string firstName, string lastName, string workplace, int professionalRoleId)
            : base()
        {
            this.ProfessionalId = professionalId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Workplace = workplace;
            //this.Email = Email;
           // this.Password = Password;
            this.ProfessionalRoleId = professionalRoleId;
        }

    }
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Professional> Users { get; set; }
        public Role()
        {
            Users = new List<Professional>();
        }
    }

}
