using Models.Domain.Models;
using System.Collections.Generic;

namespace BLL.Services.Interfaces
{
    public interface IService<T> where T : IMongoEntity
    {
        T Create(T model);

        T Update(string id, T model);

        List<T> Get();

        T Get(string id);
        void Delete(string id);
    }
}
