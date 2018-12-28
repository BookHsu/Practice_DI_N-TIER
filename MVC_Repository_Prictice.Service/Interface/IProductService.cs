using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC_Repository_Prictice.Models;
using MVC_Repository_Prictice.Service.Misc;

namespace MVC_Repository_Prictice.Service.Interface
{
    public interface IProductService: IDisposable
    {
        IResult Create(Products instance);
        IResult Update(Products instance);
        IResult Delete(int productID);
        bool IsExists(int productID);
        Products GetById(int productID);
        IEnumerable<Products> GetAll();
        IEnumerable<Products> GetByCategory(int categoryID);
    }
}
