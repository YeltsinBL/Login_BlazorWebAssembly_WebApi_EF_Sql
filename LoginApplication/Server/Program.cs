using System.Text;
using LoginApplication.Server.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Usar la interfaz de usuario Swagger como interfaz creada por la Api
builder.Services.AddSwaggerGen();

// Registramos la conexión a la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection"));
});
// Agregamos la identidad del usuario y el DBContex al Services
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>() // Agregamos los roles del usuario
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Registramos y configuramos el JWT
var securityKey = builder.Configuration["JwtSecurityKey"]; // obtenermos la clave secreta
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, // validar quién solicita el token
            ValidateAudience = true, // validar desde dónde solicita el token
            ValidateLifetime = true, // validar tiempo de vida del token 
            ValidateIssuerSigningKey = true, // validar el usuario
            ValidIssuer = builder.Configuration["JwtIssuer"], // desde donde solicita el token
            ValidAudience = builder.Configuration["JwtAudience"], // desde donde solicita el token
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(securityKey!)) // credenciales del token
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

// configuracion del Middleware para usar el Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

