using AgendaDapper.Models;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace AgendaDapper.Repositories
{
    public class StoreProcedureRepository : IRepository
    {
        private readonly IDbConnection _connection;

        // Constructor
        public StoreProcedureRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("ConexionSQLLocalDB"));
        }

        public Cliente AddClient(Cliente cliente)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@IdCliente", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Nombres", cliente.Nombres);
            parametros.Add("@Apellidos", cliente.Apellidos);
            parametros.Add("@Telefono", cliente.Telefono);
            parametros.Add("@Email", cliente.Email);
            parametros.Add("@Pais", cliente.Pais);
            /**
             * Option 1 to get the id of the new client added to the database using Dapper and a stored procedure with an output parameter and the Query method of the IDbConnection interface with the Single method of the IEnumerable interface to get the first element of the sequence or the default value if the sequence is empty or contains more than one element */
            // var id = _connection.Query<int>("sp_addclient", parametros, commandType: CommandType.StoredProcedure).Single();
            // cliente.IdCliente = id;

            /**
             * Option 2 to get the id of the new client added to the database using Dapper and a stored procedure with an output parameter and the Execute method of the IDbConnection interface with the Get method of the DynamicParameters class to get the value of the output parameter
             */
            _connection.Execute("sp_addclient", parametros, commandType: CommandType.StoredProcedure);
            cliente.IdCliente = parametros.Get<int>("@IdCliente");
            return cliente;
        }

        public void DeleteClient(int id)
        {
            _connection.Execute("sp_deleteclient", new { IdCliente = id }, commandType: CommandType.StoredProcedure);
        }

        public Cliente GetClient(int id)
        {
            return _connection.QueryFirst<Cliente>("sp_getclient", new { IdCliente = id }, commandType: CommandType.StoredProcedure);
        }

        public List<Cliente> GetClients()
        {
            return _connection.Query<Cliente>("sp_getclients", commandType: CommandType.StoredProcedure).ToList();
        }

        public Cliente UpdateClient(Cliente cliente)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@IdCliente", cliente.IdCliente);
            parametros.Add("@Nombres", cliente.Nombres);
            parametros.Add("@Apellidos", cliente.Apellidos);
            parametros.Add("@Telefono", cliente.Telefono);
            parametros.Add("@Email", cliente.Email);
            parametros.Add("@Pais", cliente.Pais);
            /**
             * Option 1 to update a client using Dapper and a stored procedure with the Execute method of the IDbConnection interface to execute a command against the connection and the Query method of the IDbConnection interface to execute a query against the connection and map the results to a strongly typed list
             */
            //_connection.Execute("sp_updateclient", parametros, commandType: CommandType.StoredProcedure);
            //return cliente;
            /**
             * Option 2 to update a client using Dapper and a stored procedure with the QueryFirst method of the IDbConnection interface to get the first element of the sequence or the default value if the sequence is empty or contains more than one element
             */
            return _connection.QueryFirst<Cliente>("sp_updateclient", parametros, commandType: CommandType.StoredProcedure);
        }
    }
}
