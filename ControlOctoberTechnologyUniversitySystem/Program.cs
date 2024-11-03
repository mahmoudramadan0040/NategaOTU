using ControlOctoberTechnologyUniversitySystem.BusinessLogic;
using ControlOctoberTechnologyUniversitySystem.Models;
using ControlOctoberTechnologyUniversitySystem.Models.Interfaces;
using ControlOctoberTechnologyUniversitySystem.Models.Repository;
using ControlOctoberTechnologyUniversitySystem.Utils.Interfaces;
using ControlOctoberTechnologyUniversitySystem.Utils.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;

//-------------------------------------------------------//
//       Create Serilog configuration                   //
//-----------------------------------------------------//



Log.Logger = new LoggerConfiguration()
       .MinimumLevel.Debug()
       .WriteTo.Console()
       .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
       .CreateLogger();
//=======================================================//

var builder = WebApplication.CreateBuilder(args);




//-------------------------------------------------------//
//       Serilog configuration                          //
//-----------------------------------------------------//
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Host.UseSerilog();
// end config serilog 


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Specify the XML documentation file path
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    // Include the XML comments in the Swagger documentation
    c.IncludeXmlComments(xmlPath);
});


//-------------------------------------------------------//
//       EntityFramework configuration                  //
//-----------------------------------------------------//
builder.Services.AddDbContext<ControlDbContext>(options => {
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:ControlDBContextConnection"]);
});

//-------------------------------------------------------//
//        Add Identity  configuration                   //
//-----------------------------------------------------//
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(o =>
{
    o.Password.RequireDigit = true;
    o.Password.RequireLowercase = false;
    o.Password.RequireUppercase = false;
    o.Password.RequireNonAlphanumeric = false;
    o.Password.RequiredLength = 4;
    o.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<ControlDbContext>()
.AddDefaultTokenProviders();
// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});
//-------------------------------------------------------//
//              activate service repos                  //
//-----------------------------------------------------//

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IStudentRepo, StudentRepo>();
builder.Services.AddScoped<ISubjectRepo, SubjectRepo>();
builder.Services.AddScoped<IManageExcelFiles, ManageExcelRepo>();
builder.Services.AddScoped<IManageImageRepo, ManageImageRepo>();
builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();
builder.Services.AddScoped<IGradeRepo, GradeRepo>();
builder.Services.AddScoped<IControllRole, ControlRole>();
builder.Services.AddScoped<IReportRepo, ReportRepo>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAny",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
    Path.Combine(builder.Environment.ContentRootPath, "Resources")),
    RequestPath = "/resources"
});
app.UseHttpsRedirection();
app.MapGet("/", async (EndpointDataSource endpointDataSource) =>
{
    var sb = new StringBuilder();
    sb.AppendLine("Available Routes:");

    foreach (var endpoint in endpointDataSource.Endpoints)
    {
        if (endpoint is RouteEndpoint routeEndpoint)
        {
            sb.AppendLine($"{routeEndpoint.DisplayName} - {routeEndpoint.RoutePattern}");
        }
    }

    return sb.ToString();
});
app.UseRouting();
app.UseAuthorization();
app.UseCors("AllowAny");
app.MapControllers();

Log.Information("===================Mapped Controllers=================");
app.Run();
public partial class Program { }