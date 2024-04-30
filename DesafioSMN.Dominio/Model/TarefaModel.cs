using System;

namespace DesafioSMN.MVC.Models
{
    public class TarefaModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataAtribuicao { get; set; } = DateTime.Now;
        public DateTime? DataConclusao { get; set; }

    }
}
