using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(255)]
        public string Name{ get; set; }

        [Min18YearsIfAMember]
        [Display(Name = "Birth of Date")]
        public DateTime? BirthDate{ get; set; }
        //[DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]

        public bool IsSubscribedToNewsLetter{ get; set; }

        public MembershipType MembershipType { get; set; }

        [Required(ErrorMessage = "Membership Type is required")]
        [Display(Name= "Membership Type")]
        public byte MembershipTypeId { get; set; }



    }
}