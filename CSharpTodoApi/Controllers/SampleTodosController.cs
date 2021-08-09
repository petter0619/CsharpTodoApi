using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
//using Newtonsoft.Json;

namespace CSharpTodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleTodosController : ControllerBase
    {
        public static List<SampleTodo> SampleTodos = new List<SampleTodo> {
            new SampleTodo("incomplete todo", false),
            new SampleTodo("complete todo", true)
        };

        [HttpGet()]
        public List<SampleTodo> GetAllSampleTodos()
        {
            return SampleTodos;
        }

        [HttpPost()]
        public async Task<dynamic> CreateSampleTodo()
        {
            string requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject<Todo>(requestBody);
            dynamic data = JsonSerializer.Deserialize<SampleTodo>(requestBody);

            var newTodo = new SampleTodo(data.TodoText, false);
            SampleTodos.Add(newTodo);
            return Ok();
        }

        [HttpGet("{id}")]
        public SampleTodo GetSingleTodo(Guid id)
        {
            SampleTodo singleTodo = SampleTodos.Find(todo => todo.Id == id);
            return singleTodo;
        }

        [HttpPut("{id}")]
        public async Task<dynamic> UpdateTodo(Guid id)
        {
            string requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject<Todo>(requestBody);
            dynamic data = JsonSerializer.Deserialize<SampleTodo>(requestBody);

            SampleTodo todoToUpdate = SampleTodos.Find(todo => todo.Id == id);
            todoToUpdate.TodoText = data.TodoText;
            todoToUpdate.IsComplete = data.IsComplete;

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTodo(Guid id)
        {
            int todoIndex = SampleTodos.FindIndex(todo => todo.Id == id);
            SampleTodos.RemoveAt(todoIndex);
            return Ok();
        }
    }
}
