using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petthy.Data;
using Petthy.Models.Professional;
using Petthy.Models.Pet;
using Petthy.Models.Request;
using Petthy.Models.Response;
using Microsoft.AspNetCore.Identity;

namespace Petthy.Controllers.Api
{
    [Route("api/doctor")]
    [ApiController]
    public class ProfessionalController : ControllerBase
    {
        private readonly UserManager<Professional> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public ProfessionalController(ApplicationDbContext dbContext, UserManager<Professional> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

       /* [HttpPost]
        [Route("addVacantDatesToSchedule")]
        public void addVacantDatesToSchedule(AddingVacantDatesToScheduleRequestModel request)
        {
            string userIdStringified = _userManager.GetUserId(User);

            int userId = int.Parse(userIdStringified);


            ProfessionalSchedule schedule = new ProfessionalSchedule
            {
                ProfessionalId = userId,
                Weekday = request.Weekday,
                DateTimeBegin = request.DateTimeBegin,
                DateTimeEnd = request.DateTimeEnd
            };

            _dbContext.ProfessionalSchedules.Add(schedule);
            _dbContext.SaveChanges();
        }*/
        [HttpPost]
        [Route("addVacantDatesToSchedule")]
        public void addVacantDatesToSchedule(string Weekday, DateTime DateTimeBegin, DateTime DateTimeEnd)
        {
            string userIdStringified = _userManager.GetUserId(User);

            int userId = int.Parse(userIdStringified);


            ProfessionalSchedule schedule = new ProfessionalSchedule
            {
                ProfessionalId = userId,
                Weekday = Weekday,
                DateTimeBegin = DateTimeBegin,
                DateTimeEnd = DateTimeEnd
            };

            _dbContext.ProfessionalSchedules.Add(schedule);
            _dbContext.SaveChanges();
        }

        [HttpGet]
        [Route("getSchedule")]
        public List<ProfessionalScheduleResponseModel> getVacantDatesOfSchedule(int professionalId)
        {
            List<ProfessionalSchedule> schedules = _dbContext.ProfessionalSchedules.Where(x => x.ProfessionalId == professionalId).ToList();

            List<ProfessionalScheduleResponseModel> responseModels = schedules
                .Select(x => new ProfessionalScheduleResponseModel(x.ProfessionalId, x.Weekday, x.DateTimeBegin, x.DateTimeEnd))
                .ToList();

            return responseModels;
        }


        [HttpPost]
        [Route("assignPetToDoctor")]
        public void AssignPetToDoctor(PetAssignmentRequestModel request)
        {
            string userIdStringified = _userManager.GetUserId(User);

            int userId = int.Parse(userIdStringified);

            Pet pet = _dbContext.Pets.Find(request.PetId);

            if (pet.ClientId != userId)
            {
                throw new ArgumentException("Pet doesn't belong to the current user.");
            }

            PetAssignment assignment = new PetAssignment
            {
                PetId = request.PetId,
                ProfessionalId = request.ProfessionalId
            };

            _dbContext.PetAssignments.Add(assignment);
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

        [HttpDelete]
        [Route("DeletePetAssignment")]
        public void DeletePetAssignment(PetAssignmentRequestModel request)
        {
            PetAssignment chosenPet = _dbContext.PetAssignments.Find(request.ProfessionalId, request.PetId);

            if (chosenPet == null)
            {
                throw new ArgumentException("There was an error. Try again");
            }

            _dbContext.PetAssignments.Remove(chosenPet);
            _dbContext.PetAssignments.Update(chosenPet);
            _dbContext.SaveChanges();
        }

        /* [HttpGet]
         [Route("getProfessionalSchedule")]
         public List<AppointmentAddingResponseModel> GetProfessionalAppointments(int professionalId)
         {
             List<ProfessionalAppointment> professionalAppointments = _dbContext.ProfessionalAppointments.Where(
                 x => x.ProfessionalId == professionalId).ToList();

             List<AppointmentAddingResponseModel> responseModels = professionalAppointments
                 .Select(x => new AppointmentAddingResponseModel(x.PetId, x.ProfessionalId, x.AppointmentDateTime))
                 .ToList();

             return responseModels;
         }*/

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
            chosenProfessionalAccount.Email = request.Email;
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
    }
}
