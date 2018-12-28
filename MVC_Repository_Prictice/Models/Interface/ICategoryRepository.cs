using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Repository_Prictice.Models.Interface
{
    interface ICategoryRepository : IRepository<Categories>
    {
        Categories GetById(int categoryID);
    }
}
