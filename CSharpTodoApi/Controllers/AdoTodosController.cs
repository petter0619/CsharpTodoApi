using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

using System.Net.Http;
using System.Net;

using AdoDataAccessLibrary;
using AdoDataAccessLibrary.Models;
using AdoDataAccessLibrary.DataAccess;

namespace CSharpTodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoTodosController : ControllerBase
    {
        //private readonly string connectionString;
        private readonly AdoDataAccess _db;

        public AdoTodosController(IConfiguration configuration)
        {
            //this.connectionString = configuration.GetConnectionString("dbConnectionString");
            this._db = new AdoDataAccess(configuration.GetConnectionString("dbConnectionString"));
        }

        [HttpGet]
        public List<AdoTodoInstance> GetAllTodos()
        {
            /*
            var allTodos = new List<Todo>();

            // ----- DB Call -----
            var cs = connectionString;

            using var con = new SqlConnection(cs);
            con.Open();

            string sql = "SELECT * FROM dbo.Todos";
            using var cmd = new SqlCommand(sql, con);

            using SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                // rdr.Read() returns and array for each row, where each column is one item in the array (column 1 at index 1, etc).
                // Get value with: rdr.GetTYPE(index) OR rdr[index]

                //System.Diagnostics.Debug.WriteLine("{0} {1} {2}", rdr.GetInt32(0), rdr.GetString(1), rdr.GetBoolean(2));
                allTodos.Add(new Todo(rdr.GetInt32(0), rdr.GetString(1), rdr.GetBoolean(2)));
            }
            // ----- ./dbCall -----

            return allTodos;
            */
            //var db = new AdoDataAccess(connectionString);
            var allTodos = _db.GetAllTodos();
            return allTodos;
        }

        [HttpPost]
        public async Task<dynamic> CreateTodo()
        {
            string requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
            dynamic data = JsonSerializer.Deserialize<AdoTodo>(requestBody);

            /*
            // ----- DB Call -----
            var cs = connectionString;
            S
            using var con = new SqlConnection(cs);
            con.Open();

            //string sql = $"INSERT INTO dbo.Todos(todo, completed) VALUES('{data.TodoText}', 0)";
            //using var cmd = new SqlCommand(sql, con);

            using var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO dbo.Todos(todo, completed) VALUES('{data.TodoText}', 0)";

            int res = cmd.ExecuteNonQuery();
            // ----- ./dbCall -----
            */
            //var db = new AdoDataAccess(connectionString);
            _db.CreateTodo(data.todo);
            return StatusCode(204);
        }

        [HttpGet("{id}")]
        public ActionResult<AdoTodoInstance> GetSingleTodo(string id)
        {
            /*
            dynamic singleTodo = null;

            // ----- DB Call -----
            var cs = connectionString;

            using var con = new SqlConnection(cs);
            con.Open();

            string sql = $"SELECT * FROM dbo.Todos WHERE id = {id}";
            using var cmd = new SqlCommand(sql, con);

            using SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                //System.Diagnostics.Debug.WriteLine("{0} {1} {2}", rdr.GetInt32(0), rdr.GetString(1), rdr.GetBoolean(2));
                singleTodo = new Todo(rdr.GetInt32(0), rdr.GetString(1), rdr.GetBoolean(2));
            }
            // ----- ./dbCall -----

            return singleTodo;
            */
            //var db = new AdoDataAccess(connectionString);
            var singleTodo = _db.GetSingleTodo(id);
            return singleTodo;
        }

        [HttpPut("{id}")]
        public async Task<dynamic> UpdateTodo(string id)
        {
            string requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
            dynamic data = JsonSerializer.Deserialize<AdoTodo>(requestBody);

            /*
            // ----- DB Call -----
            var cs = connectionString;

            using var con = new SqlConnection(cs);
            con.Open();

            string sql = $"UPDATE dbo.Todos SET todo = '{data.TodoText}', completed = {(data.IsComplete ? 1 : 0)} WHERE id = {id}";
            using var cmd = new SqlCommand(sql, con);

            int res = cmd.ExecuteNonQuery();
            // ----- ./dbCall -----

            System.Diagnostics.Debug.WriteLine("--------- {0} --------", res);

            return Ok();
            */
            //var db = new AdoDataAccess(connectionString);
            var response = _db.UpdateTodo(id, data.todo, data.completed);
            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTodo(string id)
        {
            /*
            // ----- DB Call -----
            var cs = connectionString;

            using var con = new SqlConnection(cs);
            con.Open();

            string sql = $"DELETE FROM dbo.Todos WHERE id = {id}";
            using var cmd = new SqlCommand(sql, con);

            int res = cmd.ExecuteNonQuery();
            // ----- ./dbCall -----

            System.Diagnostics.Debug.WriteLine("--------- {0} --------", res);

            return Ok();
            */
            //var db = new AdoDataAccess(connectionString);
            var response = _db.DeleteTodo(id);
            return StatusCode(204);
        }
    }
}
