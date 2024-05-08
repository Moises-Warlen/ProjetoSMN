using DesafioSMN.Dominio.Model;
using DesafioSMN.Infra.Repositorio;
using DesafioSMN.MVC.Helper;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DesafioSMN.MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;
        private readonly ISessao _sessao;
       public LoginController(IFuncionarioRepositorio funcionarioRepositorio ,
           ISessao sessao)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoDoFuncionario();
            return RedirectToAction("Index","Login");
        }

        public IActionResult Index()
        {
            // se funcionario estiver logado redirecionar para home
            if (_sessao.BuscarSessaoDoFuncionario() != null) return RedirectToAction("Index","Home");

            return View();
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FuncionarioModel funcionario = _funcionarioRepositorio.BuscarPorLogin(loginModel.Login);

                    if (funcionario != null)
                    {
                        if (funcionario.SenhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessaoDoFuncionario(funcionario);
                            return RedirectToAction("Index", "Home");
                        }

                    }

                }
                return View("Index");
            }
            catch (Exception)
            {
              return RedirectToAction("Index");
            }
        }
    }
}
