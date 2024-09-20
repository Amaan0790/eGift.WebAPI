using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGift.WebAPI.Models
{
    public class OrderDetailsModel : BaseModel
    {
        #region Data Models

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal? Discount { get; set; }

        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal? Tax { get; set; }

        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal NetAmount { get; set; }

        #endregion Data Models
    }
}