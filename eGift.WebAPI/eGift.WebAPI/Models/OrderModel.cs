using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGift.WebAPI.Models
{
    public class OrderModel : BaseModel
    {
        #region Data Models

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int CustomerId { get; set; }

        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal? TotalDiscount { get; set; }

        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal? TotalTax { get; set; }

        [Column(TypeName = "VARCHAR(200)")]
        public string OrderNumber { get; set; }

        [Column(TypeName = "VARCHAR(500)")]
        public string? Notes { get; set; }

        public DateTime? DispatchedDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public DateTime? CancelDate { get; set; }

        public int StatusId { get; set; }

        #endregion Data Models

        #region Not Mapped

        [NotMapped]
        public string StatusName { get; set; }

        [NotMapped]
        public string CustomerName { get; set; }

        #endregion Not Mapped
    }
}