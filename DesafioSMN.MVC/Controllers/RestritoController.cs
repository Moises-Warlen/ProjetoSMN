using DesafioSMN.MVC.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DesafioSMN.MVC.Controllers
{
    [PaginaParafuncionarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
