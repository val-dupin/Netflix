using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BLL;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Netflix.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ThemeController : BaseController
    {
        private readonly IServiceTheme _serviceTheme;
        public ThemeController(IServiceTheme serviceTheme)
        {
            _serviceTheme = serviceTheme;
        }
        List<Theme> List = new List<Theme>();
        [HttpGet]
        public IActionResult GetAll()
        {

            List<Theme> List = _serviceTheme.FindAll();
            return Ok(List);
        }

        [HttpPost]

        public IActionResult Save([FromBody] Theme Theme)
        {


            try
            {
                if (_serviceTheme.Add(Theme))
                    return BuildJsonResponse(201, "Utilisateur enregistré avec succes", Theme);
                else
                {
                    return BuildJsonResponse(400, "Erreur d'enregistrement");
                }
            }
            catch (Exception e)
            {

                return BuildJsonResponse(500, "Erreur serveur", null, e.Message);
            }




        }
    }
}