using DesafioSMN.Dominio.Model;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace DesafioSMN.MVC.ViewModels
{
    public class FuncionarioSemSenhaViewModel : FuncionarioModel
    {
        public FuncionarioSemSenhaViewModel( FuncionarioModel funcionario) : base(funcionario)
        {
        }
        public FuncionarioSemSenhaViewModel()
        {

        }
        public IEnumerable<FuncionarioModel> Funcionarios { get; set; }
        public IFormFile Foto { set; get; }
        public string NomeFoto { get; set; }

    }
}
