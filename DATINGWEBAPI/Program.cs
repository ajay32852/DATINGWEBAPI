using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;
using DATINGWEBAPI.BAL;
using DATINGWEBAPI.BAL.Hubs;
using DATINGWEBAPI.BAL.Utilities.Common;
using DATINGWEBAPI.BAL.Utilities.Extensions;
using DATINGWEBAPI.BLL.Utilities.Common.Localization;
using DATINGWEBAPI.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
Environment.SetEnvironmentVariable("BASEDIR", AppContext.BaseDirectory);
//Logger
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddLocalization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.RegisterDalDependencies(builder.Configuration);
builder.Services.RegisterBLLDependencies(builder.Configuration);
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddMvc()
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(JsonStringLocalizerFactory));
    });


builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("hi"),
    };
    options.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0]);
    options.SupportedCultures = supportedCultures;
});
builder.Services.AddResponseCaching();
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
});

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DATINGWEBAPI SYSTEM Core",
        Version = "v1",
        Description = "API for DATINGWEBAPI SYSTEM Core Application",
    });

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Paste your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
app.UseCors("AllowAll");
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
// Map SignalR Hub
app.MapHub<NotificationHub>("/notificationHub");

var supportedCultures = new[] { "en-US", "ar-EG", "de-DE", "hi" };
// For English (United States)
// For Arabic (Egypt)
// For German (Germany)
// hi for hindi
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);
app.ConfigureCustomExceptionMiddleware();
app.UseResponseCompression();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("../swagger/v1/swagger.json", "DATINGWEBAPI SYSTEM Core Application");
});

app.Run();
