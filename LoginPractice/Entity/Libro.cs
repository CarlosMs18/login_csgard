using System.ComponentModel.DataAnnotations;

namespace LoginPractice.Entity
{
    public class Libro
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Titulo { get; set; }  
        public DateTime? FechaPublicacion { get; set; } 
       
        public List<Comentario> Comentarios { get; set; }   

        public List<AutorLibro> AutoresLibros { get; set;}
    }
}
