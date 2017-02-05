using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PW_Application.Models
{
    public class NewPayment
    {
        [Required]
        [Display(Name = "Correspondent")]
        public string CorrespondentName { get; set; }

        [RegularExpression("([0-9]*)", ErrorMessage = "Ammount must be a natural number")]
        [Required]
        [Display(Name = "Ammount")]
        public int Ammount { get; set; }
    }
}