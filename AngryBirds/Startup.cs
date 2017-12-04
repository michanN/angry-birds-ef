using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngryBirds.API.Models;
using AngryBirds.API.Services;
using AngryBirds.CORE.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AngryBirds.DATA.Mappings;
using AngryBirds.DATA.Repositories;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using AngryBirds.API.Schemas;
using AngryBirds.API.Types;
using AngryBirds.CORE.Models;
using AutoMapper;

namespace AngryBirds.API
{
    public class Startup
    {
        public static IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var connectionString = Configuration["connectionStrings:angryBirdsAPIDBConnectionString"];

            // Dependency Injections
            services.AddDbContext<AngryBirdContext>(o => o.UseSqlServer(connectionString));
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IGraphQLProcessor, GraphQLProcessor>();
            services.AddTransient<PlayerType>();
            services.AddTransient<RoundType>();
            services.AddTransient<PlayerInputType>();
            services.AddTransient<AngryBirdsQuery>();
            services.AddTransient<AngryBirdsMutation>();
            services.AddTransient<ISchema>(
                s => new AngryBirdsSchema(new FuncDependencyResolver(type => (GraphType) s.GetService(type))));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory, AngryBirdContext angryBirdContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            // Mappings between entities and dto:s
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Player, PlayerDto>();
                cfg.CreateMap<PlayerForCreationDto, Player>().ReverseMap();

                cfg.CreateMap<Map, MapDto>().ReverseMap();
                cfg.CreateMap<MapForCreationDto, Map>().ReverseMap();

                cfg.CreateMap<Round, RoundDto>().ReverseMap();
                cfg.CreateMap<RoundForCreationDto, Round>().ReverseMap();
            });

            app.UseMvc();
        }
    }
}
