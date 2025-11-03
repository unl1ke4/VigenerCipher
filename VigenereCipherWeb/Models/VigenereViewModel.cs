using System.ComponentModel;

namespace VigenereCipherWeb.Models
{
    public class VigenereViewModel
    {
        [DisplayName("Введіть текст")]
        public string? InputText { get; set; }

        [DisplayName("Введіть ключ")]
        public string? Key { get; set; }

        [DisplayName("Результат")]
        public string? ResultText { get; set; }

        public string Operation { get; set; } = "encrypt";
    }
}