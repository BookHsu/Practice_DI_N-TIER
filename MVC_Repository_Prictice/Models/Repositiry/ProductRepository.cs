using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_Repository_Prictice.Models.Interface;

namespace MVC_Repository_Prictice.Models.Repositiry
{
    public class ProductRepository:GenericRepository<Products>, IProductRepository
    {
        public Products GetByID(int productID)
        {
            return this.Get(C => C.ProductID == productID);
        }

        public IEnumerable<Products> GetByCateogy(int categoryID)
        {
            return this.GetAll().Where(d => d.CategoryID == categoryID);
        }
    }
}