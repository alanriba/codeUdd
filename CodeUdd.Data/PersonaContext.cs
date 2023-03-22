using CodeUdd.Data.Model;

using Microsoft.EntityFrameworkCore;

namespace CodeUdd.Data
{
    public class PersonaContext: DbContext
    {
        public static string SettingName = "ConnectionStrings";
        public string? DefaultConnection { get; set; }

        public DbSet<Persona>? Persona { get; set; }

        public PersonaContext() {
            DefaultConnection = DefaultConnection ?? @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=udd;Integrated Security=True";
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DefaultConnection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
