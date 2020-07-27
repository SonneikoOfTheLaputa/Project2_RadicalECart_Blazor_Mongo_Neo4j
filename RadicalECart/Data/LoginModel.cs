using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RadicalECart.Data
{

    public class ResetPasswordModel
    {
        [Required]
        public string otp { get; set; }
       
        public string UserId { get; set; }
        [Required(ErrorMessage = "Username is required")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; }
    }
    public class DeactivateModel
    {
        [Required(ErrorMessage = "Username is required")]

        public string UserId { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
       [Compare("Password",ErrorMessage ="Passwords do not match")]
        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; }

    }
    public class RegisterModel
    {
        [Required]
        [Compare("Password",ErrorMessage ="Passwords do not match")]
        public string ConfirmPassword { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]{1,30}$",
         ErrorMessage = "Only alpha numerics are allowed with maximum length of 30")]
        public string UserId { get; set; }
        [Required]
        [MinLength(1),MaxLength(50, ErrorMessage = "Firstname should be " +
            "in between 1 and 50")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(1), MaxLength(50,ErrorMessage ="Lastname should be in between 1 and 50")]
        public string LastName { get; set; }
        [Required]
        [Range(8, 120, ErrorMessage = "Age should be in between 8 and 120")]
        public string Age { get; set; }
        [Required]
        [MinLength(1), MaxLength(150, ErrorMessage = "Address-1 should be in " +
            "between 1 and 150")]
        public string Address1 { get; set; }
        [Required]
        [MinLength(1), MaxLength(150, ErrorMessage = "Address-2 should be in " +
            "between 1 and 150")]
        public string Address2 { get; set; }
        //[Required]
        //[MinLength(1), MaxLength(150, ErrorMessage = "City should be in " +
        //    "between 1 and 150")]
        public string City { get; set; }
       
        [Required]
        [Range(10000, 699999, ErrorMessage = "Invalid pincode (should be in between 10000 and 699999")]
        public string PinCode { get; set; }
        //[Required]
        //[MinLength(1), MaxLength(150, ErrorMessage = "Country should " +
        //    "be in between 1 and 150")]
        public string Country { get; set; }
        
        [EmailAddress]
        [Required]
        
        public string Email { get; set; }
        [Required]
        [Phone]
        public string MobileNumber { get; set; }
        [Required]
        [RegularExpression(@"^(?=(.*\d){2})(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\d]).{8,}$",ErrorMessage ="Invalid password (should have -> atleast 2 digits,a lowercase," +
            " a uppercase, a special character and total of more than 7 characters")]
        public string Password { get; set; }
        [Required]
        [Range(1,5000,ErrorMessage ="Invalid Area code (should be in between 1 and 5000")]
        public string AreaCode { get; set; }
    }
    public class LoginModel
    {
        [Required(ErrorMessage = "Email address is required")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        public bool CheckBoxItem { get; set; }
    }
}
