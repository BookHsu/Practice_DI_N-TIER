using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Repository_Prictice.Service.Misc
{
    public interface IResult
    {
        Guid ID { get; }
        bool Success { get; set; }
        string Message { get; set; }
        Exception Exception { get; set; }
        List<IResult> InnerResults { get; }
    }
}
