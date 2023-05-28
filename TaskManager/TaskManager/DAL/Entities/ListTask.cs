using System.ComponentModel.DataAnnotations;

namespace TaskManager.DAL.Entities
{
    public class ListTask 
    {
        [Key]
        public Guid  Id { get; set; }

        [Display(Name = "Nombre de la tarea")]
        [MaxLength(45, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        //property for foreign keys
        [Display(Name = "Projecto")]
        public Project Project { get; set; }
       

         [Display(Name = "Id Proyecto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Guid IdProyecto { get; set; }
    }
}
