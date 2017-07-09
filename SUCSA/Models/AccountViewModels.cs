using SUCSA.DATA;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SUCSA.Models
{

    public class LoginViewModel
    {
        [Required(ErrorMessage = "请输入管理员账号")]
        public string Username { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }


    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage ="请输入旧密码")]
        [StringLength(100)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "请输入新密码")]
        [StringLength(100, ErrorMessage = "新密码长度至少为6", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = "新旧密码不一致。")]
        public string ConfirmPassword { get; set; }
    }

    public class AccountViewModels
    {
        public List<Admin> Admins { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }
}
