using System.ComponentModel.DataAnnotations;
using TaskApp.DAL.Entities;

namespace TaskManager.DAL.Entities
{
    public class Task : Entity
    {
        [Display(Description = "Descripción de la tarea")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; }

        [Display(Name = "Fecha de inicio")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Fecha fin")]
        public DateTime? FinishDate { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Status { get; set; }

        [Display(Name = "Notas")]
        public string? Notes {get; set;}

        //property for foreign keys
        [Display(Name = "Usuario de creación")]
        public User User { get; set; }

        [Display(Name = "Id usuario creación")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Guid IdUser { get; set; }

        [Display(Name = "Lista de la tarea")]
        public ListTask List { get; set; }

        [Display(Name = "Id lista")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Guid IdList { get; set; }

    }
}
