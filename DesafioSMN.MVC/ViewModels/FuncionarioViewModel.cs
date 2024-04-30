using DesafioSMN.Dominio.Model;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace DesafioSMN.MVC.ViewModels
{
    public class FuncionarioViewModel : FuncionarioModel
    {
        public FuncionarioViewModel( FuncionarioModel funcionario) : base(funcionario)
        {
        }
        public FuncionarioViewModel()
        {

        }
        public IEnumerable<FuncionarioModel> Funcionarios { get; set; }
        public IFormFile Foto { set; get; }
        public string NomeFoto { get; set; }

    }
}
