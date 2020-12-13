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
using Microsoft.AspNetCore.Authorization;

namespace Petthy.Controllers.Api
{
    [Authorize]
    [Route("api/commonProfessional")]
    [ApiController]
    public class CommonProfessionalController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public CommonProfessionalController(ApplicationDbContext dbContext,
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


        // Schedule //

        [HttpPost]
        [Route("addVacantDatesToSchedule")]
        public void addVacantDatesToSchedule(AddingVacantDatesToScheduleRequestModel request)
        {
            string userIdStringified = _userManager.GetUserId(User);
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
            string userIdStringified = _userManager.GetUserId(User);
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
            string userIdStringified = _userManager.GetUserId(User);
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
            string userIdStringified = _userManager.GetUserId(User);
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
            string userIdStringified = _userManager.GetUserId(User);
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
            string userIdStringified = _userManager.GetUserId(User);
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
            string userIdStringified = _userManager.GetUserId(User);
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
            string userIdStringified = _userManager.GetUserId(User);
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
            Professional chosenProfessionalAccount = _dbContext.Professionals.SingleOrDefault(
                x => x.UserId == userIdStringified);

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
            string userIdStringified = _userManager.GetUserId(User);
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
