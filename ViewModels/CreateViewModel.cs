using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GestionDesArticles.ViewModels
{
    public class CreateViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public int QteStock { get; set; }

        [Required]
        public int? CategoryId { get; set; }

        [Required]
        public string Type { get; set; }   // ✅ ici

        public IFormFile ImagePath { get; set; }
    }
}
