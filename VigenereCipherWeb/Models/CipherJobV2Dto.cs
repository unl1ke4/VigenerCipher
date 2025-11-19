namespace VigenereCipherWeb.Models
{
    // DTO = Data Transfer Object. Це для передачі даних через API.
    public class CipherJobV2Dto
    {
        public int Id { get; set; }
        public string InputText { get; set; } = string.Empty;
        public string ResultText { get; set; } = string.Empty;
        public string Operation { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // У v2 показуємо імена, а не ID
        public string UserName { get; set; } = string.Empty;
        public string CipherMethodName { get; set; } = string.Empty;
    }
}