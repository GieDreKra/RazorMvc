using FirstMvcApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstMvcApplication.Services
{
    public class DataService
    {
        private List<PersonModel> persons = new List<PersonModel>()
        {
            new PersonModel(){ Name = "Gitanas"},
            new PersonModel(){ Name = "Dalia"}
        };
        public List<PersonModel> GetAll()
        {
            return persons;
        }
        //dependency injection in ASP.NET core
        public void Add (PersonModel personModel)
        {
            persons.Add(personModel);
        }
    }
}
