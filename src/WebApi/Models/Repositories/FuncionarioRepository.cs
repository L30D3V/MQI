using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Models
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        // Cria variáveis globais para conexão com o BD e GridFS
        private readonly IWebApiContext _context;
        private readonly GridFSBucket _gridFS;

        public FuncionarioRepository(IWebApiContext context)
        {
            // Inicializa dados de conexão com o BD e GridFS
            _context = context;
            _gridFS = context.GridFS;
        }

        public bool DelFuncionario(string CPF)
        {
            try
            {
                // Remove imagem caso exista
                Funcionario func = _context.Funcionarios.Find(x => x.CPF == CPF).FirstOrDefault();
                if (func != null && func.Photo != null)
                {
                    _gridFS.Delete(func.Photo);
                }

                // Filtra funcionário pelo CPF e remove-o do BD
                FilterDefinition<Funcionario> filter = Builders<Funcionario>.Filter.Eq(x => x.CPF, CPF);
                DeleteResult deleteResult = _context.Funcionarios.DeleteOne(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao deletar funcionário", ex);
            }
        }

        public bool EditFuncionario(Funcionario funcionario, byte[] Photo, string fileName)
        {
            try
            {
                // Remove imagem antiga caso exista
                Funcionario func = _context.Funcionarios.Find(x => x.CPF == funcionario.CPF).FirstOrDefault();
                if (func != null && func.Photo != null)
                {
                    _gridFS.Delete(func.Photo);
                }
                
                // Adiciona nova imagem caso exista
                if (funcionario.Photo != null)
                {
                    ObjectId photoId = _gridFS.UploadFromBytes(fileName, Photo);
                    funcionario.Photo = photoId;
                }

                // Atualiza valores do funcionário no database
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
                // Retorna todos os valores salvos na Collection Funcionarios
                return _context.Funcionarios.Find(_ => true).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível recuperar lista de funcionários", ex);
            }
        }

        public FuncionarioView GetFuncionario(string CPF)
        {
            try
            {
                // Recupera funcionário pelo CPF informado
                Funcionario funcionario = _context.Funcionarios.Find(x => x.CPF == CPF).FirstOrDefault();

                // Cria novo objeto FuncionarioView e inicializa com valores recuperados do banco
                FuncionarioView data = new FuncionarioView()
                {
                    CPF = funcionario.CPF,
                    Email = funcionario.Email,
                    Endereco = funcionario.Endereco,
                    Nome = funcionario.Nome,
                    Tel = funcionario.Tel,
                };

                // Recupera imagem do funcionário caso exista
                if (funcionario.Photo != null)
                {
                    byte[] photo = _gridFS.DownloadAsBytes(funcionario.Photo);
                    string base64string = Convert.ToBase64String(photo, 0, photo.Length);
                    data.FileUrl = "data:image/png;base64," + base64string;
                }

                return data;
            }
            catch(Exception ex)
            {
                throw new Exception("Não foi possível recuperar funcionário", ex);
            }
        }

        public void RegisterFuncionario(Funcionario funcionario, byte[] Photo, string fileName)
        {
            try
            {
                // Salva foto no database caso exista
                if (Photo != null && fileName != null)
                {
                    ObjectId photoId = _gridFS.UploadFromBytes(fileName, Photo);
                    funcionario.Photo = photoId;
                }
                
                // Salva funcionário no banco de dados
                _context.Funcionarios.InsertOne(funcionario);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível cadastrar o funcionário", ex);
            }
        }
    }
}
