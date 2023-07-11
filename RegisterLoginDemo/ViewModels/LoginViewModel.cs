using System.ComponentModel.DataAnnotations;

namespace RegisterLoginDemo.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Please enter username:")]
        [Display(Name = "Enter username:")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Please enter password:")]
        [Display(Name = "Enter password:")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
