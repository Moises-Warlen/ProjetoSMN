using System.ComponentModel.DataAnnotations;

namespace DesafioSMN.Dominio.Model
{
     public class LoginModel
    {
        [Required(ErrorMessage =" digite seu email")]
        
        public string Login { get; set; }

        [Required(ErrorMessage = " digite sua senha")]
        public string Senha { get; set; }

    }
}
