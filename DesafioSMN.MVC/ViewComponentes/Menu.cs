using DesafioSMN.Dominio.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace DesafioSMN.MVC.ViewComponentes
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            string sessaoFuncionario = HttpContext.Session.GetString("sessaoFuncionarioLogado");

            if (string.IsNullOrEmpty(sessaoFuncionario)) return null;

            FuncionarioModel funcionario = JsonConvert.DeserializeObject<FuncionarioModel>(sessaoFuncionario);

            return View(funcionario);
        }
    }
}
