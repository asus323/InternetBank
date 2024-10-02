using System.ComponentModel.DataAnnotations;
using DNTPersianUtils.Core;

namespace InternetBank.Data.User
{
    public class SingUpDTO{
        [Required,ShouldContainOnlyPersianLetters(ErrorMessage = "لطفا زبان کیبورد را به فارسی تغییر دهید")]
        public string FirstName { get; set;}
        [Required,ShouldContainOnlyPersianLetters(ErrorMessage = "لطفا زبان کیبورد را به فارسی تغییر دهید")]
        public string LastName { get; set;}
        [Required,ValidIranianNationalCode]
        public int NationalCode { get; set; }
        [Required,Range(18,150,ErrorMessage = "حداقل باید 18 سال سن داشته باشید")]
        public string BirthDate { get; set; }
        [Required(ErrorMessage ="لطفا شماره تلفن خود را وارد کنید"),ValidIranianMobileNumber(ErrorMessage ="شماره تلفن وارد شده معتبر نیست"),]
        public string PhoneNumber { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required,Compare("ConfirmPassword",ErrorMessage = "پسورد با تاییدیه پسورد مطابقت ندارد")]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
