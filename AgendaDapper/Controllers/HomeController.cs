using AgendaDapper.Models;
using AgendaDapper.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AgendaDapper.Controllers
{
    public class HomeController(IRepository repository) : Controller
    {
        private readonly IRepository _repository = repository;

        public IActionResult Index()
        {
            return View(_repository.GetClients());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
