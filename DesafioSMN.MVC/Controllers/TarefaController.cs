using DesafioSMN.Dominio.Enums;
using DesafioSMN.Infra.Data;
using DesafioSMN.Infra.Repositorio;
using DesafioSMN.MVC.Filters;
using DesafioSMN.MVC.Helper;
using DesafioSMN.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DesafioSMN.MVC.Controllers
{
    [PaginaParafuncionarioLogado]
    public class TarefaController : Controller
    {

        private readonly BancoContext _context;
        private readonly ISessao _sessao;




        private readonly IFuncionarioRepositorio _funcionarioRepositorio;

        private readonly ITarefaRepositorio _tarefaRepositorio;
        public TarefaController(BancoContext context, ISessao sessao, ITarefaRepositorio tarefaRepositorio , IFuncionarioRepositorio funcionarioRepositorio)
        {

            _context = context;
            _sessao = sessao;

            _tarefaRepositorio = tarefaRepositorio;
            _funcionarioRepositorio = funcionarioRepositorio;
        }
        public IActionResult Index()
        {



            // Verifica se o funcionário está logado
            var funcionarioLogado = _sessao.BuscarSessaoDoFuncionario();
            if (funcionarioLogado == null)
            {
                // Redireciona para a página de login se o funcionário não estiver logado
                return RedirectToAction("Index", "Login");
            }




            // Verifica se o funcionário não é um administrador
            if (funcionarioLogado.Perfil != PerfilEmun.Admin)
            {
                // Obtém as tarefas do funcionário logado que não é um administrador
                var tarefa = _context.Tarefas.Where(t => t.Responsavel == funcionarioLogado.Nome).ToList();

                return View(tarefa);
            }



            // Se o funcionário for um administrador, exibe todas as tarefas
            var todasTarefas = _context.Tarefas.ToList();
            return View(todasTarefas);












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

            TarefaModel tarefa =_tarefaRepositorio.ListarPorId(id); // Suponha que você tenha uma função para obter a tarefa pelo ID
            if (tarefa != null)
            {
                // Popule a lista de funcionários apenas se a tarefa existir
                tarefa.Funcionarios = _funcionarioRepositorio.BuscarTodos(); // Suponha que você tenha uma função para obter os funcionários
                return View(tarefa);
            }
            else
            {
                return NotFound(); // Ou qualquer outra ação apropriada se a tarefa não for encontrada
            }


            //TarefaModel tarefa = _tarefaRepositorio.ListarPorId(id);
            //return View(tarefa);
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
