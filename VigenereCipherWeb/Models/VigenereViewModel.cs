using System.ComponentModel;

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
}