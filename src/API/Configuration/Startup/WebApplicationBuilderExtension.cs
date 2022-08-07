using Application.DtoModels.Models.AddGame;
using Domain.IContexts;
using Infrastructure.Persistence;
using Infrastructure.Persistence.BoardContextConfiguration;
using Infrastructure.Persistence.Configure;
using Infrastructure.Persistence.GameContextConfiguration;
using Microsoft.EntityFrameworkCore;

namespace API.Configuration.Startup
{
    public static class WebApplicationBuilderExtension
    {

        /// <summary>
        /// method for configure the services of api-rest
        /// </summary>
        /// <param name="builder">a object WebApplicationBuilder with the configuration</param>
        /// <returns>a object WebApplicationBuilder with the result</returns>
        public static WebApplicationBuilder ConfigureService(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScrutor();
            builder.Services.AddAllDbContextsPool<BaseContext>(Assembly.Load(nameof(Infrastructure)),
                                                               c => c.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddScoped<IGameContext>(provider => provider.GetRequiredService<GameContext>());
            builder.Services.AddScoped<IBoardContext>(provider => provider.GetRequiredService<BoardContext>());
            builder.Services.AddAutoMapper(typeof(MapAddGameDto).GetTypeInfo().Assembly);
            builder.Services.AddMediatR(Assembly.Load(nameof(Application)));

            return builder;
        }


        /// <summary>
        /// method for use the services of api-rest
        /// </summary>
        /// <param name="builder">a object WebApplicationBuilder with the configuration</param>
        public static void Configure(this WebApplicationBuilder builder)
            => builder.Build().Configure();
    }
}
