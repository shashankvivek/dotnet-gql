using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommanderGQL.Data;
using CommanderGQL.GraphQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GraphQL.Server.Ui.Voyager;
using CommanderGQL.GraphQL.Platforms;
using CommanderGQL.GraphQL.Commands;

namespace CommanderGQL
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        
        public Startup(IConfiguration configuration,IWebHostEnvironment env)
        {
            _configuration = configuration;
             _env = env;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<AppDbContext>(opt => 
                opt.UseSqlServer(_configuration.GetConnectionString("CommandConStr")));
             services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddProjections()
                .AddType<PlatformType>()
                .AddType<CommandType>()
                .AddFiltering()
                .AddSorting()
                .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = _env.IsDevelopment()); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });

            app.UseGraphQLVoyager(new GraphQLVoyagerOptions()
            {
                GraphQLEndPoint = "/graphql",
                Path = "/graphql-voyager"
            });
        }
    }
}
