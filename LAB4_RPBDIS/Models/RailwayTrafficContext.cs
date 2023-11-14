using Microsoft.EntityFrameworkCore;

namespace LAB4_RPBDIS.Models
{
    public class RailwayTrafficContext : DbContext
    {
        
        public RailwayTrafficContext(DbContextOptions<RailwayTrafficContext> options) : base(options)
        {

        }

        //таблицы-справочники
        public DbSet<TrainType> TrainTypes { get; set; }

        //обычные таблицы
        public DbSet<Stop> Stops { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> Employees { get; set; }

        // связующие таблицы для отношений many-to-many
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<TrainStaff> TrainStaffs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // настрйока связи many-to-many: Train и Stop через Schedule
            modelBuilder
            .Entity<Train>()
            .HasMany(t => t.Stops)
            .WithMany(s => s.Trains)
            .UsingEntity<Schedule>(
               j => j
                .HasOne(pt => pt.Stop)
                .WithMany(t => t.Schedules)
                .HasForeignKey(pt => pt.StopId),
            j => j
                .HasOne(pt => pt.Train)
                .WithMany(p => p.Schedules)
                .HasForeignKey(pt => pt.TrainId),
            j =>
            {
                j.ToTable("Schedules");
            });

            // настрйока связи many-to-many: Train и Employee через TrainStaff
            modelBuilder
            .Entity<Train>()
            .HasMany(t => t.Employees)
            .WithMany(e => e.Trains)
            .UsingEntity<TrainStaff>(
               j => j
                .HasOne(pt => pt.Employee)
                .WithMany(t => t.TrainStaffs)
                .HasForeignKey(pt => pt.EmployeeId),
            j => j
                .HasOne(pt => pt.Train)
                .WithMany(p => p.TrainStaffs)
                .HasForeignKey(pt => pt.TrainId),
            j =>
            {
                j.ToTable("TrainStaffs");
            });
        }
    }
}
