using System;
using Api.Data.Collections;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;


namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<Usuario> _usuariosCollection;

        public UsuarioController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _usuariosCollection = _mongoDB.DB.GetCollection<Usuario>(typeof(Usuario).Name.ToLower());

        }


        [HttpPost]
        public ActionResult SalvarUsuario([FromBody] UsuarioDto dto)
        {
            var usuario = new Usuario()
            {
                Name = dto.Name,
                AkumanNoMiName = dto.AkumanNoMiName,
                AkumaNoMiType = dto.AkumaNoMiType,
                Affiliated = dto.Affiliated
            };

            _usuariosCollection.InsertOne(usuario);
            return StatusCode(201, "Usuario Adicionado Com Sucesso");


        }

        [HttpGet]
        public ActionResult ObterUsuarios()
        {
            var usuarios = _usuariosCollection.Find(Builders<Usuario>.Filter.Empty).ToList();

            return Ok(usuarios);
        }
    }
}
