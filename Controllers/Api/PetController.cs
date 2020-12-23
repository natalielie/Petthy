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
        [Route("api/pet")]
        [ApiController]
        public class PetController : ControllerBase
        {
            private readonly UserManager<IdentityUser> _userManager;
            private readonly ApplicationDbContext _dbContext;

            public PetController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
            {
                _dbContext = dbContext;
                _userManager = userManager;
            }

            [HttpGet]
            [Route("getAllPets")]
            public List<Pet> getAllPets()
            {
                List<Pet> pets = _dbContext.Pets.ToList();

                return pets;
            }

        [HttpGet]
        [Route("getAllPetsAndOwners")]
        public List<PetAndOwner> getAllPetsAndOwners()
        {
            List<Pet> pets = _dbContext.Pets.ToList();
            List<Client> clients = _dbContext.Clients.ToList();
            List<PetAndOwner> petsAndOwners = new List<PetAndOwner>();

            foreach (var pet in pets)
            {
                foreach (var client in clients)
                {
                    if(pet.ClientId == client.ClientId)
                    {
                        petsAndOwners.Add(new PetAndOwner { Pet = pet, Owner = client});
                    }
                }
            }
            return petsAndOwners;
        }
        /*  [HttpGet]
          [Route("getMyPets")]
          public List<ClientResponseModel> getMyPets()
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
          }*/

        [HttpGet]
        [Route("getSinglePetByClient")]
        public Pet getSinglePetByClient(int clientId)
        {
            Pet responseModel = _dbContext.Pets.Single(x => x.ClientId == clientId);

            return responseModel;
        }

        [HttpGet]
        [Route("getSinglePet")]
        public PetAndOwner getSinglePet()
        {
            Pet pet = _dbContext.Pets.SingleOrDefault(x => x.PetId == 1);

            Client owner = _dbContext.Clients.SingleOrDefault(x => x.ClientId == pet.ClientId);
            PetAndOwner petsAndOwners = new PetAndOwner { Pet = pet, Owner = owner };

            return petsAndOwners;
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
