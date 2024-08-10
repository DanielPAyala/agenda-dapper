using AgendaDapper.Models;

namespace AgendaDapper.Repositories
{
    public interface IRepository
    {
        Cliente GetClient(int id);
        List<Cliente> GetClients();
        Cliente AddClient(Cliente cliente);
        Cliente UpdateClient(Cliente cliente);
        void DeleteClient(int id);
    }
}
