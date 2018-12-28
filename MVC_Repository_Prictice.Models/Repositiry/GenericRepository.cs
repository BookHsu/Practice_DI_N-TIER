using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using MVC_Repository_Prictice.Models.DbContextFactory;
using MVC_Repository_Prictice.Models.Interface;

namespace MVC_Repository_Prictice.Models.Repositiry
{
    public class GenericRepository<TEntity>:IRepository<TEntity> where TEntity:class
    {
        private DbContext _context

        {

            get;

            set;

        }        
        public GenericRepository(IDbContextFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException("factory");
            }
            this._context = factory.GetDbContext();
        }


        //public GenericRepository(ObjectContext context)
        //{

        //    if (context == null)
        //    {
        //        throw new ArgumentNullException("context");
        //    }
        //    this._context = new DbContext(context, true);
        //}
        /// <summary>
        /// Creates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <exception cref="System.ArgumentNullException">instance</exception>
        public void Create(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Set<TEntity>().Add(instance);
                this.SaveChanges();
            }
        }
        /// <summary>
        /// Updates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Update(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                var entry = _context.Entry(instance);
                if (entry.State == System.Data.Entity.EntityState.Detached)
                {
                    var set = _context.Set(instance.GetType());
                    var attachedEntity = set.Find(instance);
                    if (attachedEntity != null)
                    {
                        var attchedEntry = _context.Entry(attachedEntity);
                        attchedEntry.CurrentValues.SetValues(instance);
                    }
                    else
                    {
                        _context.Entry(instance).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                this.SaveChanges();
            }
        }
        /// <summary>
        /// Deletes the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Delete(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Entry(instance).State = EntityState.Deleted;
                this.SaveChanges();
            }
        }
        /// <summary>
        /// Gets the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return this._context.Set<TEntity>().FirstOrDefault(predicate);
        }
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll()
        {
            return this._context.Set<TEntity>().AsQueryable();
        }
        public void SaveChanges()
        {
            this._context.SaveChanges();
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._context != null)
                {
                    this._context.Dispose();
                    this._context = null;
                }
            }
        }
    }
}