﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage ="The field {0} is required" )]
        [MaxLength(50, ErrorMessage ="The field {0} must be maimum {1} characters length")]
        [Index("City_CompanyId_Description_Index", 2, IsUnique =true)]
        [Display(Name="Category")]
        public string Description { get; set; }

        [Required(ErrorMessage ="The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage ="You must select a {0}")]
        [Index("City_CompanyId_Description_Index", 1, IsUnique = true)]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }


        public virtual Company Company { get; set; }
         
    }
}