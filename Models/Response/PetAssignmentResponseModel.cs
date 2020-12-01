namespace Petthy.Models.Response
{
    public class PetAssignmentResponseModel
    {
        public PetAssignmentResponseModel(int petId, int professionalId)
        {
            PetId = petId;
            ProfessionalId = professionalId;
        }

        public int PetId { get; }

        public int ProfessionalId { get; }
    }
}
