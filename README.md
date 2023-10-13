# Login_BlazorWebAssembly_WebApi_EF_Sql

Creación de un Login utilizando Blazor Web Assembly para el FrontEnd, Web Api ASP.Net Core con Entity Framework y Sql Server para el Backend

## Información de la aplicación

Se creó la aplicación en .Net 7, con la Configuración para Https y el ASP.NET Core Hospedado.

## Server Project

Configuraciones en la parte de Server

### NuGet

- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore
- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Microsoft.AspNetCore.Identity.UI
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools

### Conexión a la BD

- Data
  - ApplicationDbContext: para la base de datos.
- appsettings: agregamos la conexión a la BD.
- Program: para registrar la conexión a la BD y agregar la identidad del usuario al service.
