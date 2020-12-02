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
using Petthy.Models;
using Petthy.Models.SmartDevice;

namespace Petthy.Controllers.Api
{
    [Route("api/doctor")]
    [ApiController]
    public class ProfessionalController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public ProfessionalController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        // Schedule //

        [HttpPost]
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

        // assignment //

        [HttpPost]
        [Route("assignPetToDoctor")]
        public void AssignPetToDoctor(PetAssignmentRequestModel request)
        {
            /*string userIdStringified = _userManager.GetUserId(User);

            int userId = int.Parse(userIdStringified);*/


            Pet pet = _dbContext.Pets.Find(request.PetId);

            if (pet.ClientId != 1)
            {
                throw new ArgumentException("Pet doesn't belong to the current user.");
            }

            PetAssignment assignment = new PetAssignment
            {
                PetId = request.PetId,
                ProfessionalId = request.ProfessionalId
            };

            _dbContext.PetAssignments.Add(assignment);
            _dbContext.SaveChanges();
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
            //_dbContext.PetAssignments.Update(chosenPet);
            _dbContext.SaveChanges();
        }

        // Appointments //

         [HttpGet]
         [Route("getProfessionalSchedule")]
         public List<AppointmentAddingResponseModel> GetProfessionalAppointments(int professionalId)
         {
             List<Appointment> professionalAppointments = _dbContext.Appointments.Where(
                 x => x.ProfessionalId == professionalId).ToList();

             List<AppointmentAddingResponseModel> responseModels = professionalAppointments
                 .Select(x => new AppointmentAddingResponseModel(x.PetId, x.ProfessionalId, x.Date))
                 .ToList();

             return responseModels;
         }

        [HttpPost]
        [Route("addAppointment")]
        public void addAppointment(AppointmentAddingRequestModel request)
        {
            /*string userIdStringified = _userManager.GetUserId(User);

            int userId = int.Parse(userIdStringified);*/


            Appointment appointment = new Appointment
            {
                PetId = request.PetId,
                ProfessionalId = request.ProfessionalId,
                Date = request.AppointmentDateTime
            };

            _dbContext.Appointments.Add(appointment);
            _dbContext.SaveChanges();
        }

       /* [HttpGet]
        [Route("getProfessionalAppointment")]
        public void getProfessionalAppointment(int professionalId)
        {
            /*string userIdStringified = _userManager.GetUserId(User);

            int userId = int.Parse(userIdStringified);*/


           /* public List<AppointmentAddingResponseModel> GetProfessionalAppointments(int professionalId)
            {
                List<ProfessionalAppointment> professionalAppointments = _dbContext.ProfessionalAppointments.Where(
                    x => x.ProfessionalId == professionalId).ToList();

                List<AppointmentAddingResponseModel> responseModels = professionalAppointments
                    .Select(x => new AppointmentAddingResponseModel(x.PetId, x.ProfessionalId, x.AppointmentDateTime))
                    .ToList();

                return responseModels;
            }
        }*/

        [HttpPost]
        [Route("changeAppointment")]
        public void changeAppointment(AppointmentAddingRequestModel request)
        {
            /*string userIdStringified = _userManager.GetUserId(User);

            int userId = int.Parse(userIdStringified);*/


            Appointment appointment = _dbContext.Appointments.Find(
                request.ProfessionalId, request.PetId, request.AppointmentDateTime);

            Appointment newAppointment = new Appointment
            {
                PetId = request.PetId,
                ProfessionalId = request.ProfessionalId,
                Date = request.NewAppointmentDateTime
            };

            if (appointment == null)
            {
                throw new ArgumentException("Something went wrong. Try again");
            }
            _dbContext.Entry(appointment).CurrentValues.SetValues(newAppointment);
            _dbContext.SaveChanges();
        }

        [HttpDelete]
        [Route("DeleteAppointment")]
        public void DeleteAppointment(AppointmentAddingRequestModel request)
        {
            Appointment appointment = _dbContext.Appointments.Find(request.ProfessionalId, request.PetId, request.AppointmentDateTime);

            if (appointment == null)
            {
                throw new ArgumentException("There was an error. Try again");
            }

            _dbContext.Appointments.Remove(appointment);
            //_dbContext.PetAssignments.Update(chosenPet);
            _dbContext.SaveChanges();
        }

        // Profile editing //

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

        // MedNotes //

        [HttpPost]
        [Route("addMedNote")]
        public void addMedNote(MedNoteRequest request)
        {
            //string userIdStringified = _userManager.GetUserId(User);

            //int userId = int.Parse(userIdStringified);

        PetMedCardNote medCardNote = new PetMedCardNote
            {
                PetId = request.PetId,
                Illness = request.Illness,
                Treatment = request.Treatment,
                Comment = request.Comment,
                NoteDate = request.NoteDate
            };

            _dbContext.PetMedCardNotes.Add(medCardNote);
            _dbContext.SaveChanges();
        }

        [HttpGet]
        [Route("getMedNote")]
        public List<MedNoteResponseModel> getPetMedNotes(int petId)
        {
            List<PetMedCardNote> medNotes = _dbContext.PetMedCardNotes.Where(x => x.PetId == petId).ToList();

            List<MedNoteResponseModel> responseModels = medNotes
                .Select(x => new MedNoteResponseModel(x.PetMedCardNoteId, x.PetId, 
                x.Illness, x.Treatment, x.Comment, x.NoteDate))
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

        // only for admin
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
