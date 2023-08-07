using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServerEmployee.DAL.Interfaces;
using WebServerEmployee.Domain.Entity;

namespace WebServerEmployee.DAL.Repositories
{
    public class DepartmentRepository : IDepartmentRepository<Department>
    {
        /// <summary>
        /// Получение списка всех отделов компании
        /// </summary>
        /// <returns></returns>
        public async Task<List<Department>> Select(int CompanyID)
        {
            using (var db = DbConnection.CreateConnection())
            {
                var obj = await db.QueryAsync<Department>("SELECT * FROM Department WHERE CompanyID = @CompanyID", new { CompanyID });
                return obj.ToList();
            }
        }
    }
}
