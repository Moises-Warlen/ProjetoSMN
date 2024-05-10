using DesafioSMN.Dominio.Enums;
using DesafioSMN.Infra.Data;
using DesafioSMN.Infra.Repositorio;
using DesafioSMN.MVC.Filters;
using DesafioSMN.MVC.Helper;
using DesafioSMN.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IEmail _email;
        public TarefaController(BancoContext context, ISessao sessao, ITarefaRepositorio tarefaRepositorio , IFuncionarioRepositorio funcionarioRepositorio, IEmail email)
        {
            _context = context;
            _sessao = sessao;
            _tarefaRepositorio = tarefaRepositorio;
            _funcionarioRepositorio = funcionarioRepositorio;
            _email = email;
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
                List<TarefaModel> tarefasDoFuncionario = _context.Tarefas
                    .Where(t => t.FuncionarioId == funcionarioLogado.Id)
                    .Include(t => t.Funcionario) // Carrega o funcionário associado a cada tarefa
                    .ToList();

                return View(tarefasDoFuncionario);
            }

            // Se o funcionário for um administrador, exibe todas as tarefas
            List<TarefaModel> todasTarefas = _context.Tarefas
                .Include(t => t.Funcionario) // Carrega o funcionário associado a cada tarefa
                .ToList();
            return View(todasTarefas);

            //FuncionarioModel funcionarioLogado  = _sessao.BuscarSessaoDoFuncionario();

            List<TarefaModel> tarefas  = _tarefaRepositorio.BuscarTodos(funcionarioLogado.Id);
            return View(tarefas);
        }
        public IActionResult Criar()
        {
            var tarefaModel = new TarefaModel();
            tarefaModel.Funcionarios = _funcionarioRepositorio.BuscarTodos(); // Obtém todos os funcionários e atribui à propriedade Funcionarios do modelo de tarefa
            return View(tarefaModel); //Passa o modelo de tarefa para a view Criar
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
            try
            {
                if (ModelState.IsValid)
                {
                    var funcionarioLogado = _sessao.BuscarSessaoDoFuncionario();
                    if (funcionarioLogado != null)
                    {
                        tarefa.CriadorId = funcionarioLogado.Id;
                        _tarefaRepositorio.Adicionar(tarefa);

                        // Verifica se FuncionarioId tem valor antes de buscar
                        if (tarefa.FuncionarioId.HasValue)
                        {
                            // Converte int? para int para chamar o método ListarPorId
                            int funcionarioId = tarefa.FuncionarioId.Value;
                            var funcionarioTarefa = _funcionarioRepositorio.ListarPorId(funcionarioId);
                            if (funcionarioTarefa != null)
                            {
                                // Envio de email para o funcionário associado à tarefa
                                string assunto = "Nova Tarefa Criada";
                                string mensagem = $"Uma nova tarefa foi criada para você. Detalhes: {tarefa.Descricao}";
                                _email.Enviar(funcionarioTarefa.Email, assunto, mensagem);
                            }
                            else
                            {
                                //  não é possível encontrar o funcionário associado à tarefa
                                TempData["MensagemErro"] = "Não foi possível encontrar o funcionário associado à tarefa.";
                                return RedirectToAction("Index");
                            }
                        }

                        TempData["MensagemSucesso"] = "Tarefa cadastrada com sucesso";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["MensagemErro"] = "Não foi possível encontrar o funcionário logado.";
                        return RedirectToAction("Index");
                    }
                }
                return View(tarefa);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possível cadastrar sua tarefa, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Alterar(TarefaModel tarefa)
        {

            try
            {
                var funcionarioLogado = _sessao.BuscarSessaoDoFuncionario();
                if (funcionarioLogado != null)
                {
                    // Verifica se o editor é diferente do criador
                    if (tarefa.CriadorId != funcionarioLogado.Id)
                    {
                        // Obtém o criador da tarefa
                        var criador = _funcionarioRepositorio.ListarPorId(tarefa.CriadorId);
                        if (criador != null)
                        {
                            // Envia o e-mail para o criador da tarefa
                            string assunto = "Tarefa Concluida";
                            string mensagem = $"A tarefa que você criou foi Concluida. Detalhes: {tarefa.Descricao}";
                            _email.Enviar(criador.Email, assunto, mensagem);
                        }
                        else
                        {
                            // Não foi possível encontrar o criador da tarefa
                            TempData["MensagemErro"] = "Não foi possível encontrar o criador da tarefa.";
                            return RedirectToAction("Index");
                        }
                    }
                }
                else
                {
                    TempData["MensagemErro"] = "Não foi possível encontrar o funcionário logado.";
                    return RedirectToAction("Index");
                }

                // Atualiza a tarefa
                _tarefaRepositorio.Atualizar(tarefa);
                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possível editar sua tarefa, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }



            //_tarefaRepositorio.Atualizar(tarefa);
            //return RedirectToAction("Index");
        }
       
    }
}
