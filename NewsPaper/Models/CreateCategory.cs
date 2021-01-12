using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPaper.Models
{
    public class CreateCategory
    {
        [Display(Name ="Category Name")]
        [Required(ErrorMessage ="Category Name is required.")]
        public string CategoryName { get; set; }
    }
}
