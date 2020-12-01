using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Petthy.Models.Professional
{
    public class ProfessionalRole
    {
        public int ProfessionalRoleId { get; set; }
        public string professionalRole { get; set; }
    }
}
