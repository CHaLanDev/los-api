using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace los_api
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
            //services.AddControllers();
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase());
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            var context = app.ApplicationServices.GetService<ApiContext>();
            AddTestData(context);

            app.UseMvc();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseHttpsRedirection();

            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
        }

        private void AddTestData(ApiContext context)
        {
            var testProduct = new Product
            {
                Id = 1,
                Name = "RasberryPi4",
                ImageURL = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fth.element14.com%2Fbuy-raspberry-pi&psig=AOvVaw2OTUseO0utiCF3j1vAtZQM&ust=1625016470566000&source=images&cd=vfe&ved=0CAoQjRxqFwoTCMCfv57Yu_ECFQAAAAAdAAAAABAE",
                Price = 1000
            };
            context.Products.Add(testProduct);

            var testStock = new Stock
            {
                Id = 1,
                ProductId = 1,
                Amount = 12
            };
            context.Stocks.Add(testStock);
            context.SaveChanges();
        }
    }
}
