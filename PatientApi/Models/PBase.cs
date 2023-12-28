using Microsoft.EntityFrameworkCore;

namespace PatientApi.Models
{
    public class PBase:DbContext
    {
        public PBase()
        {

        }
        public PBase(DbContextOptions<PBase> options) : base(options)
        {

        }
        public virtual DbSet<Patient_Login> Patient_Login { get; set; } = null!;

        public virtual DbSet<Schedule> Schedule { get; set; } = null!;
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.HasKey(e => e.Serial_No); // Assuming Serial_No is the primary key
                entity.Property(e => e.User_ID); // Assuming User_ID is the foreign key

                entity.HasOne(d => d.Patient_Login)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.User_ID)
                    .HasConstraintName("FK_Schedule_Patient_Login");
            });

           
        }


    }
}
