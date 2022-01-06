using EntityGraphQL.AspNet.Extensions;
using EntityGraphQL.ServiceCollectionExtensions;
using GraphQlDemo.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

ConfigureConfiguration(builder.Configuration);
ConfigureServices(builder.Services);

var app = builder.Build();

ConfigureMiddleware(app, app.Services, app.Environment);
ConfigureEndpoints(app, app.Services);

app.Run();

void ConfigureConfiguration(ConfigurationManager configuration) { }
void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddDbContext<DemoContext>(options =>
    {
        options.UseInMemoryDatabase("graphqldb");
        options.EnableSensitiveDataLogging();
    });
    services.AddGraphQLSchema<DemoContext>();
}
void ConfigureMiddleware(IApplicationBuilder app, IServiceProvider services, IWebHostEnvironment environment)
{
    if (environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseHttpsRedirection();
    app.UseAuthorization();

    using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
    {
        var context = serviceScope.ServiceProvider.GetRequiredService<DemoContext>();
        context.Database.EnsureCreated();
    }
}
void ConfigureEndpoints(IEndpointRouteBuilder app, IServiceProvider services) 
{
    app.MapControllers();
    app.MapGraphQL<DemoContext>();
}