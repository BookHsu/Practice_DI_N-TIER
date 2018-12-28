using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC_Repository_Prictice.Models;

namespace MVC_Repository_Prictice.Service.Interface
{
    public interface IProductService
    {
        IResult Create(Products instance);
        IResult Update(Products instance);
        IResult Delete(int productID);
        bool IsExists(int productID);
        Products GetByID(int productID);
        IEnumerable<Products> GetAll();
        IEnumerable<Products> GetByCategory(int categoryID);
    }
}
