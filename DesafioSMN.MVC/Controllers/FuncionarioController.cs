using DesafioSMN.Dominio.Model;
using DesafioSMN.Infra.Repositorio;
using DesafioSMN.MVC.Filters;
using DesafioSMN.MVC.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

namespace DesafioSMN.MVC.Controllers
{
    [PaginaRestritaSomenteAdmin]
    public class FuncionarioController : Controller
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public FuncionarioController( IFuncionarioRepositorio funcionarioRepositorio , IWebHostEnvironment hostingEnvironment)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            var funcionarios = _funcionarioRepositorio.BuscarTodos();
            var model = new List<FuncionarioViewModel>();
            var imagens = Path.Combine(_hostingEnvironment.WebRootPath, "imagens");
            var dInfo = new DirectoryInfo(imagens);

            foreach (var funcionario in funcionarios)
            {
                var fInfo = dInfo.GetFiles($"{funcionario.Id}.*");
                var funcionarioViewModel = new FuncionarioViewModel(funcionario);
                if (fInfo.Length > 0)
                {
                    var extensao = Path.GetExtension(fInfo[0].FullName);
                    funcionarioViewModel.NomeFoto = $"{funcionario.Id}{extensao}";
                }
                model.Add(funcionarioViewModel);
            }

            return View(model);
        }
        public IActionResult Criar()
        {
            var funcionarios = _funcionarioRepositorio.BuscarTodos();
            var model = new FuncionarioViewModel
            {
                Funcionarios = funcionarios
            };
            return View(model);
        }
        public IActionResult Apagar(int id)
        {
            _funcionarioRepositorio.Apagar(id);
            return RedirectToAction("Index");
        }


        public IActionResult Criar(FuncionarioViewModel funcionarioViewModel)
        {
            var funcionario = new FuncionarioModel
            {
                Nome = funcionarioViewModel.Nome,
                // Atribuir outras propriedades necessárias
            };

            var funcionarioIncluido = _funcionarioRepositorio.Adicionar(funcionario);

            if (funcionarioViewModel.Foto != null)
            {
                var imagens = Path.Combine(_hostingEnvironment.WebRootPath, "imagens");
                var caminho = Path.Combine(imagens, $"{funcionarioIncluido.Id}{Path.GetExtension(funcionarioViewModel.Foto.FileName)}");
                funcionarioViewModel.Foto.CopyTo(new FileStream(caminho, FileMode.Create));
            }

            return RedirectToAction("Index");
        }

        //public IActionResult Criar(FuncionarioViewModel funcionario )
        //{
        //    var funcionarioIncluido = _funcionarioRepositorio.Adicionar(funcionario);
        //    var imagens = Path.Combine(_hostingEnvironment.WebRootPath, "imagens");
        //    var caminho = Path.Combine(imagens, $"{funcionarioIncluido.Id}{Path.GetExtension(funcionario.Foto.FileName)}");
        //    funcionario.Foto.CopyTo(new FileStream (caminho, FileMode.Create));
        //    return RedirectToAction("Index");

        //}

        public IActionResult Editar(int id)
        {

            var funcionarios = _funcionarioRepositorio.BuscarTodos();
            FuncionarioModel funcionario = _funcionarioRepositorio.BuscarPorId(id);
            var funcionarioViewModel = new FuncionarioViewModel
            {
                Id = funcionario.Id,
                Nome = funcionario.Nome,
                // Adicione outras propriedades conforme necessário
                Funcionarios = funcionarios
            };
            return View(funcionarioViewModel);
            //FuncionarioModel funcionario = _funcionarioRepositorio.BuscarPorId(id);
            //return View(funcionario);
        }

        [HttpPost]
        public IActionResult Alterar(FuncionarioViewModel funcionario)
        {

            _funcionarioRepositorio.Atualizar(funcionario);
            return RedirectToAction("Index");
        }
        public IActionResult ApagarConfirmacao(int id)
        {
            FuncionarioModel funcionario = _funcionarioRepositorio.BuscarPorId(id);
            return View(funcionario); ;
        }

    }
}
