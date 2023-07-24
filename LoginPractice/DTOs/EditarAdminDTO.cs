using System.ComponentModel.DataAnnotations;

namespace LoginPractice.DTOs
{
    public class EditarAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
