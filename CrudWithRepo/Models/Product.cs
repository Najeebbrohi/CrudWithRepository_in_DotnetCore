using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudWithRepo.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Descripion { get; set; }
        public int Price { get; set; }
        public string? Photo { get; set; }
        [Display(Name = "Choose Image")]
        [NotMapped]
        public IFormFile ImgPath { get; set; }
    }
}
