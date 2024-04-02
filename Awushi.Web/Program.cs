using Awushi.Infrastructure;
using Awushi.Application;
using Awushi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Awushi.Application.Common;
using Awushi.Infrastructure.Common;
using Awushi.Web.Middldwares;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
//j
// Add services to the container.
#region Database Connection

builder.Services.AddDbContext<ApplicationDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion
builder.Services.AddCors(options =>
{
    options.AddPolicy("CustomPolicy", x=>x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =  JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        Description= @"Jwt Autherization Header Using the Bearer Scheme.
                        Enter 'Bearer' [space] and then your token in the input below.
                        Exmaple:'Bearer 123abcd"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type =ReferenceType.SecurityScheme,
                    Id="Bearer"
                },
                Scheme = "Oauth2",
                Name ="Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

#region Configuration for seeding data
static async void UpdateDatabaseAsync(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            if (context.Database.IsSqlServer())
            {
                context.Database.Migrate();
            }
            await SeedData.SeedDataAsync(context);
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex,"An Error Occured when seeding data");
        }
    }

}
#endregion

var app = builder.Build();

app.UseMiddleware<ExcepetionMiddleware>();

UpdateDatabaseAsync(app);


var serviceProvide = app.Services;
await SeedData.SeedRoles(serviceProvide);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CustomPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
