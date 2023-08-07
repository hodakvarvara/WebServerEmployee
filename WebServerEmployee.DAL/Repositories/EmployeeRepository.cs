using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServerEmployee.DAL.Interfaces;
using WebServerEmployee.Domain.Entity;

namespace WebServerEmployee.DAL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        /// <summary>
        /// Получение всех соотрудников
        /// </summary>
        /// <returns></returns>
        public async Task<List<Employee>> Select()
        {
            using (var db = DbConnection.CreateConnection())
            {
                var obj = await db.QueryAsync<Employee>("SELECT * FROM Employee");
                return obj.ToList();
            }
        }

        /// <summary>
        /// Получение сотрудника по Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Employee> GetByName(string name)
        {
            using (var db = DbConnection.CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<Employee>("SELECT * FROM Employee WHERE Name = @name", new { name });
            }
        }

        /// <summary>
        /// Получение сотрудника по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Employee> Get(int id)
        {
            using (var db = DbConnection.CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<Employee>("SELECT * FROM Employee WHERE EmployeeID = @id", new { id });
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (var db = DbConnection.CreateConnection())
            {
                var sqlQuery = "DELETE FROM Employee WHERE EmployeeId = @id";
                await db.ExecuteAsync(sqlQuery, new { id });
            }
            return true;
        }

        public async Task<int> Create(Employee employee)
        {
            using (var db = DbConnection.CreateConnection())
            {
                var sqlQuery = "INSERT INTO Employee ([Name], Surname, Phone_number, CompanyID, PassportID, DepartmentID) VALUES (@Name, @Surname, @Phone_number, @CompanyID, @PassportID, @DepartmentID); " +
                    "SELECT CAST(SCOPE_IDENTITY() as int)";
                var id = await db.QueryFirstOrDefaultAsync<int>(sqlQuery, employee);
                //await db.ExecuteAsync(sqlQuery, employee);
                employee.EmployeeID = id;
            }
            return employee.EmployeeID;
        }

        public async Task<Employee> Update(Employee employee)
        {
            using (var db = DbConnection.CreateConnection())
            {
                var sqlQuery = "UPDATE Employee SET Name = @Name, Surname = @Surname, Phone_number = @Phone_number," +
                    "CompanyID = @CompanyID, PassportID = @PassportID, DepartmentID = @DepartmentID WHERE EmployeeID = @EmployeeID";
                await db.ExecuteAsync(sqlQuery, employee);
            }
            return employee;
        }

        /// <summary>
        /// Получение сотрудников по компании
        /// </summary>
        /// <returns></returns>
        public async Task<List<Employee>> SelectEmployeeByCompany(int CompanyID)
        {
            using (var db = DbConnection.CreateConnection())
            {
                var obj = await db.QueryAsync<Employee>("SELECT * FROM Employee WHERE CompanyID = @CompanyID", new { CompanyID });
                return obj.ToList();
            }
        }

        /// <summary>
        /// Получение сотрудников по компании
        /// </summary>
        /// <returns></returns>
        public async Task<List<Employee>> SelectEmployeeByDepartment(int DepartmentID)
        {
            using (var db = DbConnection.CreateConnection())
            {
                var obj = await db.QueryAsync<Employee>("SELECT * FROM Employee WHERE DepartmentID = @DepartmentID", new { DepartmentID });
                return obj.ToList();
            }
        }
    }
}
