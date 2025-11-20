namespace POE_Part_3.Models
{
    public class Document
    {
        public int Id { get; set; } // Document ID
        public string FileName { get; set; } // Original file name
        public string StoredFileName { get; set; } // Name used for storage
        public string DocumentType { get; set; } // Type of document
        public string Description { get; set; } // Document description
        public long FileSize { get; set; } // File size in bytes
        public DateTime UploadDate { get; set; } // Upload timestamp
        public int? ClaimId { get; set; } // Associated claim ID (optional)
    }
}