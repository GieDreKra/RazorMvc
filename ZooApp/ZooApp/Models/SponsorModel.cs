using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooApp.Models
{
    public class SponsorModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Amount { get; set; }

        [DisplayName("Animal")]
        public int ZooId { get; set; }

        [NotMapped]
        public SelectList AnimalList { get; set; }
    }

}
