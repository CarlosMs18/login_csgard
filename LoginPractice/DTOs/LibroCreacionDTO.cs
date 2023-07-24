using LoginPractice.Entity;
using System.ComponentModel.DataAnnotations;

namespace LoginPractice.DTOs
{
    public class LibroCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Titulo { get; set; }

        public DateTime? FechaPublicacion { get; set; }

        public List<int> AutoresIds { get; set; }
    }
}
