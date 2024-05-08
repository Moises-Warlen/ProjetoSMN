using DesafioSMN.Dominio.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DesafioSMN.MVC.Helper
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _httpContext;

        public Sessao(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public FuncionarioModel BuscarSessaoDoFuncionario()
        {
            string sessaoFuncionario = _httpContext.HttpContext.Session.GetString("sessaoFuncionarioLogado");
        
            if (string.IsNullOrEmpty(sessaoFuncionario))return null;
            
                return JsonConvert.DeserializeObject<FuncionarioModel>(sessaoFuncionario);
        }
        public void CriarSessaoDoFuncionario(FuncionarioModel funcionario)
        {
            string valor = JsonConvert.SerializeObject(funcionario);

            _httpContext.HttpContext.Session.SetString("sessaoFuncionarioLogado", valor );
        }
        public void RemoverSessaoDoFuncionario()
        {
            _httpContext.HttpContext.Session.Remove("sessaoFuncionarioLogado");
        }
    }
}
