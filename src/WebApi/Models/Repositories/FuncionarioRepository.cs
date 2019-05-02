using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly IWebApiContext _context;

        public FuncionarioRepository(IWebApiContext context)
        {
            _context = context;
        }

        public bool DelFuncionario(string CPF)
        {
            try
            {
                FilterDefinition<Funcionario> filter = Builders<Funcionario>.Filter.Eq(x => x.CPF, CPF);
                DeleteResult deleteResult = _context.Funcionarios.DeleteOne(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao deletar funcionário", ex);
            }
        }

        public bool EditFuncionario(Funcionario funcionario)
        {
            try
            {
                ReplaceOneResult result = _context.Funcionarios.ReplaceOne(filter: f => f.CPF == funcionario.CPF, replacement: funcionario);
                return result.IsAcknowledged && result.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao editar funcionário", ex);
            }
        }

        public List<Funcionario> GetAllFuncionarios()
        {
            try
            {
                return _context.Funcionarios.Find(_ => true).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível recuperar lista de funcionários", ex);
            }
        }

        public Funcionario GetFuncionario(string CPF)
        {
            try
            {
                return _context.Funcionarios.Find(x => x.CPF == CPF).FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw new Exception("Não foi possível recuperar funcionário", ex);
            }
        }

        public void RegisterFuncionario(Funcionario funcionario)
        {
            try
            {
                _context.Funcionarios.InsertOne(funcionario);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível cadastrar o funcionário", ex);
            }
        }
    }
}
