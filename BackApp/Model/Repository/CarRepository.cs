using Npgsql;
using Dapper;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace BackApp.Model.Repository
{
    public class CarRepository
    {

        public string Execute(Car newInstance)
        {
            string cs = @"User ID=postgres;Password=postgres;Host=192.168.5.16;Port=5432;Database=postgres";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            //var ver = con.ExecuteScalar<string>("SELECT version()");

            var query = "INSERT INTO cars(name, price) VALUES(@name,@price)";
            var dp = new DynamicParameters();
            dp.Add("@name", newInstance.Name, System.Data.DbType.AnsiString, System.Data.ParameterDirection.Input, 255);
            dp.Add("@price", newInstance.Price);

            int res = con.Execute(query, dp);
            con.Close();

            if (res > 0) {
                return "OK";
            }
            else
            {
                return "KO";
            }
          

        }
        public string Execute()
        {
            string cs = @"User ID=postgres;Password=postgres;Host=192.168.5.16;Port=5432;Database=postgres";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            //var ver = con.ExecuteScalar<string>("SELECT version()");

            var cars = con.Query<Car>("SELECT * FROM cars").ToList();
            StringBuilder sb = new StringBuilder();

            cars.ForEach(car => sb.AppendLine($"Id : {car.Id} Name : {car.Name} Prix : {car.Price}"));
            
            con.Close();

            return sb.ToString();
            //return ver.ToString();
        }
    }
}
