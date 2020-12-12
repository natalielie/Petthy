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
using Microsoft.Net.Http.Headers;

namespace Petthy.Controllers.Api
{
    [Route("api/doctor")]
    [ApiController]
    public class VeterinerianController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _dbContext;
        private string id = "427b8c03-d512-4172-82e3-5ffe5f28f834" +
            "";


        public VeterinerianController(ApplicationDbContext dbContext,
            UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return Content(User.Identity.Name);
        }
        

        // MedNotes //

        [HttpPost]
        [Route("addMedNote")]
        public void addMedNote(MedNoteRequestModel request)
        {
            if (IsUserAssignedToPet(request.PetId))
            {
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
            else
            {
                throw new ArgumentException("You weren't assigned to this pet to write a med card note for it");
            }
        }

        [HttpGet]
        [Route("getMedNote")]
        public List<MedNoteResponseModel> getPetMedNotes(int petId)
        {
            if (IsUserAssignedToPet(petId))
            {
                List<PetMedCardNote> medNotes = _dbContext.PetMedCardNotes.Where(x => x.PetId == petId).ToList();

                List<MedNoteResponseModel> responseModels = medNotes
                    .Select(x => new MedNoteResponseModel(x.PetMedCardNoteId, x.PetId,
                    x.Illness, x.Treatment, x.Comment, x.NoteDate))
                    .ToList();
                return responseModels;
            }
            else
            {
                throw new ArgumentException("You weren't assigned to this pet to get its med card information");
            }
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

        // check whether this user is assigned to chosen pet
        public bool IsUserAssignedToPet(int petId)
        {
            string userIdStringified = id;
            Professional currentUser = _dbContext.Professionals.SingleOrDefault(x => x.UserId == userIdStringified);

            List<PetAssignmentResponseModel> petAssignments = GetPetAssignments(petId);
            if (petAssignments.Find(x => x.ProfessionalId == currentUser.ProfessionalId) != null)
            {
                return true;
            }
            return false;
        }



        // Schedule //

        [HttpPost]
        [Route("addVacantDatesToSchedule")]
        public void addVacantDatesToSchedule(AddingVacantDatesToScheduleRequestModel request)
        {
            //string userIdStringified = _userManager.GetUserId(User);
            string userIdStringified = id;
            Professional currentUser = _dbContext.Professionals.SingleOrDefault(x => x.UserId == userIdStringified);

            int count = request.DateTimeEnd.Hour - request.DateTimeBegin.Hour;
            int addHour = 0;
            while (count != 0)
            {
                DateTime dateTimeBegin = request.DateTimeBegin.AddHours(addHour);
                DateTime dateTimeEnd = request.DateTimeBegin.AddHours(addHour + 1);
                ProfessionalSchedule schedule = new ProfessionalSchedule
                {
                    ProfessionalId = currentUser.ProfessionalId,
                    Weekday = request.Weekday,
                    DateTimeBegin = dateTimeBegin,
                    DateTimeEnd = dateTimeEnd
                };

                _dbContext.ProfessionalSchedules.Add(schedule);
                _dbContext.SaveChanges();

                addHour++;
                count--;
            }

        }

        [HttpGet]
        [Route("getSchedule")]
        public List<ProfessionalScheduleResponseModel> getVacantDatesOfSchedule()
        {
            //string userIdStringified = _userManager.GetUserId(User);
            string userIdStringified = id;
            Professional currentUser = _dbContext.Professionals.SingleOrDefault(x => x.UserId == userIdStringified);

            List<ProfessionalSchedule> schedules = _dbContext.ProfessionalSchedules.Where(
                x => x.ProfessionalId == currentUser.ProfessionalId).ToList();

            List<ProfessionalScheduleResponseModel> responseModels = schedules
                .Select(x => new ProfessionalScheduleResponseModel(
                    x.ProfessionalId, x.Weekday, x.DateTimeBegin, x.DateTimeEnd))
                .ToList();

            return responseModels;
        }

        // assignment //

        [HttpPost]
        [Route("assignPetToDoctor")]
        public void AssignPetToDoctor(PetAssignmentRequestModel request)
        {
            // string userIdStringified = _userManager.GetUserId(User);
            string userIdStringified = id;
            Professional currentUser = _dbContext.Professionals.SingleOrDefault(x => x.UserId == userIdStringified);

            Pet pet = _dbContext.Pets.Find(request.PetId);

            if (pet == null)
            {
                throw new ArgumentException("Something wrong happened. Please, try again.");
            }

            PetAssignment assignment = new PetAssignment
            {
                PetId = request.PetId,
                ProfessionalId = currentUser.ProfessionalId
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
            string userIdStringified = id;
            Professional currentUser = _dbContext.Professionals.SingleOrDefault(x => x.UserId == userIdStringified);


            PetAssignment chosenPet = _dbContext.PetAssignments.SingleOrDefault(
                x => x.ProfessionalId == currentUser.ProfessionalId && x.PetId == request.PetId);

            if (chosenPet == null)
            {
                throw new ArgumentException("Something wrong happened. Please, try again.");
            }

            _dbContext.PetAssignments.Remove(chosenPet);
            _dbContext.SaveChanges();
        }

        // Appointments //

        [HttpGet]
        [Route("getProfessionalSchedule")]
        public List<AppointmentAddingResponseModel> GetProfessionalAppointments()
        {
            string userIdStringified = id;
            Professional currentUser = _dbContext.Professionals.SingleOrDefault(x => x.UserId == userIdStringified);

            List<Appointment> professionalAppointments = _dbContext.Appointments.Where(
                x => x.ProfessionalId == currentUser.ProfessionalId).ToList();

            List<AppointmentAddingResponseModel> responseModels = professionalAppointments
                .Select(x => new AppointmentAddingResponseModel(x.PetId, x.ProfessionalId, x.Date))
                .ToList();

            return responseModels;
        }

        [HttpPost]
        [Route("addAppointment")]
        public void addAppointment(AppointmentAddingRequestModel request)
        {
            string userIdStringified = id;
            Professional currentUser = _dbContext.Professionals.SingleOrDefault(x => x.UserId == userIdStringified);

            List<ProfessionalScheduleResponseModel> vacantSchedule = getVacantDatesOfSchedule();


            if (vacantSchedule.Any(x => x.DateTimeBegin == request.AppointmentDateTime))
            {
                Appointment appointment = new Appointment
                {
                    PetId = request.PetId,
                    ProfessionalId = currentUser.ProfessionalId,
                    Date = request.AppointmentDateTime
                };

                ProfessionalSchedule vacantDate = _dbContext.ProfessionalSchedules.SingleOrDefault(x =>
                            x.ProfessionalId == currentUser.ProfessionalId
                            && x.DateTimeBegin == request.AppointmentDateTime);


                _dbContext.Appointments.Add(appointment);
                _dbContext.ProfessionalSchedules.Remove(vacantDate);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Something wrong happened. Please, try again.");
            }
        }


        [HttpPost]
        [Route("changeAppointment")]
        public void changeAppointment(AppointmentAddingRequestModel request)
        {
            //string userIdStringified = _userManager.GetUserId(User);
            string userIdStringified = id;
            Professional currentUser = _dbContext.Professionals.SingleOrDefault(x => x.UserId == userIdStringified);


            Appointment appointment = _dbContext.Appointments.SingleOrDefault(x =>
                            x.ProfessionalId == currentUser.ProfessionalId
                            && x.PetId == request.PetId
                            && x.Date == request.AppointmentDateTime);

            List<ProfessionalScheduleResponseModel> vacantSchedule = getVacantDatesOfSchedule();


            if (vacantSchedule.Any(x => x.DateTimeBegin == request.NewAppointmentDateTime))
            {
                Appointment newAppointment = new Appointment
                {
                    PetId = request.PetId,
                    ProfessionalId = currentUser.ProfessionalId,
                    Date = request.NewAppointmentDateTime
                };

                ProfessionalSchedule vacantDate = new ProfessionalSchedule
                {
                    ProfessionalId = currentUser.ProfessionalId,
                    Weekday = request.AppointmentDateTime.ToString("dddd"),
                    DateTimeBegin = request.AppointmentDateTime,
                    DateTimeEnd = request.AppointmentDateTime.AddHours(1)
                };

                ProfessionalSchedule takenDate = _dbContext.ProfessionalSchedules.SingleOrDefault(x =>
                            x.ProfessionalId == currentUser.ProfessionalId
                            && x.DateTimeBegin == request.NewAppointmentDateTime);

                _dbContext.Appointments.Remove(appointment);
                _dbContext.Appointments.Add(newAppointment);
                _dbContext.ProfessionalSchedules.Remove(takenDate);
                _dbContext.ProfessionalSchedules.Add(vacantDate);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Something wrong happened. Please, try again.");
            }
        }

        [HttpDelete]
        [Route("DeleteAppointment")]
        public void DeleteAppointment(AppointmentAddingRequestModel request)
        {
            string userIdStringified = id;
            Professional currentUser = _dbContext.Professionals.SingleOrDefault(x => x.UserId == userIdStringified);

            Appointment appointment = _dbContext.Appointments.SingleOrDefault(x =>
                            x.ProfessionalId == currentUser.ProfessionalId
                            && x.PetId == request.PetId
                            && x.Date == request.AppointmentDateTime);

            ProfessionalSchedule vacantDate = new ProfessionalSchedule
            {
                ProfessionalId = currentUser.ProfessionalId,
                Weekday = request.AppointmentDateTime.ToString("dddd"),
                DateTimeBegin = request.AppointmentDateTime,
                DateTimeEnd = request.AppointmentDateTime.AddHours(1)
            };

            if (appointment == null)
            {
                throw new ArgumentException("Something wrong happened. Please, try again.");
            }
            _dbContext.Appointments.Remove(appointment);
            _dbContext.ProfessionalSchedules.Add(vacantDate);
            _dbContext.SaveChanges();
        }

        // Profile editing //

        [HttpPost]
        [Route("ChangeProfessionalAccount")]
        public void ChangeProfessionalAccount(ProfileEditingRequestModel request)
        {
            string userIdStringified = _userManager.GetUserId(User);
            Professional chosenProfessionalAccount = _dbContext.Professionals.SingleOrDefault(x => x.UserId == userIdStringified);

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

        [HttpDelete]
        [Route("DeleteProfessionalAccount")]
        public void DeleteProfessionalAccount()
        {
            string userIdStringified = id;
            Professional currentUser = _dbContext.Professionals.SingleOrDefault(x => x.UserId == userIdStringified);

            Professional chosenProfessional = _dbContext.Professionals.Find(currentUser.ProfessionalId);

            if (chosenProfessional == null)
            {
                throw new ArgumentException("Something wrong happened. Please, try again.");
            }

            _dbContext.Professionals.Remove(chosenProfessional);
            _dbContext.SaveChanges();
        }
    }
}
    
