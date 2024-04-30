using DesafioSMN.Dominio.Model;

namespace DesafioSMN.MVC.Helper
{
    public interface ISessao
    {
        void CriarSessaoDoFuncionario(FuncionarioModel funcionario);
        void RemoverSessaoDoFuncionario();
        FuncionarioModel BuscarSessaoDoFuncionario();
    }
}
