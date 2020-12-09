using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Petthy.Data;
using Petthy.Models.Professional;
using Petthy.Models.Pet;
using Petthy.Models.Request;
using Petthy.Models.Response;
using Microsoft.AspNetCore.Identity;
using Petthy.Models;
using Petthy.Models.SmartDevice;

namespace Petthy.Controllers.Api
{
    [Route("api/specialist")]
    [ApiController]
    public class SpecialistController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public SpecialistController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("getPetAssignments")]
        public List<PetAssignmentResponseModel> GetPetAssignments(int petId)
        {
            List<PetAssignment> petAssignments = _dbContext.PetAssignments.Where(x => x.PetId == petId).ToList();

            List<PetAssignmentResponseModel> responseModels = petAssignments
                .Select(x => new PetAssignmentResponseModel(x.PetId, x.ProfessionalId))
                .ToList();

            return responseModels;
        }


        // DiaryNotes //

        [HttpPost]
        [Route("addDiaryNote")]
        public void addDiaryNote(DiaryNoteRequestModel request)
        {
            string userIdStringified = _userManager.GetUserId(User);
            Professional currentUser = _dbContext.Professionals.SingleOrDefault(x => x.UserId == userIdStringified);
            List<PetAssignmentResponseModel> petAssignments = GetPetAssignments(request.PetId);
            if (petAssignments.Find(x => x.ProfessionalId == currentUser.ProfessionalId) != null)
            {
                PetDiaryNote petDiaryNote = new PetDiaryNote
                {
                    PetId = request.PetId,
                    LearntCommands = request.LearntCommands,
                    Advice = request.Advice,
                    Comment = request.Comment,
                    NoteDate = request.NoteDate
                };

                _dbContext.PetDiaryNotes.Add(petDiaryNote);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("You weren't assigned to this pet to write a diary note for it");
            }
        }

        [HttpGet]
        [Route("getDiaryNote")]
        public List<DiaryNoteResponseModel> getPetDiaryNotes(int petId)
        {
            List<PetDiaryNote> medNotes = _dbContext.PetDiaryNotes.Where(x => x.PetId == petId).ToList();

            List<DiaryNoteResponseModel> responseModels = medNotes
                .Select(x => new DiaryNoteResponseModel(x.PetDiaryNoteId, x.PetId,
                x.LearntCommands, x.Advice, x.Comment, x.NoteDate))
                .ToList();
            return responseModels;
        }

        [HttpGet]
        [Route("checkSmartDeviceDataByPet")]
        public List<SmartDeviceDataResponseModel> checkPetSmartDeviceDataByPetId(int petId)
        {
            List<SmartDeviceData> medNotes = _dbContext.SmartDeviceData.Where(x => x.PetId == petId).ToList();

            List<SmartDeviceDataResponseModel> responseModels = medNotes
                .Select(x => new SmartDeviceDataResponseModel(x.SmartDeviceDataId, x.PetId,
                x.IsIll, x.IsEnoughWalking, x.SmartDeviceDataDate))
                .ToList();
            return responseModels;
        }

        [HttpGet]
        [Route("checkSmartDeviceDataByDate")]
        public List<SmartDeviceDataResponseModel> checkSmartDeviceDataByDate(int petId, DateTime date)
        {
            List<SmartDeviceData> medNotes = _dbContext.SmartDeviceData.Where(x => x.PetId == petId
            && x.SmartDeviceDataDate == date).ToList();

            List<SmartDeviceDataResponseModel> responseModels = medNotes
                .Select(x => new SmartDeviceDataResponseModel(x.SmartDeviceDataId, x.PetId,
                x.IsIll, x.IsEnoughWalking, x.SmartDeviceDataDate))
                .ToList();
            return responseModels;
        }
    }
}
