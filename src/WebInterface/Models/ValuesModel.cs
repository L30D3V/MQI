using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebInterface.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    // Modelo de Valor que será salvo no BD
    public class ValuePairTest
    {
        [BsonId]
        public string id { get; set; }
        public string value { get; set; }
    }
}
