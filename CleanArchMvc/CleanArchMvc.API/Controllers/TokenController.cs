using Microsoft.AspNetCore.Mvc;
using CleanArchMvc.Application.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Domain.Account;
using CleanArchMvc.API.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authentication;
        private readonly IConfiguration _configuraton;

        public TokenController(IAuthenticate authentication, IConfiguration configuraton)
        {
            _authentication = authentication;
            _configuraton = configuraton;
        }

        // metodo de teste, nao deve ser usado em producao
        [HttpPost("CreateUser")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize]
        public async Task<ActionResult> CreateUser([FromBody] LoginModel userInfo)
        {
            var result = await _authentication.RegisterUser(userInfo.Email, userInfo.Password);
            
            if (result == true)
            {
                return Ok($"User {userInfo.Email} created successfully.");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid register attempt.");
                return BadRequest(ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel userInfo)
        {
            var result = await _authentication .Authenticate(userInfo.Email, userInfo.Password);

            if (result == true)
            {
                return generateToken(userInfo);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return BadRequest(ModelState);
            }
        } 

        private UserToken generateToken(LoginModel userInfo)
        {
            // declaracoes do usuario
            var claims = new[]
            {
                new Claim("email", userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // gerar chave privada para assinar o token
            var privateKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuraton["Jwt:SecretKey"]));

            // gerar assinatura
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256); 

            // definir tempo de expiracao
            var expiration = DateTime.UtcNow.AddMinutes(10);

            // gerar o token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuraton["Jwt:Issuer"],
                audience: _configuraton["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new UserToken{
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}