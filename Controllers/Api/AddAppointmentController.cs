using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Petthy.Data;
using Petthy.Models.Professional;
using Petthy.Models.Pet;
using Petthy.Models.Request;
using Petthy.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petthy.Models;
using Microsoft.AspNetCore.Identity;

namespace Petthy.Controllers.Api
{
    [Route("api/doctor")]
    [ApiController]
    public class AddAppointmentController : ControllerBase
    {
        private readonly UserManager<Professional> _userManager;
        private readonly UserManager<Pet> _petManager;
        private readonly ApplicationDbContext _dbContext;

        public AddAppointmentController(ApplicationDbContext dbContext, UserManager<Professional> userManager, UserManager<Pet> petManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _petManager = petManager;
        }

        [HttpPost]
        [Route("addAppointmentToSchedule")]

        public void AddAppointmentToSchedule(AppointmentAddingRequestModel request)
        {
            string userIdStringified = _userManager.GetUserId(User);

            int userId = int.Parse(userIdStringified);

            string petIdStringified = _petManager.GetUserId(User);

            int petId = int.Parse(petIdStringified);

            string dateTime = _petManager.GetUserId(User);

            DateTime AppointmentDateTime = DateTime.Parse(dateTime);

            Appointment app = _dbContext.Appointments.Find(request.ProfessionalId, request.PetId, request.AppointmentDateTime);

            if (app != null)
            {
                throw new ArgumentException("This date is already taken. Try another one");
            }

            ProfessionalAppointment appointment = new ProfessionalAppointment
            {
                PetId = request.PetId,
                ProfessionalId = request.ProfessionalId,
                AppointmentDateTime = request.AppointmentDateTime
            };

            _dbContext.ProfessionalAppointments.Add(appointment);
        }

        [HttpGet]
        [Route("getProfessionalAppointments")]
        public List<AppointmentAddingResponseModel> GetProfessionalAppointments(int professionalId)
        {
            List<ProfessionalAppointment> professionalAppointments = _dbContext.ProfessionalAppointments.Where(
                x => x.ProfessionalId == professionalId).ToList();

            List<AppointmentAddingResponseModel> responseModels = professionalAppointments
                .Select(x => new AppointmentAddingResponseModel(x.PetId, x.ProfessionalId, x.AppointmentDateTime))
                .ToList();

            return responseModels;
        }

        [HttpPost]
        [Route("ChangeProfessionalAppointments")]
        public void ChangeProfessionalAppointments(AppointmentAddingRequestModel request)
        {
            ProfessionalAppointment chosenProfessionalAppointment = _dbContext.ProfessionalAppointments.Find(request.ProfessionalId, 
                request.PetId, request.AppointmentDateTime);

            if (chosenProfessionalAppointment == null)
            {
                throw new ArgumentException("Such date was not taken. You cannot change it.");
            }

            chosenProfessionalAppointment.AppointmentDateTime = request.NewAppointmentDateTime;
            _dbContext.ProfessionalAppointments.Update(chosenProfessionalAppointment);
            _dbContext.SaveChanges();
        }


        [HttpDelete]
        [Route("DeleteProfessionalAppointments")]
        public void DeleteProfessionalAppointments(AppointmentAddingRequestModel request)
        {
            ProfessionalAppointment chosenProfessionalAppointment = _dbContext.ProfessionalAppointments.Find(request.ProfessionalId, request.PetId, request.AppointmentDateTime);

            if (chosenProfessionalAppointment == null)
            {
                throw new ArgumentException("Such date was not taken. You cannot delete an appointment.");
            }

            _dbContext.ProfessionalAppointments.Remove(chosenProfessionalAppointment);
            _dbContext.ProfessionalAppointments.Update(chosenProfessionalAppointment);
            _dbContext.SaveChanges();
        }
    }
}
