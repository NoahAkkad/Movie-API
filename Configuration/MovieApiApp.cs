using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MovieApi.Data;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Globalization;
using MovieApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace MovieApi.Configuration
{
	public class MovieApiApp
	{
        private readonly WebApplicationBuilder _builder;
        private readonly WebApplication _app;
        public MovieApiApp(string[] args)
        {
            _builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ConfigureServices(_builder);

            _app = _builder.Build();

            // Configure the HTTP request pipeline.
            ConfigureMiddlewares();
        }

        private void ConfigureServices(WebApplicationBuilder _builder)
        {
            var connectionString = _builder.Configuration.GetConnectionString("DefaultConnection");

            _builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(string.Empty);
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo(string.Empty) };
                options.SupportedUICultures = new List<CultureInfo> { new CultureInfo(string.Empty) };
            });


            _builder.Services.AddDbContext<DatabaseContext>(Options =>
            Options.UseSqlServer(_builder.Configuration.GetConnectionString("DefaultConnection")));

            AddAuthApi();

            _builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            _builder.Services.AddEndpointsApiExplorer();
            _builder.Services.AddSwaggerGen(CustomSwaggerGenOptions);
        }

        private void AddAuthApi()
        {
            _builder.Services.AddAuthorization();
            //We will add endpoints with type customuser and we will save users in the database context
            _builder.Services.AddIdentityApiEndpoints<CustomUser>(CustomIdentityOptions)
                .AddEntityFrameworkStores<DatabaseContext>();
        }

        private void CustomIdentityOptions(IdentityOptions options)
        {
            if (_builder.Environment.IsDevelopment())
            {
                // Set simpler passwordRequirements for development
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                //options.Password.RequiredLength = 4;
            }
        }

        private void CustomSwaggerGenOptions(SwaggerGenOptions options)
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey

            });

            options.OperationFilter<SecurityRequirementsOperationFilter>(); // Required for using access token
        }

        private void ConfigureMiddlewares()
        {
            if (_app.Environment.IsDevelopment())
            {
                _app.UseSwagger();
                _app.UseSwaggerUI();
            }

            _app.UseHttpsRedirection();

            _app.MapIdentityApi<CustomUser>();

            _app.UseAuthorization();

            _app.MapControllers();
        }

        public void Run()
        {
            _app.Run();
        }
    }
}

