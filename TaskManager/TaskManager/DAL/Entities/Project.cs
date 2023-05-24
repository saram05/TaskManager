using System.ComponentModel.DataAnnotations;

namespace TaskManager.DAL.Entities
{
    public class Project : Entity
    {
        [Display(Name = "Nombre del proyecto")]
        [MaxLength(45, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Descripcion proyecto")]
        [MaxLength(45, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; }

        [Display(Name = "Id usuario")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Guid IdUser { get; set; }

    }
}
