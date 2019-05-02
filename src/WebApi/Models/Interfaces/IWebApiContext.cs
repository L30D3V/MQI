namespace WebApi.Models
{
    using MongoDB.Driver;

    public interface IWebApiContext
    {
        IMongoCollection<ValuePairTest> TestValues { get; }
        IMongoCollection<Funcionario> Funcionarios { get; }
    }
}