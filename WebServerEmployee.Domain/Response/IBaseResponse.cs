using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServerEmployee.Domain.Enum;

namespace WebServerEmployee.Domain.Response
{
    public interface IBaseResponse<T>
    {
        T Data { get; set; } // результаты запроса к бд
        StatusCode StatusCode { get; }
    }
}
