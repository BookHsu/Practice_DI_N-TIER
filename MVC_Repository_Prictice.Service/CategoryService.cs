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
    public class CategoryService : ICategoryService
    {
        private IRepository<Categories> _repository ;
        public CategoryService(IRepository<Categories> repository)
        {
            this._repository = repository;
        }

        public IResult Create(Categories instance)
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

        public IResult Update(Categories instance)
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

        public IResult Delete(int categoryID)
        {
            IResult result = new Result(false);

            if (!this.IsExists(categoryID))
            {
                result.Message = "找不到資料";
            }

            try
            {
                var instance = this.GetById(categoryID);

                this._repository.Delete(instance);

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }

            return result;
        }

        public bool IsExists(int categoryID)
        {
            return this._repository.GetAll().Any(x => x.CategoryID == categoryID);
        }

        public Categories GetById(int categoryID)
        {
            return this._repository.Get(x => x.CategoryID == categoryID);
        }

        public IEnumerable<Categories> GetAll()

        {
            return this._repository.GetAll();
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