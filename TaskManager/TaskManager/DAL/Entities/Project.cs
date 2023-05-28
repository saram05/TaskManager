using System.ComponentModel.DataAnnotations;
using TaskApp.DAL.Entities;

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

        //property for foreign keys
        [Display(Name = "usuario")]
        public User User { get; set; }

        [Display(Name = "Id usuario")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Guid IdUser { get; set; }

        //a project can have n tasks
        public ICollection <ListTask> ListTasks { get; set; }

        //read property
        public int ListTasksNumber => ListTasks == null ? 0 : ListTasks.Count;

    }
}
