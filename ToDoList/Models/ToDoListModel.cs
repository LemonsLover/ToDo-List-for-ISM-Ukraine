using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public class ToDoListModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsDone { get; set; }
    }
}
