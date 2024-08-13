using AgendaDapper.Models;
using System.Data.SqlClient;
using System.Data;
using Dapper.Contrib.Extensions;

namespace AgendaDapper.Repositories
{
    public class ContribRepository : IRepository
    {
        private readonly IDbConnection _connection;

        public ContribRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("ConexionSQLLocalDB"));
        }

        public Cliente AddClient(Cliente cliente)
        {
            var id = _connection.Insert(cliente);
            cliente.IdCliente = (int)id;
            return cliente;
        }

        public void DeleteClient(int id)
        {
            _connection.Delete(new Cliente { IdCliente = id });
        }

        public Cliente GetClient(int id)
        {
            return _connection.Get<Cliente>(id);
        }

        public List<Cliente> GetClients()
        {
            return _connection.GetAll<Cliente>().ToList();
        }

        public Cliente UpdateClient(Cliente cliente)
        {
            var result = _connection.Update(cliente);
            return cliente;
        }
    }
}
