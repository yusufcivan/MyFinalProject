using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    //bir tane entity tipi tablo  ve bir tane context tipi ver diyoruz
    public class EfEntityRepositoryBase<TEntity,TContext>:IEntityRepository<TEntity>
        //programcıyı bizim altyapımızı kullansın diye yönlendiriyoruz where ile kurallar ekliyoruz
        // TEntity class olacak ve newlenemeyecek diye kurallar veriyoruz

        where TEntity: class, IEntity, new()
        where TContext : DbContext, new()

        // TBcontext Dbcontext olması gerekiyor diyoruz, kafana gore class yazma dbcontextten inherit olmalı

    {
        public void Add(TEntity entity)
        {
            //IDisposable pattern implementation of c#
            using (TContext context = new TContext())
            {
                //Git verikaynağından gönderdiğim product'tan bir nesnesyi eslestir.Ekleme olacağı için eşleme olmayacak ekleyecek
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                //Git verikaynağından gönderdiğim product'tan bir nesnesyi eslestir
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                //filtre null mı? evet ise tümünü getir : null değilse filtreleyip ver
                return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                //Git verikaynağından gönderdiğim product'tan bir nesnesyi eslestir
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

    }
}
