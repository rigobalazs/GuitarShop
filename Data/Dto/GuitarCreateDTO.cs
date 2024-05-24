using System.ComponentModel.DataAnnotations;

namespace Data.Dto
{
    public class GuitarCreateDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        [Range(1, int.MaxValue)]
        public double Price { get; set; }
    }
}
