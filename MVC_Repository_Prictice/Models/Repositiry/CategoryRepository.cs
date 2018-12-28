using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Repository_Prictice.Models.Repositiry
{
    public class CategoryRepository : Interface.ICategoryRepository
    {
        protected NorthwindEntities db
        {
            get;
            private set;
        }
        public CategoryRepository()
        {
            this.db = new NorthwindEntities();
        }
        public void Create(Categories instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                db.Categories.Add(instance);
                this.SaveChanges();
            }
        }

        public void Update(Categories instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                var entry = db.Entry(instance);
                if (entry.State == System.Data.Entity.EntityState.Detached)
                {
                    var set = db.Set(instance.GetType());
                    var attachedEntity = set.Find(instance);
                    if (attachedEntity != null)
                    {
                        var attchedEntry = db.Entry(attachedEntity);
                        attchedEntry.CurrentValues.SetValues(instance);
                    }
                    else
                    {
                        db.Entry(instance).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                this.SaveChanges();
            }
        }

        public void Delete(Categories instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                db.Entry(instance).State = System.Data.Entity.EntityState.Deleted;
                this.SaveChanges();
            }
        }

        public Categories Get(int categoryID)
        {
            return db.Categories.FirstOrDefault(d => d.CategoryID == categoryID);
        }

        public IQueryable<Categories> GetAll()
        {
            return db.Categories.OrderBy(d => d.CategoryID);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
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
                if (this.db != null)
                {
                    this.db.Dispose();
                    this.db = null;
                }
            }
        }
    }
}