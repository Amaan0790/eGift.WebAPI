using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGift.WebAPI.Models
{
    public class CityModel : BaseModel
    {
        #region Data Models

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        public string CityCode { get; set; }

        [Column(TypeName = "VARCHAR(200)")]
        public string CityName { get; set; }

        public int StateId { get; set; }

        [Column(TypeName = "VARCHAR(500)")]
        public string? Description { get; set; }

        #endregion Data Models

        #region Not Mapped

        [NotMapped]
        public string StateName { get; set; }

        #endregion Not Mapped
    }
}