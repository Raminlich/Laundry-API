using LaundryAPI.Data;
using LaundryAPI.Filters;
using LaundryAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services;
using System.Text;

namespace LaundryAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var corsService = new CorsService();
            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ModelValidation>();
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddDbContext<UserDbContext>(options =>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbConnectionString")));
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddTransient<TokenService>();
            builder.Services.AddTransient<SignInService>();
            corsService.HandleCorsDevelop(builder);
            // corsService.HandleCorsStage(builder);

            var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    };
                });

            var app = builder.Build();

            app.UseCors("AllowFrontEnd");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
