using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Repository_Prictice.Models.Repositiry
{
    public class ProductRepository:Interface.IProductRepository
    {

        protected NorthwindEntities db
        {
            get;
            private set;
        }
        public ProductRepository()
        {
            this.db = new NorthwindEntities();
        }
        public void Create(Products instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                db.Products.Add(instance);
                this.SaveChanges();
            }
        }

        public void Update(Products instance)
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

        public void Delete(Products instance)
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

        public Products Get(int categoryID)
        {
            return db.Products.FirstOrDefault(d => d.CategoryID == categoryID);
        }

        public IQueryable<Products> GetAll()
        {
            return db.Products.OrderBy(d => d.CategoryID);
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