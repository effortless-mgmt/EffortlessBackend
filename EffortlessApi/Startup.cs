using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EffortlessApi.Models;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EffortlessApi {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            var dbHost = Configuration["DB_HOST"] ?? "localhost";
            var dbPort = Configuration["DB_PORT"] ?? "5432";
            var dbUser = Configuration["DB_USER"] ?? "root";
            var dbPass = Configuration["DB_PASS"] ?? "root";

            var connectionString = $"User ID={dbUser}; Password={dbPass}; Server={dbHost}; port={dbPort}; Database=EffortlessApi;Integrated Security=true; Pooling=true;";

            services.AddEntityFrameworkNpgsql ().AddDbContext<EffortlessContext> (opt =>
                opt.UseNpgsql (connectionString));
            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_1);

            services.AddIdentityServer ()
                .AddInMemoryClients (new List<Client> ())
                .AddInMemoryIdentityResources (new List<IdentityResource> ())
                .AddInMemoryApiResources (new List<ApiResource> ())
                .AddTestUsers (new List<TestUser> ())
                .AddDeveloperSigningCredential ();

            services.AddAuthentication ()
                .AddJwtBearer (jwt => {
                    jwt.Authority = "http://localhost:5000";
                    jwt.RequireHttpsMetadata = false;
                    jwt.Audience = "api1";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env, EffortlessContext context) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseHsts ();
            }

            app.UseHttpsRedirection ();
            context.Database.Migrate ();
            app.UseIdentityServer ();
            app.UseMvc ();
        }
    }
}