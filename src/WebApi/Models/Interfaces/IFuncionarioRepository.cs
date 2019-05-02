using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public interface IFuncionarioRepository
    {
        /// <summary>
        /// Recupera todos os funcionários salvos no DB.
        /// </summary>
        /// <returns>Lista de Funcionários</returns>
        List<Funcionario> GetAllFuncionarios();

        /// <summary>
        /// Recupera funcionário salvo no banco através do CPF informado.
        /// </summary>
        /// <param name="CPF">CPF</param>
        /// <returns>Funcionário</returns>
        FuncionarioView GetFuncionario(string CPF);

        /// <summary>
        /// Registra novo funcionário no banco de dados.
        /// </summary>
        /// <param name="funcionario">Funcionário</param>
        /// <param name="Photo">Array de bytes da Imagem</param>
        /// <param name="fileName">Nome da Imagem</param>
        void RegisterFuncionario(Funcionario funcionario, byte[] Photo, string fileName);

        /// <summary>
        /// Edita funcionário salvo no banco de dados
        /// </summary>
        /// <param name="funcionario">Funcionário</param>
        /// <param name="Photo">Array de bytes da Imagem</param>
        /// <param name="fileName">Nome da Imagem</param>
        /// <returns>OK se sucesso e NotOK se falhar</returns>
        bool EditFuncionario(Funcionario funcionario, byte[] Photo, string fileName);

        /// <summary>
        /// Remove funcionário salvo no banco de dados de acordo com o CPF informado
        /// </summary>
        /// <param name="CPF">CPF</param>
        /// <returns>OK se sucesso e NotOK se falhar</returns>
        bool DelFuncionario(string CPF);
    }
}
