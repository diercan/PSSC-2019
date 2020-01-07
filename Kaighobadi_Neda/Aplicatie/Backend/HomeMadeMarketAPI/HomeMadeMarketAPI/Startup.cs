using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HomeMadeMarketAPI.Models;
using Microsoft.Extensions.Options;
using HomeMadeMarketAPI.Services;
using Microsoft.AspNetCore.Authentication;
using HomeMadeMarketAPI.Helpers;

namespace HomeMadeMarketAPI
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
            // requires using Microsoft.Extensions.Options
            services.Configure<HomeMadeMarketDatabaseSettings>(
                Configuration.GetSection(nameof(HomeMadeMarketDatabaseSettings)));

            services.AddSingleton<IHomeMadeMarketDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<HomeMadeMarketDatabaseSettings>>().Value);

            services.AddSingleton<LoginService>();
            services.AddSingleton<ProductService>();
            services.AddCors();

            services.AddControllers()
            .AddNewtonsoftJson(options => options.UseMemberCasing());
            services.AddAuthentication("Authentication")
               .AddScheme<AuthenticationSchemeOptions, AuthenticationHandler>("Authentication", null);
            services.AddScoped<ILoginService, LoginService>();

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
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
