using Audit.Core;
using Core.Persistence.Audit;
using Core.Application.Extensions;
using Core.Infrastructure.Extensions;
using Core.Persistence.Extensions;
using Core.WebAPI.Utility;
using Core.Application.Interfaces.Repositories;
using Core.WebAPI.Services;
using JwtTokenAuthentication.Permission;
using Microsoft.AspNetCore.Authorization;
using JwtTokenAuthentication.Extensions;
using Microsoft.OpenApi.Models;
using JwtTokenAuthentication.Services;
using Asp.Versioning;
using System.Reflection;
using Core.Infrastructure.Hubs;
using JwtTokenAuthentication.Constants;
using System.IdentityModel.Tokens.Jwt;
using Core.WebAPI.CustomMiddleware;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationLayer();

builder.Services.AddJwtAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddInfrastructureLayer();
builder.Services.AddPersistenceLayer(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        // Or use .Preserve to preserve object references
        // options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Preserve;
    });
builder.Services.AddEndpointsApiExplorer();
// CORS Access Config
string[] allowedOrigins = builder.Environment.IsDevelopment()
    ? new[] { "http://localhost:4200", "https://localhost:7033", "https://localhost:5002" }
    : new[] { "https://yourproductiondomain.com" };
/*builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
          builder => builder.WithOrigins(allowedOrigins)// single domain .WithOrigins("https://www.yogihosting.com")
                            .AllowAnyMethod()  // multiple domain .WithOrigins(new string[] { "https://www.yogihosting.com", "https://example1.com", "https://example2.com" })
                            .AllowAnyHeader().SetIsOriginAllowed(origin => true)); // allow any origin;
});*/
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
          builder => builder.WithOrigins(allowedOrigins)// single domain .WithOrigins("https://www.yogihosting.com")
                            .AllowAnyMethod()  // multiple domain .WithOrigins(new string[] { "https://www.yogihosting.com", "https://example1.com", "https://example2.com" })
                            .AllowAnyHeader()
                            .AllowCredentials()  // Allow credentials to be included
                             .SetIsOriginAllowedToAllowWildcardSubdomains()
                            .SetIsOriginAllowed(origin => true)); // allow any origin;
});
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1,0);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Dayaratne ERP Core API",
        Description = "A Dayaratne ERP Core API Version 1",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Kokila Sanjeewa",
            Email = string.Empty,
            Url = new Uri("https://www.facebook.com/kokila.sanjeewa"),
        },

        License = new OpenApiLicense
        {
            Name = "Use under LICX",
            Url = new Uri("https://example.com/license"),
        }
    });
    // add basic instead of Bearer
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,// add basic instead of Bearer SecuritySchemeType.Http
        Scheme = "Bearer",        // add basic instead of Bearer
        BearerFormat = "JWT",          // add basic instead of Bearer remove this line
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
               {
             {
               new OpenApiSecurityScheme
               {
                   Reference = new OpenApiReference
                         {
                                     Type = ReferenceType.SecurityScheme,
                                     Id = "Bearer" // add basic instead of Bearer
                         }
               },
                 new string[] {}
             }
               });
    //Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
       var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
       c.IncludeXmlComments(xmlPath);
});
builder.Services.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, MultiplePermissionAuthorizationHandler>();
builder.Services.AddScoped<TokenService, TokenService>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();

//https://github.com/thepirat000/Audit.NET/blob/master/src/Audit.EntityFramework/README.md#install
// Register IHttpContextAccessor
builder.Services.AddHttpContextAccessor();
var httpContextAccessor = builder.Services.BuildServiceProvider().GetRequiredService<IHttpContextAccessor>();
// Configure Audit.NET globally
Audit.Core.Configuration.Setup()
    .UseEntityFramework(_ => _
        .AuditTypeMapper(t => typeof(AuditTrail))
        .AuditEntityAction<AuditTrail>((ev, entry, entity) =>
        {
            int userSerialID = 0;
            long loginLogSerialID = 0;
            var authHeader = httpContextAccessor?.HttpContext?.Request?.Headers["Authorization"].FirstOrDefault();
            var token = !string.IsNullOrWhiteSpace(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
                ? authHeader.Substring("Bearer ".Length).Trim()
                : null;
            //  var token = httpContextAccessor.HttpContext.Request?.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!string.IsNullOrEmpty(token))
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                userSerialID = Convert.ToInt32(jwt.Claims.First(c => c.Type == "userSerialID").Value);
                loginLogSerialID = Convert.ToInt64(jwt.Claims.First(c => c.Type == "loginLogSerialID").Value);
            }

            //   var mnuHeader = httpContextAccessor.HttpContext.Request?.Headers["MnuSerialID"].FirstOrDefault();
            //    int.TryParse(mnuHeader, out var mnuSerialID);

            var mnuHeader = httpContextAccessor?.HttpContext?.Request?.Headers["MnuSerialID"].FirstOrDefault();

            int mnuSerialID = 0;
            if (!string.IsNullOrWhiteSpace(mnuHeader))
            {
                int.TryParse(mnuHeader, out mnuSerialID);
            }
            entity.Action = entry.Action;
            entity.AuditData = Helper.AuditData(entry);
            entity.MnuSerialID = mnuSerialID;
            entity.LoginLogSerialID = loginLogSerialID;
            entity.TableName = entry.EntityType.Name;
            entity.AuditDateTimeUtc = DateTime.Now;
            entity.MachineName = Environment.MachineName;
            entity.UserSerialID = userSerialID;
        })
        .IgnoreMatchedProperties(true));
var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseHttpsRedirection();
app.UseRouting();              //do not change the order between UseAuthentication and UseAuthorization()
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseMiddleware<SessionValidationMiddleware>(); // Validates session based on the current session ID
app.UseAuthorization();
// Map SignalR Hub
app.MapHub<NotificationHub>("/notificationHub"); // Make sure this comes after authentication
app.MapControllers();

//CheckDuplicatePermissions(); // enable to check duplicate menu

app.Run();



