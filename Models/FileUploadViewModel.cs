using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AzureUpload.Models
{
    public class FileUploadViewModel
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("File")]
        [Required]
        public required IFormFile File { get; set; }

    }
}