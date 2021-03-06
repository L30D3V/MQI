using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models 
{
    // Modelo de Valor que será salvo no BD
    public class ValuePairTest
    {
        [BsonId]
        [Display(Name = "Id")]
        public string id { get; set; }

        [BsonRequired]
        [Display(Name = "Valor")]
        public string value { get; set; }
    }
}