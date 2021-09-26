using System.Linq;
using CommanderGQL.Data;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Data;

namespace CommanderGQL.GraphQL
{
    public class Query
    {
        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        // [UseProjection] should be commented after code first approach of mapping and resolving
        public IQueryable<Platform> GetPlatform([ScopedService] AppDbContext context)
        {
            return context.Platforms;    
        }

        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        // [UseProjection] should be commented after code first approach of mapping and resolving
        public IQueryable<Command> GetCommand([ScopedService] AppDbContext context)
        {
            return context.Commands;
        }
    }
}