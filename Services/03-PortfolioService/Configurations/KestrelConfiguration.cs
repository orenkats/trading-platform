namespace PortfolioService.Configurations
{
    public static class KestrelConfiguration
    {
        public static void ConfigureKestrel(this WebApplicationBuilder builder)
        {
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenLocalhost(5001); // Ensure this port is unique
            });
        }
    }
}
