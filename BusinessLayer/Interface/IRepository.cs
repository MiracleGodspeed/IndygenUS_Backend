using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Model;

namespace BusinessLayer.Interface
{
    public interface IRepository<T> where T : BaseModel
    {
        IEnumerable<T> GetAll();
        T GetById(string Id);
        Task<string> Insert(T entity);
        Task <string> Update(T entity);
        void Delete(string Id);
    }
}
