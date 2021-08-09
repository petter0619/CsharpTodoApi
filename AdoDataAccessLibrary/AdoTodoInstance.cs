using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoDataAccessLibrary
{
    public class AdoTodoInstance
    {
        public int id { get; set; }
        public string todo { get; set; }
        public bool completed { get; set; }

        public AdoTodoInstance(int id, string todoText, bool isComplete)
        {
            this.todo = todoText;
            this.completed = isComplete;
            this.id = id;
        }
    }
}