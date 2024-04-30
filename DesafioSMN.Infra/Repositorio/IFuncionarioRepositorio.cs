using DesafioSMN.Dominio.Model;
using System.Collections.Generic;

namespace DesafioSMN.Infra.Repositorio
{
  public  interface IFuncionarioRepositorio
    {
        FuncionarioModel BuscarPorLogin(string  login);
        List<FuncionarioModel> BuscarTodos();
        FuncionarioModel BuscarPorId(int id);
        FuncionarioModel Adicionar(FuncionarioModel funcionario );
        FuncionarioModel Atualizar(FuncionarioModel funcionario);
        bool Apagar(int id);
    }
}
