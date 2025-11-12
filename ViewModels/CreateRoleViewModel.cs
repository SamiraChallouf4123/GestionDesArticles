using System.ComponentModel.DataAnnotations;
namespace GestionDesArticles.ViewModels
 {
    public class CreateRoleViewModel
 {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
