using System;
using System.Collections.Generic;
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

    public class FilmController : BaseController
    {
        private readonly IServiceFilm _serviceFilm;
        public FilmController(IServiceFilm serviceFilm)
        {
            _serviceFilm = serviceFilm;
        }
        List<Film> List = new List<Film>();
        [HttpGet]
        public IActionResult GetAll()
        {

            List<Film> List = _serviceFilm.FindAll();
            return Ok(List);
        }

        [HttpPost]

        public IActionResult Save([FromBody] Film Film)
        {


            try
            {
                if (_serviceFilm.Add(Film))
                    return BuildJsonResponse(201, "Utilisateur enregistré avec succes", Film);
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