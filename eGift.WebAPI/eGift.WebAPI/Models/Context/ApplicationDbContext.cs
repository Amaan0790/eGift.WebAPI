using Microsoft.EntityFrameworkCore;

namespace eGift.WebAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        #region Constructor

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #endregion Constructor

        #region DBSets

        //Address
        public DbSet<AddressModel> Address { get; set; }

        //Category
        public DbSet<CategoryModel> Category { get; set; }

        //City
        public DbSet<CityModel> City { get; set; }

        //Country
        public DbSet<CountryModel> Country { get; set; }

        //Customer
        public DbSet<CustomerModel> Customer { get; set; }

        //Employee
        public DbSet<EmployeeModel> Employee { get; set; }

        //Gender
        public DbSet<GenderModel> Gender { get; set; }

        //Login
        public DbSet<LoginModel> Login { get; set; }

        //Order
        public DbSet<OrderModel> Order { get; set; }

        //Order Details
        public DbSet<OrderDetailsModel> OrderDetails { get; set; }

        //Product
        public DbSet<ProductModel> Product { get; set; }

        //Role
        public DbSet<RoleModel> Role { get; set; }

        //State
        public DbSet<StateModel> State { get; set; }

        //Sub Category
        public DbSet<SubCategoryModel> SubCategory { get; set; }

        #endregion DBSets
    }
}