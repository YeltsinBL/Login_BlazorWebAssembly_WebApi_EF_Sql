using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LoginApplication.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LoginApplication.Server.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginController(IConfiguration configuration, SignInManager<IdentityUser> signInManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            // Verificar si las credenciales son correcta
            var result = await _signInManager.PasswordSignInAsync(login.Email!, login.Password!, false, false);

            if (!result.Succeeded) return BadRequest(new LoginResult { Successful = false, Error = "Usuario y/o contraseña son incorrectas." });
            // Agregamos la información del usuario al token
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, login.Email!)
            };
            // Obtenemos la clave de seguridad
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]!));
            // Convertimos la clave en SHA256
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // Agregamos el tiempo de expiración
            var expiry = DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtExpiryInMinutes"]));
            // Generamos el token
            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"], // Emisor
                _configuration["JwtAudience"], // Audiencian
                claims, // Información
                expires: expiry, // Fecha expiración
                signingCredentials: creds // Clave convertida
            );

            return Ok(new LoginResult { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}

