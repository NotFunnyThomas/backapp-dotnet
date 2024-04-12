using Npgsql;
using Dapper;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace BackApp.Model.Repository
{
    public class MessageRepository
    {

        public string WriteNewMessage(MessagePersist? message)
        {
            if (message == null)
            {
                return "KO";
            }
            string cs = @"User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=postgres";
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var query = "INSERT INTO message(name, price) VALUES(@name,@price)";
                var dp = new DynamicParameters();
                dp.Add("@name", message.Description, System.Data.DbType.AnsiString, System.Data.ParameterDirection.Input, 255);
                dp.Add("@price", message.CreationDate);

                int res = con.Execute(query, dp);
                if (res > 0)
                {
                    return "OK";
                }

            }
            return "KO";
        }

    }
}
