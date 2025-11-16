using VigenereCipherWeb.Data;

namespace VigenereCipherWeb.Models
{
    public class SearchViewModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string? InputTextStartsWith { get; set; }
        public string? InputTextEndsWith { get; set; }
        public string? ResultTextStartsWith { get; set; }
        public string? ResultTextEndsWith { get; set; }

        public List<CipherJobSearchResult> Results { get; set; } = new();
    }

    public class CipherJobSearchResult
    {
        public int Id { get; set; }
        public string InputText { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public string Operation { get; set; } = string.Empty;
        public string ResultText { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        
        // JOIN дані
        public string Username { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string MethodName { get; set; } = string.Empty;
        public string MethodDescription { get; set; } = string.Empty;
    }
}
