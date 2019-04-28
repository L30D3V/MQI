namespace WebApi.Models 
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    // Modelo de Valor que será salvo no BD
    public class ValuePairTest
    {
        [BsonId]
        public string id { get; set; }
        public string value { get; set; }
    }
}