namespace LaundryAPI.Services
{
    public class CorsService
    {
        private readonly WebApplicationBuilder _builder;

        public CorsService(WebApplicationBuilder builder)
        {
            _builder = builder;
        }

        public void HandleCors()
        {
            var env = _builder.Environment.EnvironmentName;
            var origins = _builder.Configuration.GetSection($"AllowedOrigins:{env}").Get<string[]>();
            _builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontEnd", policy =>
                {
                    policy.WithOrigins(origins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });
        }
    }
}
