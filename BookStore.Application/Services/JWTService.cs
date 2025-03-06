using BookStore.Application.Interface;
using BookStore.Domain.Entities.Model;
using BookStore.Domain.Handler;
using BookStore.Domain.Structs;
using BookStore.Infrastructure.Contexts;
using BookStore.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services;

public class JWTService : IJWTService
{
    private readonly IConfiguration _config;
    private readonly PasswordHandler _passwordHandler;
    private readonly IUsuarioResponseRepository _userRepository;
    public JWTService(IUsuarioResponseRepository userRepository, IConfiguration config, PasswordHandler passwordHandler)
    {
        _userRepository = userRepository;
        _passwordHandler = passwordHandler;
        _config = config;
    }
    public async Task<UsuarioResponse?> Authenticate(UsuarioRequest request)
    {
        var passwordHandler = new PasswordHandler();

        if (string.IsNullOrWhiteSpace(request.NomeDeUsuario) || string.IsNullOrWhiteSpace(request.Senha))
        {
            return null;
        }

        var usuarioResult = await _userRepository.ProcurarUsuarioPorNomeRepository(request.NomeDeUsuario);

        if (!usuarioResult.IsSuccess || usuarioResult.Data is null)
        {
            return null;
        }
        var contaDeUsuario = usuarioResult.Data;
        if (!_passwordHandler.Verify(request.Senha, contaDeUsuario.Senha))
        {
            return null;
        }

        var issuer = _config["JWTConfig:Issuer"];
        var audience = _config["JWTConfig:Audience"];
        var key = _config["JWTConfig:Key"];
        var tokenValidityMins = _config.GetValue<int>("JWTConfig:TokenValidityMins");
        var tokenExpiryTime = DateTime.UtcNow.AddMinutes(tokenValidityMins);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, request.NomeDeUsuario),
                new Claim(ClaimTypes.Role, request.Cargo)
            }),
            Expires = tokenExpiryTime,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(securityToken);
        return new UsuarioResponse
        {
            TokenDeAcesso = accessToken,
            NomeDeUsuario = request.NomeDeUsuario,
            Email = request.Email,
            DataCriacao = DateTime.UtcNow,
            ExpiraEm = (int)tokenExpiryTime.Subtract(DateTime.UtcNow).TotalSeconds
        };
    }
}