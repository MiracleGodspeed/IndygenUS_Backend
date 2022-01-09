using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interface;

namespace DataLayer.Model
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        protected readonly DBContext _context;
        private Microsoft.EntityFrameworkCore.DbSet<T> entities;
        public Repository(DBContext context)
        {
            _context = context;
            entities = _context.Set<T>();
        }
        public void Delete(string Id)
        {
            if (Id != null) throw new ArgumentNullException("No Id Value");

            T entity = entities.SingleOrDefault(f => f.Id == Id);
            entities.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public T GetById(string Id)
        {
            return entities.SingleOrDefault(f => f.Id == Id);
        }

        public async Task<string> Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException("null entity");
            entities.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<string> Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("null entity");
            entities.Update(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }
    }
}
