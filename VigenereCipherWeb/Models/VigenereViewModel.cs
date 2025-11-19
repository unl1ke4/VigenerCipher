using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VigenereCipherWeb.Models
{
    public class VigenereViewModel
    {
        [DisplayName("Enter text")]
        public string? InputText { get; set; }

        [DisplayName("Enter the key")]
        public string? Key { get; set; }

        [DisplayName("Result")]
        public string? ResultText { get; set; }

        public string Operation { get; set; } = "encrypt";
    }

    public class AppUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Auth0UserId { get; set; } = string.Empty; // Auth0 sub

        [MaxLength(100)]
        public string Username { get; set; } = string.Empty; // Можна брати nickname з Auth0

        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        public List<CipherJob> CipherJobs { get; set; } = new();
    }

    public class CipherJob
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string InputText { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Key { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? ResultText { get; set; }

        [Required]
        public string Operation { get; set; } = "encrypt"; // encrypt / decrypt

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign key
        public int? UserId { get; set; }
        public AppUser? User { get; set; }
    }
}