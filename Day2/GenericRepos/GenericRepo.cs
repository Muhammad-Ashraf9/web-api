using Day2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.TelemetryCore.TelemetryClient;

namespace Day2.GenericRepos
{
    public class GenericRepo<TEntity> where TEntity : class
    {
        private ITIContext db;

        public GenericRepo(ITIContext dbContext)
        {
            db = dbContext;
        }

        public List<TEntity> GetAll()
        {
           return db.Set<TEntity>().ToList();
        }

        public TEntity? GetById(int id)
        {
            return db.Set<TEntity>().Find(id);
        }

        public void Add(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            db.Set<TEntity>().Remove(entity);
        }

        public void Save()
        {
            db.SaveChanges();
        }


    }
}
