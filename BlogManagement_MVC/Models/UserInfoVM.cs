using BlogManagement_MVC.Data;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BlogManagement_MVC.Models
{
    public class UserInfoVM
{
        public string Id { get; set; }
        [Required(ErrorMessage = "{0} is required!")]
        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "{0} is required!")]
        [Display(Name = "Middle Name")]
        [DataType(DataType.Text)]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "{0} is required!")]
        [Display(Name = "Last Name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "{0} must be format")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "{0} must be correctly format")]
        public string Email { get; set; }

        public DateTime RegisteredAt { get; set; }
        public string Intro { get; set; }

    }
}
