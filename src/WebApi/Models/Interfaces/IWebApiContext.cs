namespace WebApi.Models
{
    using MongoDB.Driver;
    using MongoDB.Driver.GridFS;

    public interface IWebApiContext
    {
        IMongoCollection<ValuePairTest> TestValues { get; }
        IMongoCollection<Funcionario> Funcionarios { get; }
        GridFSBucket GridFS { get; }
    }
}