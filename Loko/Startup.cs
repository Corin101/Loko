using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Loko.Utils;

namespace Loko
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ReadConfiguration();
        }

        public IConfiguration Configuration { get; }
        public static IConfigurationRoot DbConfiguration { get; set; }
        private static string logFile = "Loko.log";
        private static string errorLogFile = "Error_Loko.log";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }
        
        // This method gets called by the runtime. It is used to get the setup for connecting to Data Base. 
        public static void ReadConfiguration()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("lokoSettings.json");
                DbConfiguration = builder.Build();
                Config.SqlServer = DbConfiguration["sqlServer"];
                Config.DatabaseName = DbConfiguration["databaseName"];
                Config.DbLogin = DbConfiguration["sqlLogin"];
                Config.DbPassword = DbConfiguration["sqlPassword"];
                Logger.WriteLog(logFile, "DataBase Configuration uploaded succesfully");
            }
            catch (Exception e)
            {
                Logger.WriteLog(errorLogFile, "Config file error : " + e.Message);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
