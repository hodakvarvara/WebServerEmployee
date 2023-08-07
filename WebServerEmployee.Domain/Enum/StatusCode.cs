using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerEmployee.Domain.Enum
{
    public enum StatusCode
    {
        ObjNotFound = 0, // объект не найден
        OK = 200,
        InternalServerError = 500 // ошибка случилась на стороне сервера 
    }
}
