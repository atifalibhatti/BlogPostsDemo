using LetsPost.UserManagement.Helpers;
using LetsPost.UserManagement.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add the AuthSettings configuration section to the DI container
builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));
var autheSettings = new AuthSettings();
builder.Configuration.GetSection("AuthSettings").Bind(autheSettings);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
})
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    IdentityModelEventSource.ShowPII = true;
                    options.BackchannelHttpHandler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                    };
                    options.Audience = autheSettings.Audience;
                    options.Authority = autheSettings.Issuer;                    
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = c =>
                        {
                            throw c.Exception;
                        }
                    };

                });

// Add the TokenValidator to the DI container
builder.Services.AddScoped<ITokenValidator, TokenValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
