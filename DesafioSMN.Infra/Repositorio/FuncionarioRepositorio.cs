using DesafioSMN.Dominio.Model;
using DesafioSMN.Infra.Data;
using System.Collections.Generic;
using System.Linq;

namespace DesafioSMN.Infra.Repositorio
{
    public class FuncionarioRepositorio : IFuncionarioRepositorio
    {

        private readonly BancoContext _context;
        public FuncionarioRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;
        }
        public FuncionarioModel BuscarPorLogin(string email)
        {
            return _context.Funcionarios.FirstOrDefault(x => x.Email == email);
        }
        public FuncionarioModel BuscarPorId(int id)
        {
           return _context.Funcionarios.FirstOrDefault(x => x.Id == id);
        }
        public List<FuncionarioModel> BuscarTodos()
        {
            return _context.Funcionarios.ToList();
        }
        public FuncionarioModel Adicionar(FuncionarioModel funcionario)
        {
            _context.Funcionarios.Add(funcionario);
            _context.SaveChanges();
            return funcionario;
        }
        public FuncionarioModel Atualizar(FuncionarioModel funcionario)
        {

            FuncionarioModel funcionarioDB = BuscarPorId(funcionario.Id);

            if (funcionarioDB == null) throw new System.Exception("Houve um erro na atualização da tarefa!");

            funcionarioDB.Nome = funcionario.Nome;
            funcionarioDB.Email = funcionario.Email;
            funcionarioDB.Perfil = funcionario.Perfil;
            funcionarioDB.Senha = funcionario.Senha;
            funcionarioDB.Celular = funcionario.Celular;
            funcionarioDB.DataNascimento = funcionario.DataNascimento;
            funcionarioDB.Rua = funcionario.Rua;
            funcionarioDB.Numero = funcionario.Numero;
            funcionarioDB.Bairro = funcionario.Bairro;
            funcionarioDB.Cidade = funcionario.Cidade;
            funcionarioDB.Cep = funcionario.Cep;
            funcionarioDB.Gestor_FuncionarioId = funcionario.Gestor_FuncionarioId;

            _context.Funcionarios.Update(funcionarioDB);
            _context.SaveChanges();
            return funcionarioDB;
        }
        public bool Apagar(int id)
        {
            FuncionarioModel funcionarioDB = BuscarPorId(id);
            if (funcionarioDB == null) throw new System.Exception("Houve um erro na Deleção da tarefa!");

            _context.Funcionarios.Remove(funcionarioDB);
            _context.SaveChanges();
            return true;
        }

        public FuncionarioModel ListarPorId(int id)
        {
            return _context.Funcionarios.FirstOrDefault(x => x.Id == id);
        }
    }
}
