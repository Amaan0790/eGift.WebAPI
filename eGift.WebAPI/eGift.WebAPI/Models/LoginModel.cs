using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGift.WebAPI.Models
{
    public class LoginModel 
    {
        #region Data Models

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int RefId { get; set; }

        [Column(TypeName = "VARCHAR(200)")]
        public string RefType { get; set; }

        [Column(TypeName = "VARCHAR(200)")]
        public string UserName { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        public string Password { get; set; }

        public int RoleId { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? LogInDate { get; set; }

        public DateTime? LastLogInDate { get; set; }

        #endregion Data Models

        #region Not Mapped

        [NotMapped]
        public string ConfirmPassword { get; set; }

        #endregion Not Mapped
    }
}