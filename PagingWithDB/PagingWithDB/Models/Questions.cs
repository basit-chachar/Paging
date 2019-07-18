using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PagingWithDB.Models
{
    public class Questions
    {
        [BindNever]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Question")]
        [StringLength(500,
            ErrorMessage = "Please enter question!")]
        public string Question { get; set; }

        [Required]
        [Display(Name = "Answer")]
        [StringLength(500,
            ErrorMessage = "Please enter answer!")]
        public string Answer { get; set; }

    }
}
