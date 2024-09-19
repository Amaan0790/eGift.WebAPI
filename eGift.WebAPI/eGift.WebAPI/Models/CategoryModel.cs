﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGift.WebAPI.Models
{
    public class CategoryModel : BaseModel
    {
        #region Data Models

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "VARCHAR(200)")]
        public string CategoryName { get; set; }

        [Column(TypeName = "VARCHAR(500)")]
        public string? Description { get; set; }

        #endregion Data Models
    }
}