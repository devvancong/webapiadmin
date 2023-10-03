namespace Webapi.Logs
{
    public static class LogFactory
    {
        public static void Configuration(this IApplicationBuilder app, IConfiguration configuration)
        {
            var loggerFactory = app.ApplicationServices.GetService<ILoggerFactory>();
            loggerFactory.AddFile(configuration["Logging:LogFilePath"].ToString());
        }
    }
}
