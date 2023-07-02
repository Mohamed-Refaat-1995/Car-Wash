using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarService.Models
{
    public class CarServiceEntities : IdentityDbContext<ApplicationUser>
    {

        public CarServiceEntities() : base()
        {

        }
        //using during injection
        public CarServiceEntities(DbContextOptions option) : base(option)
        {

        }

        public DbSet<Times> Times { get; set; }
        public DbSet<Technican> Technicans { get; set; }
        public DbSet<CallCenter> CallCenter { get; set; }
        public DbSet<CallCenterData> CallCenterData { get; set; }
        public DbSet<CoordinatorData> CoordinatorData { get; set; }
        public DbSet<TechnicanData> TechnicanData { get; set; }
        public DbSet<PreRequst> PreRequst { get; set; }
        public DbSet<Coordinator> Coordinator { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<TechnicalOrderDay> TechnicalOrderDay { get; set; }
        public DbSet<TechnicalOrderDayTime> TechnicalOrderDayTime { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MOHAMED\\SQL19;Initial Catalog=FinalProject;Integrated Security=True;Encrypt=False");
            base.OnConfiguring(optionsBuilder);
        }

        #region old seeding
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<City>().HasData(new[]
        //    {
        //        new City
        //        {
        //            Id = 1,
        //            Name = "Assiut",

        //        },
        //        new City
        //        {
        //            Id = 2,
        //            Name = "Sohag",

        //        },
        //        new City
        //        {
        //            Id = 3,
        //            Name = "Tema",

        //        },
        //        new City
        //        {
        //            Id = 4,
        //            Name = "Alexanderia",

        //        },
        //    });
        //    modelBuilder.Entity<IdentityRole>().HasData(new[] {
        //    new IdentityRole
        //    {
        //        Id=Guid.NewGuid().ToString(),
        //        Name="فني"
        //    },


        //    }); ;
        //    modelBuilder.Entity<Services>().HasData(new[] {
        //    new Services
        //    {
        //        Id=1,
        //        Name="s1"
        //    },
        //    new Services
        //    {
        //        Id=2,
        //        Name="s2"
        //    },


        //    }); ;

        //} 
        #endregion




    }
}
