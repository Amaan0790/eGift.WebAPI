using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGift.WebAPI.Models
{
    public class ProductModel : BaseModel
    {
        #region Data Models

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "VARCHAR(200)")]
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public int SubCategoryId { get; set; }

        public int? QuantityPerUnit { get; set; }

        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal UnitPrice { get; set; }

        public int? SizeId { get; set; }

        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal? Discount { get; set; }

        public int? UnitInStock { get; set; }

        public int? UnitInOrder { get; set; }

        public int? ProductAvailable { get; set; }

        [Column(TypeName = "VARCHAR(500)")]
        public string? ShortDescription { get; set; }

        [Column(TypeName = "VARCHAR(4000)")]
        public string? LongDescription { get; set; }

        [Column(TypeName = "VARCHAR(500)")]
        public string? PicturePath1 { get; set; }

        [Column(TypeName = "VARCHAR(500)")]
        public string? PicturePath2 { get; set; }

        [Column(TypeName = "VARCHAR(500)")]
        public string? PicturePath3 { get; set; }

        [Column(TypeName = "VARCHAR(500)")]
        public string? PicturePath4 { get; set; }

        [Column(TypeName = "VARBINARY(MAX)")]
        public byte[]? PictureData1 { get; set; }

        [Column(TypeName = "VARBINARY(MAX)")]
        public byte[]? PictureData2 { get; set; }

        [Column(TypeName = "VARBINARY(MAX)")]
        public byte[]? PictureData3 { get; set; }

        [Column(TypeName = "VARBINARY(MAX)")]
        public byte[]? PictureData4 { get; set; }

        [Column(TypeName = "VARCHAR(500)")]
        public string ProductImagePath { get; set; }

        [Column(TypeName = "VARBINARY(MAX)")]
        public byte[] ProductImageData { get; set; }

        #endregion Data Models

        #region Not Mapped

        [NotMapped]
        public IFormFile? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? Picture1 { get; set; }

        [NotMapped]
        public IFormFile? Picture2 { get; set; }

        [NotMapped]
        public IFormFile? Picture3 { get; set; }

        [NotMapped]
        public IFormFile? Picture4 { get; set; }

        [NotMapped]
        public string? CategoryName { get; set; }

        [NotMapped]
        public string? SubCategoryName { get; set; }

        [NotMapped]
        public string? SizeName { get; set; }

        #endregion Not Mapped
    }
}