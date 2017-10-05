namespace ConductorAppAdmin
{
    using ConductorAppAdmin.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Start das configurações.
    /// </summary>
    public class Startup
    {
        #region Properties

        /// <summary>
        /// Obtem a configuração.
        /// </summary>
        public IConfiguration Configuration { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construtor da classe do Startup.
        /// </summary>
        /// <param name="configuration">Configuração.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Coleção de serviços.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.Configure<StorageAccountOption>(Configuration.GetSection("StorageAccount"));

            services.AddDbContext<ConductorAppAdminContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConductorAppAdminContext")));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Aplicativo.</param>
        /// <param name="env">Ambiente.</param>
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

        #endregion
    }
}
