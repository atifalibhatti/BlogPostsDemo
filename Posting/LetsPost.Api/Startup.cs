using Microsoft.OpenApi.Models;
using LetsPost.Application;
using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text;
using LetsPost.Api.Settings;
using LetsPost.Api.Middleware;

namespace LetsPost.Api;
public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Startup).Assembly);
        AddCors(services);
        AddControllers(services);

        services.AddEndpointsApiExplorer();
        AddSwaggerDetails(services);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(new[] { typeof(Startup).Assembly }));

        DocumentDbSettings docDbSettings = new();
        Configuration.GetSection("DocumentDbSettings").Bind(docDbSettings);

        var connectionString = Configuration["ConnectionStrings:Default"];
        services.AddAppServices(connectionString, docDbSettings.DocConnectionString, docDbSettings.DocDatabaseName);
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors("CorsPolicy");
        UseExceptionHandler(app);

        app.UseMiddleware<RequestAuthMiddleware>();
        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        app.Run();
    }

    public static void UseExceptionHandler(WebApplication app)
    {
        app.UseExceptionHandler(eApp =>
        {
            eApp.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var errorContext = context.Features.Get<IExceptionHandlerFeature>();
                if (errorContext != null)
                {
                    var errorId = Activity.Current?.Id ?? context.TraceIdentifier;
                    var respn = JsonConvert.SerializeObject(new
                    {
                        Error = errorId,
                        Message = "There is a problem processing your request. Please try again later."
                    });
                    await context.Response.WriteAsync(respn, Encoding.UTF8);
                }
            });
        });

    }

    public static void AddSwaggerDetails(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "Lets Post",
                Version = "v1",
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization Header using Bearer Scheme."
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference =new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }

                    },Array.Empty<string>()
                }
            });
        });


    }
    private static void AddCors(IServiceCollection services)
    {
        services.AddCors(action =>
        action.AddPolicy("CorsPolicy", builder =>
             builder
             .AllowAnyMethod()
             .AllowAnyHeader()
             .AllowAnyOrigin()));

    }
    public static void AddControllers(IServiceCollection services)
    {
        services.AddControllers(
            options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
            .ConfigureApiBehaviorOptions(options => { });

    }
}
