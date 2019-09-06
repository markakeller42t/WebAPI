using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebAPI.Services;

namespace WebAPI
{
    public class Startup
    {
        private readonly string _SQLUserId = null;
        private readonly string _SQLPassword = null;
        private readonly string _connstringSystem = null;
        private readonly string _connstringApp = null;
        private readonly ApplicationConnectionString applicationConnectionString;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _SQLUserId = Configuration["SQL:UserId"];
            _SQLPassword = Configuration["SQL:Password"];

            var builderSys = new SqlConnectionStringBuilder(
                Configuration.GetConnectionString("SQL_System"));
            builderSys.UserID = _SQLUserId;
            builderSys.Password = _SQLPassword;
            _connstringSystem = builderSys.ConnectionString;

            var builderApp = new SqlConnectionStringBuilder(
                Configuration.GetConnectionString("SQL_Application"));
            builderApp.UserID = _SQLUserId;
            builderApp.Password = _SQLPassword;
            _connstringApp = builderApp.ConnectionString;

            applicationConnectionString = new ApplicationConnectionString(_connstringApp);
            

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<IApplicationConnectionString>(applicationConnectionString);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            //app.Run(async (context) => {
            //    await context.Response.WriteAsync($"Connection String System:{_connstringSystem}" +
            //        $"{Environment.NewLine}Connection String App:{_connstringApp}" +
            //        $"{Environment.NewLine}Environment:{env.EnvironmentName}");
            //    });
        }
    }
}
