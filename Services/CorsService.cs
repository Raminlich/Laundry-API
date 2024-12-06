namespace LaundryAPI.Services
{
    public class CorsService
    {
        public CorsService()
        {
            
        }

        public void HandleCorsDevelop(WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontEnd", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });
        }

        public void HandleCorsStage(WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontEnd", policy =>
                {
                    policy.WithOrigins("http://192.168.1.102:2000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });
        }
    }
}
