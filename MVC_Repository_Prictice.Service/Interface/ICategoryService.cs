using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC_Repository_Prictice.Models;
using MVC_Repository_Prictice.Service.Misc;

namespace MVC_Repository_Prictice.Service.Interface
{
   public interface ICategoryService:IDisposable
    {
        IResult Create(Categories instance);
        IResult Update(Categories instance);
        IResult Delete(int categoryID);
        bool IsExists(int categoryID);
        Categories GetById(int categoryID);
        IEnumerable<Categories> GetAll();
    }
}
