using MySql.Data.MySqlClient;
using System.Reflection.PortableExecutable;

namespace MentoriaAPI.Utils
{
    public class Conexion
    {
        private static MySqlConnection connection;
        private static string connectionString = "";

        public Conexion()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // Construir la configuración
            IConfiguration configuration = builder.Build();

            // Leer valores de configuración
            string defaultConnection = configuration["ConnectionStrings"];
            string logLevel = configuration["Logging:LogLevel:Default"];

            connectionString = defaultConnection;
        }

        private void OpenConnection()
        {
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("Conexión abierta.");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al abrir la conexión: " + ex.Message);
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("Conexión cerrada.");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al cerrar la conexión: " + ex.Message);
            }
        }

        public string ExecuteSimpleStoreProcedure(string nombreProcedimiento, params MySqlParameter[]? parametros)
        {
            try
            {
                OpenConnection();
                using (MySqlCommand cmd = new MySqlCommand(nombreProcedimiento, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (parametros != null)
                    {
                        cmd.Parameters.AddRange(parametros);
                    }
                    cmd.ExecuteNonQuery();
                }
                CloseConnection();
                return "Procedimiento almacenado ejecutado.";
            }
            catch (MySqlException ex)
            {
                CloseConnection();
                return "Error al ejecutar el procedimiento almacenado: " + ex.Message;
            }
        }

        public MySqlDataReader ExecuteStoreProcedure(string nombreProcedimiento, params MySqlParameter[]? parametros)
        {
            MySqlDataReader reader = null;
            try
            {
                OpenConnection();
                using (MySqlCommand cmd = new MySqlCommand(nombreProcedimiento, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (parametros != null)
                    {
                        cmd.Parameters.AddRange(parametros);
                    }
                    reader = cmd.ExecuteReader();
                }
            }
            catch (MySqlException ex)
            {
                CloseConnection();
            }
            return reader;
        }
    }
}
