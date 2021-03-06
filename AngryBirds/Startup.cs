﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngryBirds.API.Models;
using AngryBirds.API.Resolvers;
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
            services.AddScoped<IPlayersResolver, PlayersResolver>();
            services.AddScoped<IMapsResolver, MapsResolver>();
            services.AddScoped<IRoundResolver, RoundResolver>();
            services.AddScoped<GraphQLQuery>();
            services.AddScoped(typeof(ResponseGraphType<>));
            services.AddScoped(typeof(ResponseListGraphType<>));
            services.AddTransient<PlayerType>();
            services.AddTransient<PlayerInputType>();
            services.AddTransient<RoundType>();
            services.AddTransient<RoundInputType>();
            services.AddTransient<MapType>();
            services.AddTransient<MapInputType>();
            services.AddTransient<ISchema>(
                s => new AngryBirdsSchema(new FuncDependencyResolver(type => (GraphType)s.GetService(type))));
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
                cfg.CreateMap<PlayerForManipulationDto, Player>().ReverseMap();

                cfg.CreateMap<Map, MapDto>();
                cfg.CreateMap<MapForCreationDto, Map>().ReverseMap();
                cfg.CreateMap<MapForManipulationDto, Map>().ReverseMap();

                cfg.CreateMap<Round, RoundDto>();
                cfg.CreateMap<RoundForCreationDto, Round>().ReverseMap();
                cfg.CreateMap<RoundForManipulationDto, Round>().ReverseMap();
            });

            app.UseMvc();
        }
    }
}
