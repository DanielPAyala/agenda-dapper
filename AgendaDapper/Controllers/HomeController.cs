using AgendaDapper.Models;
using AgendaDapper.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AgendaDapper.Controllers
{
    public class HomeController(IRepository repository) : Controller
    {
        private readonly IRepository _repository = repository;

        [HttpGet]
        public IActionResult Index()
        {
            return View(_repository.GetClients());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Cliente());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IdCliente, Nombres, Apellidos, Telefono, Email, Pais, FechaCreacion")] Cliente client)
        {
            if (ModelState.IsValid)
            {
                _repository.AddClient(client);
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var cliente = _repository.GetClient(id.GetValueOrDefault());
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("IdCliente, Nombres, Apellidos, Telefono, Email, Pais, FechaCreacion")] Cliente client)
        {
            if (id != client.IdCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _repository.UpdateClient(client);
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
