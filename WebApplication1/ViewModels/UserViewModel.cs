using System.ComponentModel.DataAnnotations;

namespace AuthProject.ViewModels
{
    public class ResetPasswordDtoModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }
    }

    public class LoginAndPasswordModel : ResetPasswordDtoModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }

    public class UserCreateModel : ResetPasswordDtoModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }
    }

    public class UserUpdateModel
    {
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }
    }

    public class UserViewModel : ResetPasswordDtoModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public long Id { get; set; }
    }
}
