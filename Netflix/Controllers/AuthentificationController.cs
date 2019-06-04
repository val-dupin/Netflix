using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BLL;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Netflix.Helpers;

namespace Netflix.Controllers
{


    [Route("api/[controller]")]
    public class AuthentificationController : BaseController
    {
        private IServiceUser _serviceUser;
        private IConfiguration _configuration;

        public AuthentificationController(IServiceUser serviceUser, IConfiguration configuration)
        {
            _serviceUser = serviceUser;
            _configuration = configuration;
        }

        [HttpPost]
        [ValidataModel]
        public IActionResult Login([FromBody] ViewAuthentification viewAuthentification)
        {
            try
            {
                var user = _serviceUser.Login(viewAuthentification.Login, viewAuthentification.Password);
                if (user == null)
                {
                    return BuildJsonResponse(404, "L'utilisateur n'exsite pas", null, "Nom d'utilisateur ou mot de pass incorrect");
                }
                var Claims = new[]
                {
                   new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())

               };

                var symmetriSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityToken:Key"]));
                var signingCredentials = new SigningCredentials(symmetriSecurityKey, SecurityAlgorithms.HmacSha512);
                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _configuration["JwtSecurityToken:Issuer"],
                    audience: _configuration["JwtSecurityToken:Audience"],
                    claims: Claims,
                    expires: DateTime.UtcNow.AddMonths(1),
                    signingCredentials: signingCredentials
                    );
                var data = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    expiration = jwtSecurityToken.ValidTo,
                    currentuser = user

                };
                return BuildJsonResponse(200, "Authentification effectuée avec succès", data);
            }
            catch (Exception e)
            {
                return BuildJsonResponse(500, "Erreur serveur", null, e.Message);
            }
        }
    }
}