using Microsoft.EntityFrameworkCore;

namespace VigenereCipherWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<CipherJob> CipherJobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed дані для прикладів шифрувальних задач
            modelBuilder.Entity<CipherJob>().HasData(
                new CipherJob { Id = 1, InputText = "HELLO", Key = "KEY", Operation = "encrypt", ResultText = "RIJVS" },
                new CipherJob { Id = 2, InputText = "WORLD", Key = "KEY", Operation = "encrypt", ResultText = "CPSME" }
            );
        }
    }

    public class AppUser
    {
        public int Id { get; set; }
        public string Auth0UserId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class CipherJob
    {
        public int Id { get; set; }
        public string InputText { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public string Operation { get; set; } = "encrypt";
        public string ResultText { get; set; } = string.Empty;
        public int? AppUserId { get; set; }       // для прив'язки до користувача
        public AppUser? AppUser { get; set; }
    }
}
