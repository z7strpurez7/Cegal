using Microsoft.EntityFrameworkCore;

namespace PetClinic.Model.Repository
{
    public class PetClinicDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Vet> Vets { get; set; }

        public PetClinicDbContext(DbContextOptions<PetClinicDbContext> options) : base(options) {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Clients");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.BirthDate).IsRequired();
                entity.Property(e => e.StreetAddress).IsRequired().HasMaxLength(200);
                entity.Property(e => e.ZipCode).IsRequired().HasMaxLength(4);
                entity.Property(e => e.City).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Sex).IsRequired().HasMaxLength(1);
                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(8);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);

                entity.HasMany(e => e.Pets)
                      .WithOne(p => p.Client)
                      .HasForeignKey(p => p.ClientId)
                      .OnDelete(DeleteBehavior.Cascade);
               
            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.ToTable("Pets");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Type).IsRequired();
                entity.Property(e => e.BirthYear).IsRequired().HasMaxLength(4);
                entity.Property(e => e.Breed).IsRequired();
                entity.Property(e => e.MedicalHistory).HasMaxLength(5000);

                entity.HasMany(p => p.Appointments)
                      .WithOne(a => a.Pet)
                      .HasForeignKey(a => a.PetId);
            });

            modelBuilder.Entity<Vet>(entity =>
            {
                entity.ToTable("Veterinarians");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.StreetAdress).IsRequired().HasMaxLength(200);
                entity.Property(e => e.City).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Zip).IsRequired().HasMaxLength(4);
                entity.Property(e => e.Position).IsRequired();
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Phone).IsRequired().HasMaxLength(8);
                entity.Property(e => e.BirthDate).IsRequired();

                entity.HasOne(e => e.Clinic)
                      .WithMany(c => c.Vets)
                      .HasForeignKey(e => e.ClinicId);

                entity.HasMany(e => e.Schedules)
                      .WithOne(s => s.Vet)
                      .HasForeignKey(s => s.VetId);

                entity.HasMany(e => e.Specialities)
                      .WithOne(s => s.Vet)
                      .HasForeignKey(s => s.VetId);
            });

            modelBuilder.Entity<Speciality>(entity =>
            {
                entity.ToTable("Specialities");
                entity.HasKey(s => s.Id); 

                entity.Property(s => s.SpecialityName).IsRequired();

                entity.HasOne(s => s.Vet)
                      .WithMany(v => v.Specialities)
                      .HasForeignKey(s => s.VetId);
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedules");
                entity.HasKey(e => e.Id); 

                entity.Property(e => e.StartTime).IsRequired();
                entity.Property(e => e.EndTime).IsRequired();
                entity.Property(e => e.Type).IsRequired();

                entity.HasOne(e => e.Appointment)
                      .WithOne(a => a.Schedule)
                      .HasForeignKey<Appointment>(a => a.Id); 
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointments");
                entity.HasKey(a => a.Id); 

                entity.HasOne(a => a.Pet)
                      .WithMany(p => p.Appointments)
                      .HasForeignKey(a => a.PetId);

                entity.Property(a => a.StartTime).IsRequired();
                entity.Property(a => a.EndTime).IsRequired();
                entity.Property(a => a.Type).IsRequired();
                entity.Property(a => a.Status).IsRequired();
            });

            modelBuilder.Entity<Clinic>(entity =>
            {
                entity.ToTable("Clinics");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.StreetAdress).IsRequired().HasMaxLength(200);
                entity.Property(e => e.City).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ZipCode).IsRequired().HasMaxLength(4);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(8);
                entity.Property(e => e.MaximumNumberOfVets).IsRequired();

                entity.HasMany(c => c.OpeningHours)
                      .WithOne(o => o.Clinic)
                      .HasForeignKey(o => o.ClinicId);
            });

            modelBuilder.Entity<OpeningHour>(entity =>
            {
                entity.ToTable("OpeningHours");
                entity.HasKey(e => e.Id); 

                entity.Property(e => e.DayOfWeek).IsRequired(); 
                entity.Property(e => e.OpeningTime).IsRequired();
                entity.Property(e => e.ClosingTime).IsRequired();
            });
        }
    }
}
