namespace DAL.Repositories.Interfaces
{
    using Models.Domain.Models;
    using System.Collections.Generic;

    public interface IRepository<T> where T:IMongoEntity
    {
        List<T> Get();
        T Get(string id);
        T Create(T model);
        void Update(string id, T model);
        void Delete(string id);
    }
}
