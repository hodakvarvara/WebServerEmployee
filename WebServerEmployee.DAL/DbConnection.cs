using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerEmployee.DAL
{
    internal class DbConnection
    {
        public static SqlConnection CreateConnection()
        {
            return new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=EmployeeDb;Integrated Security=True"); 
        }
        
    }
}
