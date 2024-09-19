using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGift.WebAPI.Models
{
    public class EmployeeModel : BaseModel
    {
        #region Data Models

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "VARCHAR(200)")]
        public string FirstName { get; set; }

        [Column(TypeName = "VARCHAR(200)")]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public int GenderId { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        public string Mobile { get; set; }

        [Column(TypeName = "VARCHAR(200)")]
        public string? Email { get; set; }

        public int? AddressId { get; set; }

        public bool IsActive { get; set; } = true;

        [Column(TypeName = "VARCHAR(500)")]
        public string? ProfileImagePath { get; set; }

        [Column(TypeName = "VARBINARY(MAX)")]
        public byte[]? ProfileImageData { get; set; }

        public int RoleId { get; set; }

        public bool IsDefault { get; set; } = false;

        #endregion Data Models

        #region Not Mapped

        [NotMapped]
        public int Age { get; set; }

        [NotMapped]
        public string AddressName { get; set; }

        [NotMapped]
        public string GenderName { get; set; }

        [NotMapped]
        public string RoleName { get; set; }

        [NotMapped]
        public IFormFile ProfileImage { get; set; }

        #endregion Not Mapped
    }
}