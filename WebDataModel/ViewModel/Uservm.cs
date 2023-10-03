using System.ComponentModel.DataAnnotations;

namespace WebDataModel.ViewModel
{
    public class Uservm
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [MinLength(5,ErrorMessage ="Nhỏ quá vậy!")]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        public DateTime? Birthday { get; set; }

        public int? Age { get; set; }
        public bool? Sex { get; set; }
    }
}