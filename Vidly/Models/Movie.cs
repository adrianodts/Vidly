using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        [StringLength(255)]
        public string Name { get; set; }
        //[Required(ErrorMessage = "Release Date is required")]        
        //[DateValidate]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }

        public DateTime? DateAdded { get; set; }

        [Required(ErrorMessage = "Number in Stock is required")]
        [Display(Name = "Number in Stock")]
        [Range(1, 20, ErrorMessage = "Number in Stock should be between 1 and 20")]
        public int StockQuantity{ get; set; }

        public int NumberAvailable { get; set; }

        public Genre Genre { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        [Display(Name = "Genre")]
        public byte GenreId { get; set; }

    }
}