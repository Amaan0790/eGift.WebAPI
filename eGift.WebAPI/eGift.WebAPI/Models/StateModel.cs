using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGift.WebAPI.Models
{
    public class StateModel : BaseModel
    {
        #region Data Models

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        public string StateCode { get; set; }

        [Column(TypeName = "VARCHAR(200)")]
        public string StateName { get; set; }

        public int CountryId { get; set; }

        [Column(TypeName = "VARCHAR(500)")]
        public string? Description { get; set; }

        #endregion Data Models

        #region Not Mapped

        [NotMapped]
        public string? CountryName { get; set; }

        #endregion Not Mapped
    }
}