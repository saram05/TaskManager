using System.ComponentModel.DataAnnotations;

namespace TaskManager.DAL.Entities
{
    public class Group : Entity
    {
        [Display(Name = "Grupo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
