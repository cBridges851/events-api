using System.Linq.Expressions;

namespace EventsAPI.Services {
    public interface IDataService<T> {
        Task<List<T>> GetAll();
        T? Get<S>(Expression<Func<T, object>> property, object value);
        void Create(T newObject);
        bool Update(T updatedObject);
        bool Delete(Guid id);
    }
}