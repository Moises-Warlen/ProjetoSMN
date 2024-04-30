using DesafioSMN.Dominio.Enums;
using DesafioSMN.Dominio.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace DesafioSMN.MVC.Filters
{
    public class PaginaRestritaSomenteAdmin : ActionFilterAttribute
    {

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string sessaoFuncionario = context.HttpContext.Session.GetString("sessaoFuncionarioLogado");

            if (string.IsNullOrEmpty(sessaoFuncionario))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "login" }, { "action", "Index" } });
            }
            else
            {
                FuncionarioModel funcionario = JsonConvert.DeserializeObject<FuncionarioModel>(sessaoFuncionario);
                if (funcionario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "login" }, { "action", "Index" } });

                }
                if(funcionario.Perfil != PerfilEmun.Admin)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Restrito" }, { "action", "Index" } });

                }
            }

            base.OnActionExecuted(context);

        }
    }
}
