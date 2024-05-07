using DesafioSMN.Dominio.Model;
using System;
using System.Collections.Generic;

namespace DesafioSMN.MVC.Models
{
    public class TarefaModel
    {
        public int Id { get; set; }
        public string Responsavel { get; set; }
        public string Descricao { get; set; }
        public DateTime DataAtribuicao { get; set; } = DateTime.Now;
        public DateTime? DataConclusao { get; set; }
        public List<FuncionarioModel> Funcionarios { get; set; }
        public int? FuncionarioId { get; set; } // relacionamento com  funcionario
        public FuncionarioModel Funcionario { get; set; }// relacinamento com  funcionario

    }
}
