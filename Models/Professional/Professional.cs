using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Petthy.Models.Professional
{
    public class Professional
    {
        public int ProfessionalId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Workplace { get; set; }
        public int ProfessionalRoleId { get; set; }

        public Professional() { }
        public Professional(int professionalId, string userId, string firstName, 
            string lastName, string workplace, int professionalRoleId)
        {
            this.ProfessionalId = professionalId;
            this.UserId = userId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Workplace = workplace;
            this.ProfessionalRoleId = professionalRoleId;
        }

    }
   /* public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Professional> Users { get; set; }
        public Role()
        {
            Users = new List<Professional>();
        }
    }*/

}
