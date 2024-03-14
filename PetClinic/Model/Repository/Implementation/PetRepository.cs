using Microsoft.EntityFrameworkCore;
using PetClinic.Model.Repository.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace PetClinic.Model.Repository.Implementation
{
    public class PetRepository : IPetRepository
    {
        private readonly PetClinicDbContext _context;

        public PetRepository(PetClinicDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsPetRegisteredAsync(string name, int ownerId)
        {
            return await _context.Pets.AnyAsync(p => p.Name == name && p.ClientId == ownerId);
        }
        public async Task<bool> IsDuplicatePetNameAsync(string newName, int ownerId, int petId)
        {
            return await _context.Pets.AnyAsync(p => p.Name == newName && p.ClientId == ownerId && p.Id != petId);
        }

        public async void InsertPet(Pet pet)
        {
            _context.Pets.Add(pet);
        }

        public async Task<Pet> GetPetByIdAsync(int id)
        {
            return await _context.Pets.FindAsync(id);
        }

        public async Task<Pet> GetPetWithAppointmentsAsync(int id)
        {
            return await _context.Pets
                .Include(p => p.Appointments)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<List<Pet>> GetPetsWithChildrenFromClientIdAsync(int id)
        {
            return await _context.Pets.Where(e => e.ClientId == id).Include(a => a.Appointments).ToListAsync();
        }
        public void UpdatePet(Pet pet)
        {
            _context.Pets.Update(pet);
        }

        public void DeletePet(Pet pet)
        {
            _context.Pets.Remove(pet);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
