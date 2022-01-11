namespace AssignmentAPI.Configurations.Policies
{
    public static class PoliciesExtention
    {
        public static void ConfigurePolicies(this IServiceCollection services, PoliciesConfig policies)
        {
            policies.AllowPolicies.ForEach(policy => {
                services.AddCors(options => {
                    options.AddPolicy(name: policy.Name, builder => {
                        foreach (string url in policy.Allowed)
                        {
                            builder.WithOrigins(url)
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                        }
                    });
                });
            });
        }

        public static void UsePolicies(this IApplicationBuilder app, PoliciesConfig config)
        {
            config.AllowPolicies.ForEach(policy => app.UseCors(policy.Name));
        }
    }
}
