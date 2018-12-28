using System;
using System.Collections.Generic;
using System.Linq;
using MVC_Repository_Prictice.Models;
using MVC_Repository_Prictice.Models.Interface;
using MVC_Repository_Prictice.Models.Repositiry;
using MVC_Repository_Prictice.Service.Interface;
using MVC_Repository_Prictice.Service.Misc;

namespace MVC_Repository_Prictice.Service
{
    public class ProductService : IProductService
    {
        private IRepository<Products> _repository;
        public ProductService(IRepository<Products> repository)
        {
            this._repository = repository;
        }

        public Misc.IResult Create(Products instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException();
            }
            IResult result = new Result(false);
            try
            {
                this._repository.Create(instance);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public IResult Update(Products instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException();
            }
            IResult result = new Result(false);
            try
            {
                this._repository.Update(instance);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public IResult Delete(int productID)
        {
            IResult result = new Result(false);
            if (!this.IsExists(productID))
            {
                result.Message = "找不到資料";
            }
            try
            {
                var instance = this.GetById(productID);
                this._repository.Delete(instance);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public bool IsExists(int productID)
        {
            return this._repository.GetAll().Any(x => x.ProductID == productID);
        }

        public Products GetById(int productID)
        {
            return this._repository.Get(x => x.ProductID == productID);
        }

        public IEnumerable<Products> GetAll()
        {
            return this._repository.GetAll();
        }

        public IEnumerable<Products> GetByCategory(int categoryID)
        {
            return this._repository.GetAll().Where(x => x.CategoryID == categoryID);
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
                if (this._repository != null)
                {
                    this._repository.Dispose();
                    this._repository = null;
                }
            }
        }
    }
}