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
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Swashbuckle.AspNetCore

### Conexión a la BD

- Data
  - ApplicationDbContext: para la base de datos.
- appsettings: agregamos la conexión a la BD.
- Program: para registrar la conexión a la BD y agregar la identidad del usuario al service.
- Realizamos la migración a la BD tomando de referencia el `IdentityDbContext`.

```sh
dotnet ef migrations add InitialCreate -o Data/Migrations
dotnet ef database update
```

> Nota: Como se esta utilizando Docker para la BD, en la conexión del appsettings, debe de incluir el `TrustServerCertificate=True;`.

### Configuración de JWT

- Program: se registró y configuró el JWT en el service del builder.
  - Se habilitó el uso de Autenticación y Autorización.

### Configuracion de Controllers

- AccountsController: registar al usuario.
- LoginController: iniciar sesión.
- WeatherForecastController: agregar una autorización por roles.
- Program: se configuró para utilizar la interfaz de Swagger.

## Shared Project

- Clases
  - RegisterModel: modelo que recibira el controller.
  - RegisteResult: respuesta del controller.
  - LoginModel: credenciales del login.
  - LoginResult: respuesta del controller

## Client Project

### NuGets

- Microsoft.AspNetCore.Components.Authorization
- Blazored.LocalStorage

### Configuraciones

- Helper
  - ApiAuthenticationStateProvider
    - Verificar si existe el token del usuario para agregarlo a la petición.
    - Notificar si el usuario esta autenticado o ha cerrado sesión.
- Service
  - IAuthService: interfaz del servicio.
  - AuthService: clase hererdada de la interfaz IAuthService y contiene la logica para registrarse, iniciar y cerrar sesión.
- Pages
  - Login: formulario para iniciar sesión.
  - LoginDisplay: formulario para mostrar las opciones de sesión.
  - Logout: formulario para cerrar sesión.
  - Register: formulario para registrar nuestra cuenta.
  - Counter: mostrar el formulario al usuario autenticado y mensajes de su tipo de rol.
  - FetchData: agregar lógica si el usuario esta autenticado y pertenece al rol de la api para mostrar información.
- Shared
  - MainLayout: se agrega el LoginDisplay para mostrar las opciones.
- App: agregamos el componente de autorización.
- Program: agregamos los archivos creados y servicios utilizados.
