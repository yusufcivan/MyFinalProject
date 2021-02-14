using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    //generic constraint generik kısıt //where T = 'T ne olabilir' class referans tip olabilir ve IEntity veya implemente olabilir
    // new() : new'lenebilir olmalı Direk IEntity'i degil içinde tuttuğu refansları kullanmak için öyle yapıyor.
    public interface IEntityRepository<T> where T:class, IEntity,new()
    {
        List<T> GetAll(Expression<Func<T,bool>> filter = null); //filter null demek filtre vermeyebilirsin demek
        T Get(Expression<Func<T, bool>> filter); //filtre vermek zorunlu
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
