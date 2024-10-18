using System.ComponentModel;

namespace eGift.WebAPI.Common
{
    #region General

    public enum Gender
    {
        [Description("Male")]
        Male = 1,

        [Description("Female")]
        Female = 2
    }

    #endregion

    #region Project Specific

    public enum Role
    {
        [Description("Employee")]
        Employee = 1,

        [Description("Customer")]
        Customer = 2,

        [Description("Admin")]
        Admin = 3
    }

    #endregion
}
