﻿namespace LoginPractice.Entity
{
    public class AutorLibro
    {
       public int LibroId { get; set; } 
       public int AutorId { get; set; }    
       public int Orden { get; set; }
       public Autor Autor { get; set; } 
       public Libro Libro { get; set; } 
    }
}
