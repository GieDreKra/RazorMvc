using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooApp.Models;

namespace ZooApp.Services
{
    public class ZooSqlService
    {
        private SqlConnection _connection;
        public ZooSqlService(ILogger<ZooSqlService> logger, SqlConnection connection)
        {
            _connection = connection;
        }

        public List<ZooModel> GetAll()
        {
            List<ZooModel> animals = new List<ZooModel>();
            _connection.Open();
            using var command = new SqlCommand("select * from dbo.Zoo", _connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                animals.Add(new ZooModel()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    Gender = reader.GetString(3),
                    Age = reader.GetInt32(4),
                });
            }
            _connection.Close();
            return animals;
        }

        public List<SponsorModel> GetAllSponsors()
        {
            List<SponsorModel> sponsors = new List<SponsorModel>();
            _connection.Open();
            using var command = new SqlCommand("select s.Id, s.FirstName, s.LastName, s.Amount, s.ZooId from dbo.Sponsor s", _connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                sponsors.Add(new SponsorModel()
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Amount = reader.GetInt32(3),
                    ZooId = reader.GetInt32(4)
                });
            }
            _connection.Close();
            return sponsors;
        }

        public void Add(ZooModel model)
        {
            string sql = $"insert into dbo.Zoo (Name,Description,Gender,Age) values ('{model.Name}','{model.Description}','{model.Gender}',{model.Age})";
            var command = new SqlCommand(sql, _connection);
            try
            {
                _connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                _connection.Close();
            }
        }

        public void AddSponsor(SponsorModel model)
        {
            string sql = $"insert into dbo.Sponsor (FirstName,LastName,Amount,ZooId) values ('{model.FirstName}','{model.LastName}','{model.Amount}',{model.ZooId})";
            var command = new SqlCommand(sql, _connection);
            try
            {
                _connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                _connection.Close();
            }
        }

        public void Delete(int id)
        {
            string sql = $"delete from dbo.Zoo where Id={id}";
            var command = new SqlCommand(sql, _connection);
            try
            {
                _connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                _connection.Close();
            }
        }

        public void Edit(ZooModel model)
        {
            string sql = $"update dbo.Zoo set Name='{model.Name}',Description='{model.Description}',Gender='{model.Gender}',Age={model.Age} where Id={model.Id}";
            var command = new SqlCommand(sql, _connection);
            try
            {
                _connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                _connection.Close();
            }
        }

        public ZooModel Find(int id)
        {
            _connection.Open();
            using var command = new SqlCommand($"select * from dbo.Zoo where Id={id}", _connection);
            using var reader = command.ExecuteReader();
            reader.Read();
            ZooModel animal = new ZooModel
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Description = reader.GetString(2),
                Gender = reader.GetString(3),
                Age = reader.GetInt32(4)
            };
            _connection.Close();
            return animal;
        }

    }
}
