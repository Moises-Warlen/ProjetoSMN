using DesafioSMN.MVC.Models;
using System.Collections.Generic;

namespace DesafioSMN.Infra.Repositorio
{
   public   interface ITarefaRepositorio
    {
        TarefaModel ListarPorId(int id);
        List<TarefaModel> BuscarTodos(int funcionarioid);
        TarefaModel Adicionar(TarefaModel tarefa);
        TarefaModel Atualizar(TarefaModel tarefa);
        bool Apagar(int id);
    }
}
