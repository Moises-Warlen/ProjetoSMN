using DesafioSMN.Dominio.Model;
using DesafioSMN.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioSMN.Infra.Data
{
   public class BancoContext :DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {

        }
        public DbSet<TarefaModel> Tarefas { get; set; }
        public DbSet<FuncionarioModel> Funcionarios { get; set; }
    }
}
