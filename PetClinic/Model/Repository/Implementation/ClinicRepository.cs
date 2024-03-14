using Microsoft.EntityFrameworkCore;
using PetClinic.Model.Repository.Interfaces;

namespace PetClinic.Model.Repository.Implementation
{
    public class ClinicRepository : IClinicRepository
    {
        private readonly PetClinicDbContext _context;

        public ClinicRepository(PetClinicDbContext context)
        {
            _context = context;
        }

       
        public async Task<Clinic> GetClinicByIdAsync(int id)
        {
            return await _context.Clinics.FindAsync(id);
        }
        public async Task<Clinic> GetClinicWithChildrenIdAsync(int id)
        {
            return await _context.Clinics.Include(e => e.OpeningHours).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task InsertClinicAsync(Clinic clinic)
        {
            _context.Clinics.Add(clinic);
            await SaveAsync();
        }

        public async Task UpdateClinicAsync(Clinic clinic)
        {
            _context.Clinics.Update(clinic);
            await SaveAsync();
        }

       

        public async Task<bool> ClinicExistsAsync(int clinicId)
        {
            return await _context.Clinics.AnyAsync(c => c.Id == clinicId);
        }
        public async Task<bool> ClinicNameExistsAsync(string clinicName)
        {
            return await _context.Clinics.AnyAsync(c => c.Name == clinicName);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
