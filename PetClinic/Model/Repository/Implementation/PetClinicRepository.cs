using Microsoft.EntityFrameworkCore;
using PetClinic.Model.Repository.Interfaces;

namespace PetClinic.Model.Repository.Implementation
{
    public class PetClinicRepository : IPetClinicRepository
    {
        private PetClinicDbContext _context;

        public PetClinicRepository(PetClinicDbContext context)
        {
            _context = context;
        }
        public async Task<bool> IsEmailRegisteredAsync(string email)
        {
            return await _context.Clients.AnyAsync(c => c.Email == email);
        }

        public async void InsertClient(Client client)
        {
            _context.Clients.Add(client);
        }

        public void UpdateClient(Client client)
        {
            _context.Clients.Update(client);
        }
        public void DeleteClient(Client client)
        {
            _context.Clients.Remove(client);
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<Client> GetClientWithChildrenAsync(int id)
        {
            var client = await _context.Clients
           .Include(c => c.Pets)
           .ThenInclude(p => p.Appointments)
           .FirstOrDefaultAsync(c => c.Id == id);

            return client;
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
