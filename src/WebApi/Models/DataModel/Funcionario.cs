using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    // Modelo da classe funcionário que será salva no BD
    public class Funcionario
    {
        [BsonId]
        [Display(Name = "CPF")]
        public string CPF { get; set; }

        [BsonRequired]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Telefone")]
        public string Tel { get; set; }

        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        public ObjectId Photo { get; set; }
    }

    public class FuncionarioView
    {
        [BsonId]
        [Display(Name = "CPF")]
        public string CPF { get; set; }

        [BsonRequired]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Telefone")]
        public string Tel { get; set; }

        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Display(Name = "Foto")]
        public IFormFile Photo { get; set; }
        public string FileUrl { get; set; }
    }
}
