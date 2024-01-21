using System.ComponentModel.DataAnnotations;

namespace AzureUpload.Models.FileUpload
{
    public class FileUpload
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string FileUrl { get; set; }

    }
}