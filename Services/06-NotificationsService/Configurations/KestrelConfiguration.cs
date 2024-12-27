namespace NotificationsService.Configurations
{
    public static class KestrelConfiguration
    {
        public static void ConfigureKestrel(this WebApplicationBuilder builder)
        {
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenLocalhost(5005); // Ensure this port is unique
            });
        }
    }
}
