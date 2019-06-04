using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
    public class UserController : BaseController
    {
        private readonly IServiceUser _serviceUser;
        public UserController(IServiceUser serviceUser)
        {
            _serviceUser = serviceUser;
        }
        List<User> List = new List<User>();
        [HttpGet]
        public IActionResult GetAll()
        {

            List<User> List = _serviceUser.FindAll();
            return Ok(List);
        }

        [HttpPost]

        public IActionResult Save([FromBody] User User)
        {


            try
            {
                if (_serviceUser.Add(User))
                    return BuildJsonResponse(201, "Utilisateur enregistré avec succes", User);
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