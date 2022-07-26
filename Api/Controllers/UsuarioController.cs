using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select idusuario as ""idusuario"",
                        nomeusuario as ""nomeusuario""
                from usuario
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DataBase");
            NpgsqlDataReader myreader;
            using(NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using(NpgsqlCommand mycommand = new NpgsqlCommand(query, mycon))
                {
                    myreader = mycommand.ExecuteReader();
                    table.Load(myreader);

                    myreader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Usuario usuario)
        {
            string query = @"
                insert into usuario(nomeusuario)
                values(@nomeusuario)
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DataBase");
            NpgsqlDataReader myreader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand mycommand = new NpgsqlCommand(query, mycon))
                {
                    mycommand.Parameters.AddWithValue("@nomeusuario", usuario.nomeusuario);
                    myreader = mycommand.ExecuteReader();
                    table.Load(myreader);

                    myreader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Adicionado com sucesso!");
        }

        [HttpPut]
        public JsonResult Put(Usuario usuario)
        {
            string query = @"
                update usuario
                set nomeusuario = @nomeusuario
                where idusuario = @idusuario
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DataBase");
            NpgsqlDataReader myreader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand mycommand = new NpgsqlCommand(query, mycon))
                {
                    mycommand.Parameters.AddWithValue("@idusuario", usuario.idusuario);
                    mycommand.Parameters.AddWithValue("@nomeusuario", usuario.nomeusuario);
                    myreader = mycommand.ExecuteReader();
                    table.Load(myreader);

                    myreader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Atualizado com sucesso!");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                delete from usuario
                where idusuario = @idusuario
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DataBase");
            NpgsqlDataReader myreader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand mycommand = new NpgsqlCommand(query, mycon))
                {
                    mycommand.Parameters.AddWithValue("@idusuario", id);
                    myreader = mycommand.ExecuteReader();
                    table.Load(myreader);

                    myreader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Deletado com sucesso!");
        }
    }
}
