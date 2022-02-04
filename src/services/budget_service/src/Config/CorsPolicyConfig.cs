namespace Config
{
    public static class CorsPolicyConfig
    {
        public static void ConfigureCorsPolicy(IServiceCollection services, string policyName)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(policyName,
                    builder => builder.WithOrigins(
                        "http://localhost:5000"
                    )
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .AllowAnyHeader());
            });
        }
    }
}
