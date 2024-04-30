using DesafioSMN.Infra.Repositorio;
using DesafioSMN.MVC.Filters;
using DesafioSMN.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DesafioSMN.MVC.Controllers
{
    [PaginaParafuncionarioLogado]
    public class TarefaController : Controller
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;

        private readonly ITarefaRepositorio _tarefaRepositorio;
        public TarefaController(ITarefaRepositorio tarefaRepositorio , IFuncionarioRepositorio funcionarioRepositorio)
        {
            _tarefaRepositorio = tarefaRepositorio;
            _funcionarioRepositorio = funcionarioRepositorio;
        }
        public IActionResult Index()
        {

          List<TarefaModel> tarefas  = _tarefaRepositorio.BuscarTodos();
            return View(tarefas);
        }
        public IActionResult Criar()
        {

            var tarefaModel = new TarefaModel();
            tarefaModel.Funcionarios = _funcionarioRepositorio.BuscarTodos(); // Obtém todos os funcionários e atribui à propriedade Funcionarios do modelo de tarefa
            return View(tarefaModel); // Passa o modelo de tarefa para a view Criar




            //var funcionarios = _funcionarioRepositorio.BuscarTodos();

            //return View(funcionarios);
        }
        public IActionResult Editar(int id)
        {
            TarefaModel tarefa = _tarefaRepositorio.ListarPorId(id);
            return View(tarefa);
        }
        public IActionResult ApagarConfirmacao(int id)
        {
            TarefaModel tarefa = _tarefaRepositorio.ListarPorId(id);
            return View(tarefa); ;
        }
        public IActionResult Apagar(int id)
        {
            _tarefaRepositorio.Apagar(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Criar(TarefaModel tarefa)
        {
            _tarefaRepositorio.Adicionar(tarefa);
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult Alterar(TarefaModel tarefa)
        {

            _tarefaRepositorio.Atualizar(tarefa);
            return RedirectToAction("Index");
        }

       
    }
}
