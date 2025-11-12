using System.ComponentModel.DataAnnotations;

namespace GestionDesArticles.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "Nom")]
        public string CategoryName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
