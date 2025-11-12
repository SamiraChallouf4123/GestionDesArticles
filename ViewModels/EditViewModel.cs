namespace GestionDesArticles.ViewModels
{
    public class EditViewModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }

        public int QteStock { get; set; }

        public int? CategoryId { get; set; }

        public string Type { get; set; }   // ✅ ici aussi

        public IFormFile ImagePath { get; set; }

        public string ExistingImagePath { get; set; }
    }
}
