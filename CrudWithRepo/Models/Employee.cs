using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudWithRepo.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
        public string? Photo { get; set; }
        [Display(Name = "Choose Image")]
        [NotMapped]
        public IFormFile ImgPath { get; set; }
    }
}
