using DesafioSMN.Infra.Data;
using DesafioSMN.MVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace DesafioSMN.Infra.Repositorio
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private readonly BancoContext _bancoContext;
        public TarefaRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public TarefaModel Atualizar(TarefaModel tarefa)
        {
            TarefaModel tarefaDB = ListarPorId(tarefa.Id);

            if (tarefaDB == null) throw new System.Exception("Houve um erro na atualização da tarefa!");
           // tarefaDB.Responsavel = tarefa.Responsavel;
            tarefaDB.Descricao = tarefa.Descricao;
            //tarefaDB.DataAtribuicao = tarefa.DataAtribuicao;
            tarefaDB.DataConclusao = tarefa.DataConclusao;
            _bancoContext.Tarefas.Update(tarefaDB);
            _bancoContext.SaveChanges();
            return tarefaDB;
        }
        public bool Apagar(int id)
        {
            TarefaModel tarefaDB = ListarPorId(id);
            if (tarefaDB == null) throw new System.Exception("Houve um erro na Deleção da tarefa!");
         
            _bancoContext.Tarefas.Remove(tarefaDB);
            _bancoContext.SaveChanges();
            return true;
        }
        public TarefaModel ListarPorId(int id)
        {
            return _bancoContext.Tarefas.FirstOrDefault(x => x.Id ==id);
        }
        public List<TarefaModel> BuscarTodos()
        {
            return _bancoContext.Tarefas.ToList();
           
        }
        public TarefaModel Adicionar(TarefaModel tarefa)
        {
            _bancoContext.Tarefas.Add(tarefa);
            _bancoContext.SaveChanges();

            return tarefa;
        }
       
    }
}
