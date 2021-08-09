using System;

namespace AdoDataAccessLibrary.Models
{
    public class AdoTodo
    {
        public int? id { get; set; }
        public string todo { get; set; }
        public bool? completed { get; set; }
    }
}
