using System.ComponentModel.DataAnnotations;

namespace LoginPractice.Entity
{
    public class Autor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Nombre { get; set; }    


        public List<Libro> libros { get; set; } 

        public List<AutorLibro> autoresLibros { get; set; }
    }
}
