using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGift.WebAPI.Models
{
    public class SubCategoryModel : BaseModel
    {
        #region Data Models

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int CategoryId { get; set; }

        [Column(TypeName = "VARCHAR(200)")]
        public string SubCategoryName { get; set; }

        [Column(TypeName = "VARCHAR(500)")]
        public string? Description { get; set; }

        #endregion Data Models

        #region Not Mapped

        [NotMapped]
        public string CategoryName { get; set; }

        #endregion Not Mapped
    }
}