using EventsAPI.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using System.Linq.Expressions;
using System.Reflection;

namespace EventsAPI.Services {
    public class DataService<T> : IDataService<T> {
        private List<T> values = new List<T>();
        private NHibernate.ISession session;
        private IDistributedCache cache;

        public DataService(NHibernate.ISession session, IDistributedCache cache) {
            this.session = session;
            this.cache = cache;
        }

        public async Task<List<T>> GetAll() {
            var recordKey = $"Events_{DateTime.UtcNow.ToString("yyyyMMdd_hhmm")}";
            var cachedValues = await cache.GetRecordAsync<List<T>>(recordKey);

            if (cachedValues is null) {
                // can we change object?
                this.values = this.session.CreateCriteria<object>().List<T>().ToList();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Loaded from database!");
                await cache.SetRecordAsync(recordKey, this.values);
                return this.values;
            }

            this.values = cachedValues;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Loaded from cache!");

            return this.values;
        }

        public T? Get<S>(Expression<Func<T, object>> property, object value) {
            var body = property.Body;
            if (body is UnaryExpression unaryExpression) {
                body = unaryExpression.Operand;
            }

            var propertyInfo = ((MemberExpression) body).Member as PropertyInfo;
            return this.values.FirstOrDefault(x => Equals(propertyInfo?.GetValue(x), value));
        }

        public void Create(T newObject) {
            throw new NotImplementedException();
        }

        public bool Update(T updatedObject) {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id) {
            throw new NotImplementedException();
        }
    }
}
