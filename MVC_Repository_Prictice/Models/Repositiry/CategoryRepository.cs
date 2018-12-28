using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_Repository_Prictice.Models.Interface;

namespace MVC_Repository_Prictice.Models.Repositiry
{
    public class CategoryRepository:GenericRepository<Categories>, ICategoryRepository
    {
        /// <summary>
        /// Gets the by ID.
        /// </summary>
        /// <param name="categoryID">The category ID.</param>
        /// <returns></returns>
        public Categories GetById(int categoryID)
        {
            return this.Get(c => c.CategoryID == categoryID);
        }
    }
}