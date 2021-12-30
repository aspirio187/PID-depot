using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Api.Depot.DAL.IRepositories
{
    public interface IRepositoryBase<TKey, TEntity> 
        where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TKey Create(TEntity data);
        TEntity GetById(TKey key);
        bool Update(TKey key, TEntity data);
        bool Delete(TKey key);
    }
}
