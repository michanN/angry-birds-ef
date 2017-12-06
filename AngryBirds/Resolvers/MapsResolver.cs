using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngryBirds.API.Models;
using AngryBirds.API.Schemas;
using AngryBirds.API.Types;
using AngryBirds.CORE.Data;
using AngryBirds.CORE.Models;
using AutoMapper;
using GraphQL.Types;

namespace AngryBirds.API.Resolvers
{
    public class MapsResolver : Resolver, IMapsResolver
    {
        private readonly IPlayerRepository _playerRepository;

        public MapsResolver(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public void Resolve(GraphQLQuery graphQLQuery)
        {
            graphQLQuery.FieldAsync<ResponseGraphType<MapType>>(
                "map",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType>() { Name = "mapId" }),
                resolve: async context =>
                {
                    var id = context.GetArgument<Guid>("mapId");
                    var mapFromRepo = await _playerRepository.GetMapByIdAsync(id);                   
                    var mapToRetun = Mapper.Map<MapDto>(mapFromRepo);
                    return Response(mapToRetun);
                });

            graphQLQuery.FieldAsync<ResponseListGraphType<MapType>>(
                "maps",
                resolve: async context =>
                {
                    var mapsFromRepo = await _playerRepository.GetAllMapsAsync();
                    var mapsToReturn = Mapper.Map<IEnumerable<MapDto>>(mapsFromRepo);
                    return Response(mapsToReturn);
                });

            graphQLQuery.FieldAsync<ResponseGraphType<MapType>>(
                "createMap",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<MapInputType>> {Name = "map"}
                ),
                resolve: async context =>
                {
                    var map = context.GetArgument<MapForCreationDto>("map");
                    var mapEntity = Mapper.Map<Map>(map);
                    await _playerRepository.AddMapAsync(mapEntity);
                    var mapToReturn = Mapper.Map<MapDto>(mapEntity);
                    return Response(mapToReturn);
                });

            graphQLQuery.FieldAsync<ResponseGraphType<MapType>>(
                "updateMap",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<MapInputType>> {Name = "map"}
                ),
                resolve: async context =>
                {
                    var map = context.GetArgument<MapForManipulationDto>("map");
                    var mapEntity = Mapper.Map<Map>(map);
                    await _playerRepository.UpdateMapAsync(mapEntity);
                    var mapToReturn = Mapper.Map<MapDto>(mapEntity);
                    return Response(mapToReturn);
                });
        }
    }
}
