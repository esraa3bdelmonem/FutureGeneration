



using Future_Generation.RepoServices;
using Future_Generation.Repositories;

namespace Future_Generation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var ConnectionString = builder.Configuration.GetConnectionString(name: "DefaultConnection")
                                         ?? throw new InvalidOperationException(message: "No Connection String was found");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(ConnectionString));
            builder.Services.AddScoped<ICourseRepository, CourseRepositoryService>();
            builder.Services.AddScoped<IRepository<Student>, StudentRepositoryService>();
            builder.Services.AddScoped<IRepository<StudentCourses>, StudentCoursesRepositoryService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}