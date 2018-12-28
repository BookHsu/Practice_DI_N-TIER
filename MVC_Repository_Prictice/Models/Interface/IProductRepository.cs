using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Repository_Prictice.Models.Interface
{
    public interface IProductRepository :IRepository<Products>
    {
        Products GetByID(int productID);
        IEnumerable<Products> GetByCateogy(int categoryID);
    }
}