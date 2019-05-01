namespace WebApi.Models 
{
    using MongoDB.Bson.Serialization.Attributes;

    // Modelo de Valor que será salvo no BD
    public class ValuePairTest
    {
        [BsonId]
        public string id { get; set; }
        [BsonRequired]
        public string value { get; set; }
    }
}