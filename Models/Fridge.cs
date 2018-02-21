//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FridgeBuddy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    
    public partial class Fridge
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Picture { get; set; }
       

      
        public string Item { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s\-]*$")]
        [Required]
        [StringLength(30)]
        public string Category { get; set; }

  


        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Location { get; set; }


        [Display(Name = "Expiry Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime ExpiryDate { get; set; }

        [Display(Name = "Days Till Expiry")]
        public Nullable<int> DaysTillExpiry { get; set; }
    }
}