
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ControleGasto.Dados;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ControleGasto.Api;

[Route("api/[controller]")]
[ApiController]
public class CadastroUsuario : ControllerBase
{
    private readonly Dados.DB db;
    private readonly IConfiguration _configuration;

    public CadastroUsuario(Dados.DB DB, IConfiguration configuration)
    {
        db = DB;
        _configuration = configuration;
    }

    [HttpPost("[action]")]
    [AllowAnonymous]
    public IActionResult CriaConta([FromBody] Usuarios usuario)
    {
        if (usuario == null)
        {
            return BadRequest(new { message = "Os dados do usuário são inválidos." });
        }

        bool usuarioExiste = db.Usuarios.Any(p => p.Usuario == usuario.Usuario);

        if (usuarioExiste)
        {
            return BadRequest(new { message = "Usuário já existente." });
        }

        usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

        var novoUsuario = new Usuarios
        {
            Usuario = usuario.Usuario,
            Nome = usuario.Nome,
            Senha = usuario.Senha,
            Ativo = true
        };

        db.Usuarios.Add(novoUsuario);
        db.SaveChangesAsync();

        return Ok(new { message = "Conta criada com sucesso!" });
    }


    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] loginUsuario login)
    {
        var usuario = await db.Usuarios.FirstOrDefaultAsync(u => u.Usuario == login.Usuario);

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(login.Senha, usuario.Senha))
        {
            return BadRequest(new { message = "Usuário ou senha inválidos." });

        }

        var token = GerarToken(login.Usuario);

        return Ok(new { token = token });
    }

  
    public string GerarToken(string nomeUsuario)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


        var now = DateTime.UtcNow;
        var notBefore = now;
        var expires = now.AddHours(1);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Name, nomeUsuario)
            }),
            NotBefore = notBefore,
            Expires = expires,
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }


}

public class loginUsuario()
{
    public string Usuario { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}
