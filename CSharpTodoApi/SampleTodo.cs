using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpTodoApi
{
    public class SampleTodo
    {
        public string TodoText { get; set; }
        public bool IsComplete { get; set; }

        public Guid Id { get; set; }

        public SampleTodo(string todoText, bool isComplete)
        {
            this.TodoText = todoText;
            this.IsComplete = isComplete;
            this.Id = Guid.NewGuid();
        }
    }
}
