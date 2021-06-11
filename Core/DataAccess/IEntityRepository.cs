using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    //generic constraint ------ generic kısıt
    //class referans tip demek
    //IEntity den implemente olması ve new'lenebilir olması gerek
    public interface IEntityRepository<T> where T: class,IEntity,new()
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void UpdateList(List<T> entities);  
        void Delete(T entity);
    }
}
