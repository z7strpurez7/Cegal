namespace PetClinic.Model.Repository.Interfaces
{
    public interface IClinicRepository : IDisposable
    {
    
        Task<Clinic> GetClinicByIdAsync(int id);
        Task<Clinic> GetClinicWithChildrenIdAsync(int id);
        Task InsertClinicAsync(Clinic clinic);
        Task UpdateClinicAsync(Clinic clinic);
        Task<bool> ClinicExistsAsync(int clinicId);
        Task<bool> ClinicNameExistsAsync(string clinicName);
        Task SaveAsync();
    }
}
