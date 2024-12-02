using System.Reflection;
using System.Text.Json;
using desafio_microondas.Application.BackgroundServices;
using desafio_microondas.Application.Behaviors;
using desafio_microondas.Application.Queries.HeatingProgramQueries;
using desafio_microondas.Infrastructure.Data;
using desafio_microondas.Infrastructure.Hubs;
using desafio_microondas.Infrastructure.Repositories.HeatingProgramRepository;
using desafio_microondas.Infrastructure.Repositories.MicrowaveRepository;
using desafio_microondas.Infrastructure.Services;
using desafio_microondas.Infrastructure.Services.MicrowaveService;
using desafio_microondas.Infrastructure.Services.TaskManagerService;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace desafio_microondas
{
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        //Controllers and Swagger
        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "MicrowaveApi", Version = "v1" });
        });

        //Database
        services.AddDbContext<MicrowaveDbContext>(options =>
        {
            options.UseSqlite(Configuration.GetConnectionString("Sqlite"));
        });

        //SignalR
        services.AddSignalR();
        
        //MediatR and AutoMapper
        services.AddAutoMapper(typeof(Startup));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        //Repositories
        services.AddTransient<IMicrowaveRepository, MicrowaveRepository>();
        services.AddTransient<IHeatingProgramRepository, HeatingProgramRepository>();
        
        //Queries
        services.AddTransient<IHeatingProgramQueries, HeatingProgramQueries>();
        
        //Services
        services.AddTransient<IMicrowaveService, MicrowaveService>();
        services.AddSingleton<IMicrowaveQueueService, MicrowaveQueueService>();
        
        //Background Services
        services.AddHostedService<ProcessMicrowaveQueue>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MicrowaveApi v1"));
        }

        app.UseExceptionHandler(options => options.Run(async context =>
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var ex = context.Features.Get<IExceptionHandlerFeature>();

            if (ex == null) return;

            if (ex.Error is ValidationException validationException)
            {
                var errors = validationException.Errors.Select(error =>
                    new { PropertyName = error.PropertyName, ErrorMessage = error.ErrorMessage });
                var json = JsonSerializer.Serialize(new { Message = ex.Error.Message, errors = errors });
                await context.Response.WriteAsync(json);
            }
            else
            {
                var errorMessage = new { Message = ex.Error.Message };
                var json = JsonSerializer.Serialize(errorMessage);
                await context.Response.WriteAsync(json);
            }
        }));

        app.UseRouting();
        app.UseStaticFiles();

        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<MicrowaveHub>("/microwaveHub");
        });
    }
}
}