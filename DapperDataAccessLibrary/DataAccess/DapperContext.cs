using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;   // https://dapper-tutorial.net/
using System.Data.SqlClient;
using System.Data;
using DapperDataAccessLibrary.Models;

namespace DapperDataAccessLibrary.DataAccess
{
    public class DapperContext
    {
        private readonly string connectionString;

        public DapperContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<DapperTodo> GetAllTodos()
        {
            var allTodos = new List<DapperTodo>();

            string sql = "SELECT * FROM dbo.Todos";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                allTodos = connection.Query<DapperTodo>(sql).ToList();
            }

            return allTodos;
        }

        public int CreateTodo(string todo)
        {
            string sql = $"INSERT INTO dbo.Todos(todo, completed) VALUES(@todo, 0)";

            using (var connection = new SqlConnection(connectionString))
            {
                var affectedRows = connection.Execute(sql, new { todo = todo });
                System.Diagnostics.Debug.WriteLine(affectedRows);
                return affectedRows;
            }
        }

        public DapperTodo GetSingleTodo(string todoId)
        {
            DapperTodo todo;

            string sql = "SELECT * FROM dbo.Todos WHERE id = @id";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                todo = connection.QuerySingleOrDefault<DapperTodo>(sql, new { id = todoId });
            }

            return todo;
        }

        public int UpdateTodo(string todoId, string todoText, bool isCompleted)
        {
            string sql = "UPDATE dbo.Todos SET todo = @todo, completed = @completed WHERE id = @id";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var affectedRows = connection.Execute(sql, new
                {
                    todo = todoText,
                    id = todoId,
                    completed = (isCompleted ? 1 : 0)
                });
                System.Diagnostics.Debug.WriteLine(affectedRows);
                return affectedRows;
            }
        }

        public int DeleteTodo(string todoId)
        {
            string sql = "DELETE FROM dbo.Todos WHERE id = @id";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var affectedRows = connection.Execute(sql, new { id = todoId });
                System.Diagnostics.Debug.WriteLine(affectedRows);
                return affectedRows;
            }
        }
    }
}
