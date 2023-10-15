

namespace Future_Generation.Repositories
{
    
        public interface IRepository<T>
        {
            List<T> GetAll();
           //SearchResult<T> GetAll(EntitySC sc);
            T GetById(int id);
           void Add(T entity);
        void Update(int id, T entity);
            void Delete(int id);
            
    }

    
}
