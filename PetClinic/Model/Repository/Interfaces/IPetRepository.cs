namespace PetClinic.Model.Repository.Interfaces
{
    public interface IPetRepository : IDisposable
    {
        void InsertPet(Pet pet);
        Task<int> SaveAsync();
        Task<Pet> GetPetByIdAsync(int id);
        Task<Pet> GetPetWithAppointmentsAsync(int id);
        Task<bool> IsPetRegisteredAsync(string name, int ownerId);
        Task<bool> IsDuplicatePetNameAsync(string newName, int clientId, int petId);
        Task<List<Pet>> GetPetsWithChildrenFromClientIdAsync(int id);
        void UpdatePet(Pet pet);
        void DeletePet(Pet pet);
    
    }
}
