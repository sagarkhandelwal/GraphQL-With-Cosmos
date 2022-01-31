using GraphQLPOC.GraphQL;
using GraphQLPOC.Models;
using GraphQLPOC.Services;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GraphQLPOC
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            #region Connection String  
            var accountEndpoint = Configuration.GetValue<string>("CosmosDb:Account");
            var accountKey = Configuration.GetValue<string>("CosmosDb:Key");
            var dbName = Configuration.GetValue<string>("CosmosDb:DatabaseName");
            services.AddDbContext<DatabaseContext>(x => x.UseCosmos(accountEndpoint, accountKey, dbName));
            #endregion

            services.AddScoped<IEmployeeService, EmployeeService>();

            services.AddScoped<Query>();
            services.AddScoped<Mutation>();
            services.AddGraphQLServer().AddType<GraphQLTypes>()
                                        .AddQueryType<Query>()
                                        .AddMutationType<Mutation>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UsePlayground(new PlaygroundOptions
                {
                    QueryPath = "/api",
                    Path = "/playground"
                });
            }
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL("/api");
                
            });
        }
    }
}
