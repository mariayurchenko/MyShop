using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SB.WebShared.DynamicsAuthentication;

namespace HappyBirthdayWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();


            var _clientId = Configuration["ClientOptions:ClientId"];
            var _clientSecret = Configuration["ClientOptions:ClientSecret"];
            var _resource = Configuration["ClientOptions:Resource"];
            var _apiVersion = Configuration["ClientOptions:ApiVersion"];
            var _tenantId = Configuration["ClientOptions:TenantId"];

            //services.AddAzureAppConfiguration();

            services.AddSingleton<IAuthenticationService>(x =>
                new AuthenticationService(_clientId, _clientSecret, _resource, _apiVersion, _tenantId));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
