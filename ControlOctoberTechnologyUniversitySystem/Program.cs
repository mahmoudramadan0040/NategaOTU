using ControlOctoberTechnologyUniversitySystem.Models;
using ControlOctoberTechnologyUniversitySystem.Models.Interfaces;
using ControlOctoberTechnologyUniversitySystem.Models.Repository;
using ControlOctoberTechnologyUniversitySystem.Utils.Interfaces;
using ControlOctoberTechnologyUniversitySystem.Utils.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

//-------------------------------------------------------//
//              activate service repos                  //
//-----------------------------------------------------//

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IStudentRepo, StudentRepo>();
builder.Services.AddScoped<ISubjectRepo, SubjectRepo>();
builder.Services.AddScoped<IManageExcelFiles, ManageExcelRepo>();
builder.Services.AddScoped<IManageImageRepo, ManageImageRepo>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
