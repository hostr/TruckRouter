using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapSolver.Algorithms;
using MapSolver.Interfaces;
using MapSolver.Providers;
using MapSolver.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MapSolver
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
            // Framework services
            services.AddMvc();
            services.AddMemoryCache();

            // Application services
            services.AddScoped<ISolvingService, SolvingService>();
            services.AddScoped<INeighborProvider, VerticalHorizontalNeighborProvider>();

            services.AddScoped<ManhattanHeuristicAlgorithm>();
            services.AddScoped<PythagoreasTheoremAlgorithm>();

            services.AddScoped(factory =>
            {
                Func<string, IDistanceAlgorithm> accessor = key =>
                {
                    switch (key)
                    {
                        case "Manhattan":
                            return factory.GetService<ManhattanHeuristicAlgorithm>();
                        case "Pythagoreas":
                            return factory.GetService<PythagoreasTheoremAlgorithm>();
                        default:
                            throw new KeyNotFoundException();
                    }
                };

                return accessor;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
