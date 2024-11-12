using eGift.WebAPI.Common;
using eGift.WebAPI.Models;

namespace eGift.WebAPI.Middlewares
{
    public class DefaultAdminUserMiddleware
    {
        #region Variables

        private readonly RequestDelegate _next;

        #endregion

        #region Constructors

        public DefaultAdminUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        #endregion

        #region Invoke Functions

        public async Task InvokeAsync(HttpContext context)
        {
            // Resolve the scoped service within the request scope
            var dbContext = context.RequestServices.GetRequiredService<ApplicationDbContext>();

            var defaultAdminModel = dbContext.Employee.Where(x => x.RoleId == (int)Role.Admin && x.IsDefault).FirstOrDefault();
            if (defaultAdminModel == null)
            {
                var employeeModel = new EmployeeModel()
                {
                    FirstName = "Amaan",
                    LastName = "Afzal",
                    DateOfBirth = Convert.ToDateTime("1999-09-07"),
                    GenderId = (int)Gender.Male,
                    Mobile = "9834551589",
                    Email = "mohammad.amaan0790@gmail.com",
                    IsActive = true,
                    IsDefault = true,
                    RoleId = (int)Role.Admin,
                    CreatedBy = 0,
                    CreatedDate = DateTime.Now
                };
                dbContext.Employee.Add(employeeModel);
                await dbContext.SaveChangesAsync();
                if (employeeModel?.ID > 0)
                {
                    var loginModel = new LoginModel()
                    {
                        RefId = employeeModel.ID,
                        RefType = Role.Admin.ToString(),
                        UserName = "Admin",
                        Password = "Admin@123",
                        RoleId = (int)Role.Admin,
                        IsActive = true
                    };
                    dbContext.Login.Add(loginModel);
                    await dbContext.SaveChangesAsync();
                }
            }

            //// Log request
            //Debug.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");

            // Call the next middleware in the pipeline
            await _next(context);

            //// Log response
            //Debug.WriteLine($"Response: {context.Response.StatusCode}");
        }

        #endregion
    }
}
