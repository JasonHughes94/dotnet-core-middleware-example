using System;
using System.Collections.Generic;
using System.Linq;
using AuthMiddlewareExample.Middleware;
using Example.Services.Interfaces.TokenValidation;
using Example.Services.TokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthMiddlewareExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            AnonymousPaths = new List<string>
            {
                "/NoAuth"
            };
        }

        public IConfiguration Configuration { get; }
        private List<string> AnonymousPaths { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<ITokenValidator, TokenValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseWhen(context => PathRequiresAuthentication(context.Request.Path),
                appBuilder => { appBuilder.UseMiddleware<AuthenticationMiddleware>(); });

            app.UseMvc();
        }

        private bool PathRequiresAuthentication(string path)
        {
            return AnonymousPaths.All(anonymousPath =>
                !path.StartsWith(anonymousPath, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}