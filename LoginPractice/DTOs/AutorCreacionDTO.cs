﻿using System.ComponentModel.DataAnnotations;

namespace LoginPractice.DTOs
{
    public class AutorCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Nombre { get; set; }
    }
}
