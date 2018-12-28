using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC_Repository_Prictice.Models;

namespace MVC_Repository_Prictice.Service.Interface
{
   public interface ICategoryService
    {
        IResult Create(Categories instance);
        IResult Update(Categories instance);
        IResult Delete(int categoryID);
        bool IsExists(int categoryID);
        Categories GetByID(int categoryID);
        IEnumerable<Categories> GetAll();
    }
}
