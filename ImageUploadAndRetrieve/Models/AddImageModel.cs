using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;

namespace ImageUploadAndRetrieve.Models
{
    public class AddImageModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [NotMapped]
        public IFormFile File { get; set; }

    }
}
