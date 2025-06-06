using BHD_Prueba_API.Data;
using BHD_Prueba_API.Helpers;
using BHD_Prueba_API.Models;
using BHD_Prueba_API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BHD_Prueba_API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtHelper _jwt;
        private readonly IConfiguration _config;

        public UsersController(AppDbContext context, JwtHelper jwt, IConfiguration config)
        {
            _context = context;
            _jwt = jwt;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
        {
            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(request.Email))
                return BadRequest(new { mensaje = "Formato de correo inválido" });

            if (string.IsNullOrWhiteSpace(request.Password))
                return BadRequest(new { mensaje = "Formato de contraseña inválido" });

            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest(new { mensaje = "El nombre es obligatorio" });

            if (request.Phones == null || !request.Phones.Any())
                return BadRequest(new { mensaje = "Debe incluir al menos un número de teléfono" });

            // Validar email con regex
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(request.Email))
                return BadRequest(new { mensaje = "Formato de correo inválido" });

            // Validar contraseña con regex configurable
            var passwordRegex = new Regex(_config["PasswordRegex"] ?? "^(?=.*[A-Z])(?=.*\\d).{6,}$");
            if (!passwordRegex.IsMatch(request.Password))
                return BadRequest(new { mensaje = "Formato de contraseña inválido" });

            // Validar si el correo ya existe
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                return BadRequest(new { mensaje = "El correo ya registrado" });

            // Generar usuario
            var token = _jwt.GenerateToken(request.Email);
            var now = DateTime.UtcNow;

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                Created = now,
                Modified = now,
                LastLogin = now,
                Token = token,
                IsActive = true,
                Phones = request.Phones.Select(p => new Phone
                {
                    Number = p.Number,
                    CityCode = p.CityCode,
                    CountryCode = p.CountryCode
                }).ToList()
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                user.Id,
                user.Created,
                user.Modified,
                user.LastLogin,
                user.Token,
                user.IsActive
            });
        }

        
    }
}
