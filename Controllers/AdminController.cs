using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Petthy.Data;
using Petthy.Models.Pet;
using Petthy.Models.Professional;
using Petthy.Models.Request;
using System;
using System.Threading.Tasks;

namespace Petthy.Controllers.Api
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


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("ChangeProfessionalAccount")]
        public void ChangeProfessionalAccount(ProfileEditingRequestModel request)
        {

            Professional chosenProfessionalAccount = _dbContext.Professionals.Find(request.ProfessionalId);

            if (chosenProfessionalAccount == null)
            {
                throw new ArgumentException("Something went wrong. Try again");
            }
            Professional chosenUser = new Professional
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Workplace = request.Workplace,
                ProfessionalRoleId = request.ProfessionalRoleId
            };

            _dbContext.Professionals.Remove(chosenProfessionalAccount);
            _dbContext.Professionals.Add(chosenUser);
            _dbContext.SaveChanges();
        }


        [Authorize(Roles = "Administrator")]
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
        public void DeleteDiaryNote(int DiaryNoteId)
        {
            PetDiaryNote chosenDiaryNote = _dbContext.PetDiaryNotes.Find(DiaryNoteId);

            if (chosenDiaryNote == null)
            {
                throw new ArgumentException("There was an error. Try again");
            }

            _dbContext.PetDiaryNotes.Remove(chosenDiaryNote);
            _dbContext.SaveChanges();
        }


        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        [Route("DeleteMedNote")]
        public void DeleteMedNote(int medNoteId)
        {
            PetMedCardNote chosenMedNote = _dbContext.PetMedCardNotes.Find(medNoteId);

            if (chosenMedNote == null)
            {
                throw new ArgumentException("There was an error. Try again");
            }

            _dbContext.SaveChanges();
        }
    }
}
