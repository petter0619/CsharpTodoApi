using AdoDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoDataAccessLibrary.DataAccess
{
    public class AdoDataAccess
    {
        private readonly string _dbConnection;

        public AdoDataAccess(string dbConnectionString)
        {
            if (dbConnectionString == null)
            {
                throw new ArgumentNullException(nameof(dbConnectionString));
            }
            _dbConnection = dbConnectionString;
        }

        public List<AdoTodoInstance> GetAllTodos()
        {
            var allTodos = new List<AdoTodoInstance>();

            // ----- DB Call -----
            var cs = _dbConnection;

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
                allTodos.Add(new AdoTodoInstance( rdr.GetInt32(0), rdr.GetString(1), rdr.GetBoolean(2)));
            }
            // ----- ./dbCall -----

            return allTodos;
        }

        public int CreateTodo(string todoText)
        {
            // ----- DB Call -----
            var cs = _dbConnection;

            using var con = new SqlConnection(cs);
            con.Open();

            //string sql = $"INSERT INTO dbo.Todos(todo, completed) VALUES('{data.TodoText}', 0)";
            //using var cmd = new SqlCommand(sql, con);

            using var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO dbo.Todos(todo, completed) VALUES('{todoText}', 0)";

            int res = cmd.ExecuteNonQuery();
            // ----- ./dbCall -----
            return res;
        }

        public AdoTodoInstance GetSingleTodo(string id)
        {
            dynamic singleTodo = null;

            // ----- DB Call -----
            var cs = _dbConnection;

            using var con = new SqlConnection(cs);
            con.Open();

            string sql = $"SELECT * FROM dbo.Todos WHERE id = {id}";
            using var cmd = new SqlCommand(sql, con);

            using SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                //System.Diagnostics.Debug.WriteLine("{0} {1} {2}", rdr.GetInt32(0), rdr.GetString(1), rdr.GetBoolean(2));
                singleTodo = new AdoTodoInstance(rdr.GetInt32(0), rdr.GetString(1), rdr.GetBoolean(2));
            }
            // ----- ./dbCall -----

            return singleTodo;
        }

        public int UpdateTodo(string id, string todoText, bool isComplete)
        {
            // ----- DB Call -----
            var cs = _dbConnection;

            using var con = new SqlConnection(cs);
            con.Open();

            string sql = $"UPDATE dbo.Todos SET todo = '{todoText}', completed = {(isComplete ? 1 : 0)} WHERE id = {id}";
            using var cmd = new SqlCommand(sql, con);

            int res = cmd.ExecuteNonQuery();
            // ----- ./dbCall -----

            return res;
        }

        public int DeleteTodo(string id)
        {
            // ----- DB Call -----
            var cs = _dbConnection;

            using var con = new SqlConnection(cs);
            con.Open();

            string sql = $"DELETE FROM dbo.Todos WHERE id = {id}";
            using var cmd = new SqlCommand(sql, con);

            int res = cmd.ExecuteNonQuery();
            // ----- ./dbCall -----

            return res;
        }
    }
}
