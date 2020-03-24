using System.Threading.Tasks;
using CustomerApi.Data;
using CustomerApi.HiddenModel;
using CustomerApi.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CustomerApi
{
    public class Startup
    {
        readonly string cloudAMQPConnectionString = "host=hawk.rmq.cloudamqp.com;virtualHost=embszgpu;username=embszgpu;password=Maer_MLg3Ib4341CdUnICTys6OBO53Lq";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CustomerApiContext>(o => o.UseInMemoryDatabase("CustomersDb"));
            services.AddScoped<IRepository<HiddenCustomer>, CustomerRepository>();
            services.AddTransient<IDbInitializer, DbInitializer>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Task.Factory.StartNew(() =>
            new MessageListener(app.ApplicationServices, cloudAMQPConnectionString).Start());

            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
                // Initialize the database
                var services = scope.ServiceProvider;
                var dbContext = services.GetService<CustomerApiContext>();
                var dbInitializer = services.GetService<IDbInitializer>();
                dbInitializer.Initialize(dbContext);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
