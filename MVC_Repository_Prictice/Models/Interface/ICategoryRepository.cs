using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Repository_Prictice.Models.Interface
{
    interface ICategoryRepository: IDisposable
    {
        void Create(Categories instance);
        void Update(Categories instance);
        void Delete(Categories instance);
        Categories Get(int categoryID);
        IQueryable<Categories> GetAll();
        void SaveChanges();

    }
}
