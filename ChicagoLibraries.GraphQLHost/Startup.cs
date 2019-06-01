using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChicagoLibraries.Data;
using ChicagoLibraries.Data.LiteDb;
using ChicagoLibraries.GraphQL.Data;
using GraphQL;
using GraphQL.Http;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChicagoLibraries.GraphQLHost
{
    public class Startup
    {
        private readonly IHostingEnvironment env;
        private readonly IConfiguration configuration;

        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            this.env = env;
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();

            RegisterGraphQLTypes(services);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddGraphQL(_ =>
            {
                _.EnableMetrics = true;
                _.ExposeExceptions = true;
            })
            .AddUserContextBuilder(httpContext => new GraphQLUserContext { User = httpContext.User });

            services.AddSingleton<ILibraryRepository, LibraryRepository>(sp => new LibraryRepository(configuration.GetValue<string>("ConnectionStrings:LiteDb")));
        }

        private IServiceCollection RegisterGraphQLTypes(IServiceCollection services)
        {
            services.AddSingleton<LibraryQuery>();
            services.AddSingleton<LibraryType>();
            services.AddSingleton<ISchema, LibrarySchema>();

            return services;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // add http for Schema at default url /graphql
            app.UseGraphQL<ISchema>("/graphql");

            // use graphql-playground at default url /ui/playground
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions
            {
                Path = "/ui/playground"
            });
        }
    }
}
