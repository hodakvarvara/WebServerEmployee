using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServerEmployee.Domain.Enum;

namespace WebServerEmployee.Domain.Response
{
    /// <summary>
    /// Обработка значений для BL
    /// </summary>
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public string Description { get; set; } // строка, содержащая текст ошибки
        public StatusCode StatusCode { get; set; } // код ошибки
        public T Data { set; get; } // результаты запроса к бд 
    }
}
