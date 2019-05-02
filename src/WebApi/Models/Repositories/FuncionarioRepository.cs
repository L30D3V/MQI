using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly IWebApiContext _context;
        private readonly GridFSBucket _gridFS;

        public FuncionarioRepository(IWebApiContext context)
        {
            _context = context;
            _gridFS = context.GridFS;
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

        public FuncionarioView GetFuncionario(string CPF)
        {
            try
            {
                Funcionario funcionario = _context.Funcionarios.Find(x => x.CPF == CPF).FirstOrDefault();

                FuncionarioView data = new FuncionarioView()
                {
                    CPF = funcionario.CPF,
                    Email = funcionario.Email,
                    Endereco = funcionario.Endereco,
                    Nome = funcionario.Nome,
                    Tel = funcionario.Tel,
                };

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
                ObjectId photoId = _gridFS.UploadFromBytes(fileName, Photo);
                funcionario.Photo = photoId;
                _context.Funcionarios.InsertOne(funcionario);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível cadastrar o funcionário", ex);
            }
        }
    }
}
