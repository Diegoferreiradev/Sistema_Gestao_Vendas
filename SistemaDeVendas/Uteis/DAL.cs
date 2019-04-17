using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SistemaDeVendas.Uteis
{
    public class DAL
    {
        private static string Server = "DESKTOP-CUP9PM2";
        private static string Database = "sistema_venda";
        private static string User = "sa";
        private static int Password = 1234;
        private static string connectionString = $"Server={Server}; Database={Database}; User Id={User}; Password={Password}";
        private static SqlConnection Connection;

        public DAL()
        {
            Connection = new SqlConnection(connectionString);
            Connection.Open();
        }

        public DataTable RetDataTable(string sql)
        {
            DataTable data = new DataTable();
            SqlCommand command = new SqlCommand(sql, Connection);
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(data);
            return data;
        }

        public DataTable RetDataTable(SqlCommand command)
        {
            DataTable data = new DataTable();
            command.Connection = Connection;
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(data);
            return data;
        }

        public void ExecutarComandoSQL(string sql)
        {
            SqlCommand command = new SqlCommand(sql, Connection);
            command.ExecuteNonQuery();
        }
    }
}
