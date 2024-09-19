﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGift.WebAPI.Models
{
    public class CountryModel : BaseModel
    {
        #region Data Models

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        public string CountryCode { get; set; }

        [Column(TypeName = "VARCHAR(200)")]
        public string CountryName { get; set; }

        [Column(TypeName = "VARCHAR(500)")]
        public string? Description { get; set; }

        #endregion Data Models
    }
}