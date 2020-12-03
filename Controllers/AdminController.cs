using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Petthy.Data;
using Petthy.Models.Pet;
using Petthy.Models.Professional;
using Petthy.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petthy.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;

        public AdminController(RoleManager<IdentityRole> manager, ApplicationDbContext dbContext)
        {
            _roleManager = manager;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> GetRoles()
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = "admin", NormalizedName = "ADMIN" });
            return View(await _roleManager.Roles.ToListAsync());
        }

        [HttpPost]
        [Route("ChangeProfessionalAccount")]
        public void ChangeProfessionalAccount(ProfileEditingRequestModel request)
        {
            Professional chosenProfessionalAccount = _dbContext.Professionals.Find(request.ProfessionalId);

            if (chosenProfessionalAccount == null)
            {
                throw new ArgumentException("Something went wrong. Try again");
            }

            chosenProfessionalAccount.FirstName = request.FirstName;
            chosenProfessionalAccount.LastName = request.LastName;
            //chosenProfessionalAccount.Email = request.Email;
            chosenProfessionalAccount.PhoneNumber = request.PhoneNumber;
            //chosenProfessionalAccount.Password = request.Password;
            chosenProfessionalAccount.Workplace = request.Workplace;

            _dbContext.Professionals.Update(chosenProfessionalAccount);
            _dbContext.SaveChanges();
        }


        [HttpDelete]
        [Route("DeleteProfessionalAccount")]
        public void DeleteProfessionalAccount(int ProfessionalId)
        {
            Professional chosenProfessional = _dbContext.Professionals.Find(ProfessionalId);

            if (chosenProfessional == null)
            {
                throw new ArgumentException("There was an error. Try again");
            }

            _dbContext.Professionals.Remove(chosenProfessional);
            _dbContext.Professionals.Update(chosenProfessional);
            _dbContext.SaveChanges();
        }

        [HttpDelete]
        [Route("DeleteDiaryNote")]
        public void DeleteDiaryNote(int medNoteId)
        {
            PetDiaryNote chosenDiaryNote = _dbContext.PetDiaryNotes.Find(medNoteId);

            if (chosenDiaryNote == null)
            {
                throw new ArgumentException("There was an error. Try again");
            }

            _dbContext.PetDiaryNotes.Remove(chosenDiaryNote);
            _dbContext.PetDiaryNotes.Update(chosenDiaryNote);
            _dbContext.SaveChanges();
        }

        [HttpDelete]
        [Route("DeleteMedNote")]
        public void DeleteMedNote(int medNoteId)
        {
            PetMedCardNote chosenMedNote = _dbContext.PetMedCardNotes.Find(medNoteId);

            if (chosenMedNote == null)
            {
                throw new ArgumentException("There was an error. Try again");
            }

            _dbContext.PetMedCardNotes.Remove(chosenMedNote);
            _dbContext.PetMedCardNotes.Update(chosenMedNote);
            _dbContext.SaveChanges();
        }
    }
}
