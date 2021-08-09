using System;
using System.Collections.Generic;
using System.Linq;

namespace EFDataAccessLibrary
{
    public class EFDataAccess
    {
        private readonly string connectionString;
        private readonly tododb0619Context _context;

        public EFDataAccess(string connectionString)
        {
            this.connectionString = connectionString;
            this._context = new tododb0619Context(connectionString);
        }

        public dynamic[] GetAllTodos()
        {
            dynamic[] response = new dynamic[2];
            /*
            using (var context = new tododb0619Context(connectionString))
            {
                var allTodos = context.Todos.ToList();
                return allTodos;
            }
            */
            try
            {
                var allTodos = _context.Todos.ToList();
                response[0] = allTodos;
                return response;
            }
            catch (Exception ex)
            {
                response[1] = ex;
                return response;
            }
        }

        public dynamic[] CreateTodo(string todoText)
        {
            dynamic[] response = new dynamic[2];

            try {
                using (var context = new tododb0619Context(connectionString))
                {
                    var newTodo = new Todo
                    {
                        Todo1 = todoText,
                        Completed = false
                    };

                    context.Todos.Add(newTodo);
                    var affectedRows = context.SaveChanges();
                    System.Diagnostics.Debug.WriteLine(affectedRows);
                    response[0] = affectedRows;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response[1] = ex;
                return response;
            }
        }

        public Todo GetSingleTodo(string id)
        {
            using (var context = _context /*new tododb0619Context(connectionString)*/)
            {
                var todo = context.Todos.SingleOrDefault(c => c.Id == Convert.ToInt32(id));
                return todo;
            }
        }

        public int UpdateTodo(string id, string todoText, bool isCompleted)
        {
            using (var context = new tododb0619Context(connectionString))
            {
                var todo = context.Todos.FirstOrDefault(c => c.Id == Convert.ToInt32(id));
                todo.Todo1 = todoText;
                todo.Completed = isCompleted;
                var affectedRows = context.SaveChanges();
                System.Diagnostics.Debug.WriteLine(affectedRows);
                return affectedRows;
            }
        }

        public int DeleteTodo(string id)
        {
            using (var context = new tododb0619Context(connectionString))
            {
                var todo = context.Todos.FirstOrDefault(c => c.Id == Convert.ToInt32(id));
                context.Todos.Remove(todo);
                var affectedRows = context.SaveChanges();
                System.Diagnostics.Debug.WriteLine(affectedRows);
                return affectedRows;
            }
        }
    }
}
