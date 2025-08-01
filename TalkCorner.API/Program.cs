using Microsoft.EntityFrameworkCore;
using TalkCorner.Application;
using TalkCorner.Persistence;
using TalkCorner.Persistence.DatabaseContext;

namespace TalkCorner.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddPersistenceServices(builder.Configuration);

            builder.Services.AddControllers();

            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var ctx = scope.ServiceProvider.GetRequiredService<TalkCornerDbContext>();
            ctx.Database.Migrate();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
