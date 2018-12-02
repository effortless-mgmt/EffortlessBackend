using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EffortlessApi.Extensions;
using EffortlessApi.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace EffortlessApi{
    public class Startup
    {
        public Startup (IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services)
        {
            var dbHost = Configuration["DB_HOST"] ?? "localhost";
            var dbPort = Configuration["DB_PORT"] ?? "5432";
            var dbUser = Configuration["DB_USER"] ?? "root";
            var dbPass = Configuration["DB_PASS"] ?? "root";

            var authSigningKey = Configuration["AUTH_SIGNING_KEY"] ?? "fNGxeQqjhXhRduHA";

            var connectionString = $"User ID={dbUser}; Password={dbPass}; Server={dbHost}; port={dbPort}; Database=EffortlessApi;Integrated Security=true; Pooling=true;";

            services.AddEntityFrameworkNpgsql().AddDbContext<EffortlessContext>(opt => opt.UseNpgsql(connectionString));
            services.ConfigureCors();
            services.ConfigureAuthorization(authSigningKey);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(o => 
            {
                o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env, EffortlessContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            context.Database.Migrate();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}