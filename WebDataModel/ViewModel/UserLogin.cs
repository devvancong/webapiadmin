using System.ComponentModel.DataAnnotations;

namespace WebDataModel.ViewModel
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Không bỏ trống!")]
        [MaxLength(255, ErrorMessage = "Qúa 255 ký tự!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Không bỏ trống!")]
        [MaxLength(255, ErrorMessage = "Qúa 255 ký tự!")]
        public string Password { get; set; }
    }
}