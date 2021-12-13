using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListWebApp.Models;

namespace TodoListWebApp.Services
{
    public class DataService
    {
        private List<TodoModel> todos = new List<TodoModel>()
        {
            new TodoModel(){ Name = "Pirmas", Description="Todo"},
            new TodoModel(){ Name = "Antras", Description="Todo"}
        };

        public List<TodoModel> GetAll()
        {
            return todos;
        }

        public void Add(TodoModel todoModel)
        {
            todos.Add(todoModel);
        }
    }
}
