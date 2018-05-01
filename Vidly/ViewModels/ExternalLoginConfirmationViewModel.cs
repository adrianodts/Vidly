using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Microsoft.Build.Framework.Required]
        [Display(Name = "Driving License")]
        public string DrivingLicense { get; set; }

        [Microsoft.Build.Framework.Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Microsoft.Build.Framework.Required]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }
    }
}