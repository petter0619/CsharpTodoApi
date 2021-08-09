using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EFDataAccessLibrary;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace CSharpTodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFTodosController : ControllerBase
    {
        private readonly EFDataAccess _db;

        public EFTodosController(IConfiguration configuration)
        {
            this._db = new EFDataAccess(configuration.GetConnectionString("dbConnectionString"));
        }

        [HttpGet]
        public ActionResult<List<Todo>> GetAllTodos()
        {
            var response = _db.GetAllTodos();

            return response[1] == null ? response[0] : StatusCode(409);
        }

        
        [HttpPost]
        public async Task<StatusCodeResult> CreateTodo()
        {
            // Body value names: Todo1, Completed
            string requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
            dynamic data = JsonSerializer.Deserialize<Todo>(requestBody);

            if (data.Todo1 == null) return StatusCode(400);

            var response = _db.CreateTodo(data.Todo1);

            return (response[0] != null && response[0] > 0)
                ? StatusCode(201) // 201 = Created (success status response code indicates that the request has succeeded and has led to the creation of a resource)
                : StatusCode(409); // 409 = Conflict (the request could not be completed due to a conflict with the current state of the target resource. This code is used in situations where the user might be able to resolve the conflict and resubmit the request.)
        }

        [HttpGet("{id}")]
        public Todo GetSingleTodo(string id)
        {
            var response = _db.GetSingleTodo(id);
            return response;
        }

        [HttpPut("{id}")]
        public async Task<StatusCodeResult> UpdateTodo(string id)
        {
            // Body value names: Todo1, Completed
            string requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
            dynamic data = JsonSerializer.Deserialize<Todo>(requestBody);

            var affectedRows = _db.UpdateTodo(id, data.Todo1, data.Completed);

            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTodo(string id)
        {
            var response = _db.DeleteTodo(id);
            return response < 1
                ? null
                : StatusCode(204);

            // A successful response of DELETE requests SHOULD be HTTP response code 200 (OK)
            // if the response includes an entity describing the status, 202 (Accepted) if the
            // action has been queued, or 204 (No Content) if the action has been performed but
            // the response does not include an entity.
        }
    }
}
