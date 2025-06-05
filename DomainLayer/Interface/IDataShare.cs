
using DomainLayer.Entity;

namespace DomainLayer.Interface
{
    public interface IDataShare
    {
        Task<CodeshareEntity> CreateAsync(CodeshareEntity dto);

        Task<CodeshareEntity?> GetAsync(string urlKey);

        Task<CodeshareEntity> UpdateAsync(CodeshareEntity entity);

    }
}
