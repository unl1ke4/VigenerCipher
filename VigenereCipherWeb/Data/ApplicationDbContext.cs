using Microsoft.EntityFrameworkCore;

namespace VigenereCipherWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; } = null!;
        public DbSet<CipherJob> CipherJobs { get; set; } = null!;
        public DbSet<CipherMethod> CipherMethods { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed даних для методів шифрування (довідник)
            modelBuilder.Entity<CipherMethod>().HasData(
                new CipherMethod { Id = 1, Name = "Vigenere", Description = "Класичний шифр Віженера з буквеним ключем" },
                new CipherMethod { Id = 2, Name = "Caesar", Description = "Шифр Цезаря зі зсувом алфавіту" },
                new CipherMethod { Id = 3, Name = "Playfair", Description = "Біграмний шифр Плейфера" }
            );

            // Seed даних для користувачів (довідник)
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser { Id = 1, Auth0UserId = "auth0|demo1", Username = "demo_user", Email = "demo@example.com" },
                new AppUser { Id = 2, Auth0UserId = "auth0|demo2", Username = "test_user", Email = "test@example.com" }
            );

            // Seed даних для завдань шифрування (центральна таблиця)
            modelBuilder.Entity<CipherJob>().HasData(
                new CipherJob 
                { 
                    Id = 1, 
                    InputText = "HELLO", 
                    Key = "KEY", 
                    Operation = "encrypt", 
                    ResultText = "RIJVS",
                    AppUserId = 1,
                    CipherMethodId = 1,
                    CreatedAt = new DateTime(2025, 11, 15, 10, 30, 0)
                },
                new CipherJob 
                { 
                    Id = 2, 
                    InputText = "WORLD", 
                    Key = "KEY", 
                    Operation = "encrypt", 
                    ResultText = "CPSME",
                    AppUserId = 1,
                    CipherMethodId = 1,
                    CreatedAt = new DateTime(2025, 11, 16, 14, 20, 0)
                },
                new CipherJob 
                { 
                    Id = 3, 
                    InputText = "SECRET", 
                    Key = "ABC", 
                    Operation = "decrypt", 
                    ResultText = "TFDSFU",
                    AppUserId = 2,
                    CipherMethodId = 2,
                    CreatedAt = new DateTime(2025, 11, 17, 9, 15, 0)
                }
            );
        }
    }

    public class AppUser
    {
        public int Id { get; set; }
        public string Auth0UserId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Навігаційна властивість
        public ICollection<CipherJob> CipherJobs { get; set; } = new List<CipherJob>();
    }

    public class CipherJob
    {
        public int Id { get; set; }
        public string InputText { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public string Operation { get; set; } = "encrypt";
        public string ResultText { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Keys
        public int? AppUserId { get; set; }
        public int CipherMethodId { get; set; }

        // Навігаційні властивості
        public AppUser? AppUser { get; set; }
        public CipherMethod CipherMethod { get; set; } = null!;
    }

    public class CipherMethod
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Навігаційна властивість
        public ICollection<CipherJob> CipherJobs { get; set; } = new List<CipherJob>();
    }
}
