using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialPortal.ViewModels
{
    public class UpdateViewModel
    {
        public string Id { get; set; }
        [StringLength(20, ErrorMessage = "Must be within {2} and {1} long.", MinimumLength = 1)]
        public string FirstName { get; set; }
        [StringLength(20, ErrorMessage = "Must be within {2} and {1} long.", MinimumLength = 1)]
        public string LastName { get; set; }
        [MaxLength(20)]
        public string DisplayName { get; set; }
        public string AvatarUrl { get; set; }
        [StringLength(50, ErrorMessage = "The Email must be between {2} and {1} characters long.", MinimumLength = 5)]
        public string Email { get; set; }
    }
}