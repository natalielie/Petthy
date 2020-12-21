using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using Petthy.Data;
using Petthy.Models;
using Petthy.Models.Pet;
using Petthy.Models.Professional;
using Petthy.Models.Request;
using Petthy.Models.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
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

        [HttpPost]
        [Route("assignPetToDoctor")]
        public void AssignPetToDoctor(PetAssignmentRequestModel request)
        {
            Professional currentUser = _dbContext.Professionals.SingleOrDefault(
                x => x.ProfessionalId == request.ProfessionalId);

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
        [Route("getAllAssignments")]
        public List<PetAndProfessional> getAllAssignments()
        {
            List<PetAssignment> petAssignments = _dbContext.PetAssignments.ToList();


            List<Pet> pets = _dbContext.Pets.ToList();
            List<Professional> professionals = _dbContext.Professionals.ToList();

            List<PetAndProfessional> petsAndProfessionals = new List<PetAndProfessional>();
            foreach (var assignment in petAssignments)
            {
                Pet pet = _dbContext.Pets.SingleOrDefault(x => x.PetId == assignment.PetId);
                Professional professional = _dbContext.Professionals.SingleOrDefault(
                    x => x.ProfessionalId == assignment.ProfessionalId);
                petsAndProfessionals.Add(new PetAndProfessional
                {
                    Pet = pet,
                    Professional = professional
                });

            }

            return petsAndProfessionals;
        }

        [HttpGet]
        [Route("getAllAppointments")]
        public List<PetAndProfessional> getAllAppointments()
        {
            List<Appointment> professionalAppointments = _dbContext.Appointments.ToList();

            List<AppointmentAddingResponseModel> responseModels = professionalAppointments
                .Select(x => new AppointmentAddingResponseModel(x.PetId, x.ProfessionalId, x.Date))
                .ToList();

            List<Pet> pets = _dbContext.Pets.ToList();
            List<Professional> professionals = _dbContext.Professionals.ToList();

            List<PetAndProfessional> petsAndProfessionals = new List<PetAndProfessional>();
            foreach (var appointment in professionalAppointments)
            {
                DateTime currentTime = DateTime.Now;
                if (appointment.Date >= currentTime)
                {
                    Pet pet = _dbContext.Pets.SingleOrDefault(x => x.PetId == appointment.PetId);
                    Professional professional = _dbContext.Professionals.SingleOrDefault(
                        x => x.ProfessionalId == appointment.ProfessionalId);
                    petsAndProfessionals.Add(new PetAndProfessional
                    {
                        AppointmentId = appointment.AppointmentId,
                        Pet = pet,
                        Professional = professional,
                        DateTimeBegin = appointment.Date
                    });
                }
            }

            return petsAndProfessionals;
        }




        // database
        [HttpPost]
        [Route("backupDatabase")]
        public async Task<string> BackupDatabase()
        {
            // read connectionstring from config file
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=aspnet-PetthyDb;Trusted_Connection=True;MultipleActiveResultSets=true";

            // read backup folder from config file ("C:/temp/")
            var backupFolder = "C:/Program Files/Microsoft SQL Server/MSSQL12.SQLEXPRESS/MSSQL/Backup/";

            var sqlConStrBuilder = new SqlConnectionStringBuilder(connectionString);

            // set backupfilename (you will get something like: "C:/temp/MyDatabase-2013-12-07.bak")
            var backupFileName = String.Format("{0}{1}-{2}.bak",
                backupFolder, sqlConStrBuilder.InitialCatalog,
                DateTime.Now.ToString("yyyy-MM-dd"));

            using (var connection = new SqlConnection(sqlConStrBuilder.ConnectionString))
            {
                var query = String.Format("BACKUP DATABASE {0} TO DISK='{1}'",
                    sqlConStrBuilder.InitialCatalog, backupFileName);

                using (var command = new SqlCommand(query, connection))
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    return "Backup done";
                }
            }
        }


        [HttpPost]
        [Route("restoreDatabase")]
        private void RestoreDatabase(string localDatabasePath, string fileListDataName, string fileListLogName)
        {
            string localDownloadFilePath = "C:/Program Files/Microsoft SQL Server/MSSQL12.SQLEXPRESS/MSSQL/Backup/";
    Console.WriteLine(string.Format("Restoring database {0}...", localDatabasePath));
            string fileListDataPath = Directory.GetParent(localDownloadFilePath).Parent.FullName + @"\DATA\" + fileListDataName + ".mdf";
            string fileListLogPath = Directory.GetParent(localDownloadFilePath).Parent.FullName + @"\DATA\" + fileListLogName + ".ldf";

            string sql = @"RESTORE DATABASE @dbName FROM DISK = @path WITH RECOVERY,
        MOVE @fileListDataName TO @fileListDataPath,
        MOVE @fileListLogName TO @fileListLogPath";

            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=aspnet-PetthyDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.CommandTimeout = 7200;
                    command.Parameters.AddWithValue("@dbName", fileListDataName);
                    command.Parameters.AddWithValue("@path", localDatabasePath);
                    command.Parameters.AddWithValue("@fileListDataName", fileListDataName);
                    command.Parameters.AddWithValue("@fileListDataPath", fileListDataPath);
                    command.Parameters.AddWithValue("@fileListLogName", fileListLogName);
                    command.Parameters.AddWithValue("@fileListLogPath", fileListLogPath);

                    command.ExecuteNonQuery();
                }
            }
            Console.WriteLine(string.Format("Database restoration complete for {0}.", localDatabasePath));
        }
    }
}
