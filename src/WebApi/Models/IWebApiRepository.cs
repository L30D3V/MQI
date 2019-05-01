namespace WebApi.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IWebApiRepository
    {
        // api/[GET]
        Task<IEnumerable<ValuePairTest>> GetAllValues();

        // api/id/[GET]
        Task<ValuePairTest> GetValue(string id);

        // api/[POST]
        Task Create(ValuePairTest value);

        // api/[PUT]
        Task<bool> Update(ValuePairTest value);

        // api/id/[DELETE]
        Task<bool> Delete(string id);

        // Listar todos os valores
        List<ValuePairTest> ListValues();

        // Recuperar valor espec�fico por id
        ValuePairTest GetValueById(string id);

        // Recuperar valor espec�fico por id
        bool DeleteById(string id);

        // Editar valor
        bool EditById(ValuePairTest value);
    }
}