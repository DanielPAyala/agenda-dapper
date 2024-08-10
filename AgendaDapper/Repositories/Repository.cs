using AgendaDapper.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace AgendaDapper.Repositories
{
    public class Repository : IRepository
    {
        private readonly IDbConnection _connection;

        // Constructor
        public Repository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("ConexionSQLLocalDB"));
        }

        public Cliente AddClient(Cliente cliente)
        {
            var sql = "INSERT INTO Cliente (Nombres, Apellidos, Telefono, Email, Pais, FechaCreacion) VALUES (@Nombres, @Apellidos, @Telefono, @Email, @Pais, @FechaCreacion); SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = _connection.Query<int>(sql, cliente).Single();
            cliente.IdCliente = id;
            return cliente;
        }

        public void DeleteClient(int id)
        {
            var sql = "DELETE FROM Cliente WHERE IdCliente = @Id";
            _connection.Execute(sql, new { @Id = id });
        }

        public Cliente GetClient(int id)
        {
            var sql = "SELECT * FROM Cliente WHERE IdCliente = @Id";
            // return _connection.Query<Cliente>(sql, new { Id = id }).Single();
            // return _connection.Query<Cliente>(sql, new { Id = id }).FirstOrDefault();
            return _connection.QueryFirst<Cliente>(sql, new { Id = id });
        }

        public List<Cliente> GetClients()
        {
            var sql = "SELECT * FROM Cliente";
            return _connection.Query<Cliente>(sql).ToList();
        }

        public Cliente UpdateClient(Cliente cliente)
        {
            var sql = "UPDATE Cliente SET Nombres = @Nombres, Apellidos = @Apellidos, Telefono = @Telefono, Email = @Email, Pais = @Pais, FechaCreacion = @FechaCreacion WHERE IdCliente = @IdCliente";
            _connection.Execute(sql, cliente);
            return cliente;
        }
    }
}
