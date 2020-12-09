using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Petthy.Data;
using Petthy.Models;
using Petthy.Models.Pet;
using Petthy.Models.Professional;
using Petthy.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petthy.Controllers.Api
{
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public ClientController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("getAllClients")]
        public List<Client> getAllClients()
        {
            List<Client> clients = _dbContext.Clients.ToList();

            return clients;
        }

        [HttpGet]
        [Route("getMyClients")]
        public List<ClientResponseModel> getMyClients()
        {
            string userIdStringified = _userManager.GetUserId(User);
            Professional currentUser = _dbContext.Professionals.SingleOrDefault(x => x.UserId == userIdStringified);

            List<PetAssignmentResponseModel> myPetAssignments = getVeterineriansAssignments(currentUser.ProfessionalId).ToList();

            List<Client> clients = new List<Client>().ToList();

            for (int i = 0; i < myPetAssignments.Count; i++)
            {
                Pet newPet = _dbContext.Pets.
                    SingleOrDefault(x => x.PetId == myPetAssignments[i].PetId);
                Client newClient = _dbContext.Clients.SingleOrDefault(x => x.ClientId == newPet.ClientId);
                if (clients.Where(x => x.ClientId == newClient.ClientId) == null)
                {
                    clients.Add(newClient);
                }

            }
            List<ClientResponseModel> responseModels = clients
                .Select(x => new ClientResponseModel(
                    x.ClientId, x.FirstName, x.LastName, x.PhoneNumber, x.DateOfBirth, x.Address))
                .ToList();

            return responseModels;
        }

        [HttpGet]
        [Route("getSingleClient")]
        public Client getSingleClient(int clientId)
        {
            Client responseModel = _dbContext.Clients.Single(x => x.ClientId == clientId);

            return responseModel;
        }

        public List<PetAssignmentResponseModel> getVeterineriansAssignments(int myId)
        {

            List<PetAssignment> petAssignments = _dbContext.PetAssignments.Where(x => x.ProfessionalId == myId).ToList();

            List<PetAssignmentResponseModel> responseModels = petAssignments
                .Select(x => new PetAssignmentResponseModel(x.PetId, x.ProfessionalId))
                .ToList();

            return responseModels;
        }

    }
}
