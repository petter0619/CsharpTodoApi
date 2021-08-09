using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
/*
using Dapper;   // https://dapper-tutorial.net/
using System.Data.SqlClient;
using System.Data;
*/
using DapperDataAccessLibrary.Models;
using DapperDataAccessLibrary.DataAccess;

namespace CSharpTodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DapperTodosController : ControllerBase
    {
        private readonly DapperContext _db;

        public DapperTodosController(IConfiguration configuration)
        {
            this._db = new DapperContext(configuration.GetConnectionString("dbConnectionString"));
        }

        [HttpGet]
        public List<DapperTodo> GetAllTodos()
        {
            /*
            var allTodos = new List<DapperTodo>();

            string sql = "SELECT * FROM dbo.Todos";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                allTodos = connection.Query<DapperTodo>(sql).ToList();
            }

            return allTodos;
            */
            var allTodos = _db.GetAllTodos();
            return allTodos;
        }

        [HttpPost]
        public async Task<dynamic> CreateTodo()
        {
            string requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
            dynamic data = JsonSerializer.Deserialize<DapperTodo>(requestBody);

            /*
            string sql = $"INSERT INTO dbo.Todos(todo, completed) VALUES(@todo, 0)";

            using (var connection = new SqlConnection(connectionString))
            {
                var affectedRows = connection.Execute(sql, new { todo = data.todo });
                System.Diagnostics.Debug.WriteLine(affectedRows);
            }
            */

            var affectedRows = _db.CreateTodo(data.todo);
            return StatusCode(204);
        }

        [HttpGet("{id}")]
        public DapperTodo GetSingleTodo(string id)
        {
            /*
            DapperTodo todo;

            string sql = "SELECT * FROM dbo.Todos WHERE id = @id";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                todo = connection.QuerySingleOrDefault<DapperTodo>(sql, new { id = id });
            }

            return todo;
            */
            var todo = _db.GetSingleTodo(id);
            return todo;
        }

        [HttpPut("{id}")]
        public async Task<dynamic> UpdateTodo(string id)
        {
            string requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
            dynamic data = JsonSerializer.Deserialize<DapperTodo>(requestBody);

            /*
            string sql = "UPDATE dbo.Todos SET todo = @todo, completed = @completed WHERE id = @id";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var affectedRows = connection.Execute(sql, new {
                    todo = data.todo,
                    id = id,
                    completed = (data.completed ? 1 : 0)
                });
                System.Diagnostics.Debug.WriteLine(affectedRows);
            }

            return Ok();
            */
            var affectedRows = _db.UpdateTodo(id, data.todo, data.completed);
            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTodo(string id)
        {
            /*
            string sql = "DELETE FROM dbo.Todos WHERE id = @id";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var affectedRows = connection.Execute(sql, new { id = id });
                System.Diagnostics.Debug.WriteLine(affectedRows);
            }

            return Ok();
            */
            var affectedRows = _db.DeleteTodo(id);
            return StatusCode(204);
        }
    }
}
