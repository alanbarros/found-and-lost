using System.ComponentModel.DataAnnotations;

namespace Application.Boundaries.Inputs
{
    public class CategoryInput
    {
        [Required]
        [MinLength(3, ErrorMessage = "O nome deve conter ao menos 3 letras")]
        [MaxLength(20, ErrorMessage = "O nome deve conter no máximo 20 letras")]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;
    }
}