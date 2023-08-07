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
    public class CompanyRepository : ICompanyRepository<Company>
    {

        /// <summary>
        /// Получение списка всех компаний
        /// </summary>
        /// <returns></returns>
        public async Task<List<Company>> SelectCompany()
        {
            using (var db = DbConnection.CreateConnection())
            {
                var obj = await db.QueryAsync<Company>("SELECT * FROM Company");
                return obj.ToList();
            }
        }
    }
}
