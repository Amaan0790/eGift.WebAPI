﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGift.WebAPI.Models
{
    public class AddressModel : BaseModel
    {
        #region Data Models

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "VARCHAR(200)")]
        public string? Street1 { get; set; }

        [Column(TypeName = "VARCHAR(200)")]
        public string? Street2 { get; set; }

        public int CountryId { get; set; }

        public int StateId { get; set; }

        public int CityId { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        public string? Pincode { get; set; }

        #endregion Data Models

        #region Not Mapped

        [NotMapped]
        public string? CountryName { get; set; }

        [NotMapped]
        public string? StateName { get; set; }

        [NotMapped]
        public string? CityName { get; set; }

        //[NotMapped]
        //public string? FullAddress { get; set; }

        [NotMapped]

        // This property computes FullAddress based on other properties
        public string? FullAddress
        {
            get
            {
                return $"{Street1}, {CityName}, {StateName}, {CountryName} - {Pincode}";
            }
        }

        #endregion Not Mapped
    }
}