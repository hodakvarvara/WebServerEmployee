using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServerEmployee.BL.Interfaces;
using WebServerEmployee.DAL.Interfaces;
using WebServerEmployee.Domain.Entity;
using WebServerEmployee.Domain.Enum;
using WebServerEmployee.Domain.Response;

namespace WebServerEmployee.BL.Implementations
{
    public class DepartmentBL : IDepartmentBL
    {

        private readonly IDepartmentRepository<Department> _departmentRepository;
        public DepartmentBL(IDepartmentRepository<Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        /// <summary>
        /// Получение всех Отделы, которые есть в бд
        /// </summary>
        /// <returns></returns>
        public async Task<IBaseResponse<IEnumerable<Department>>> GetAllDepartments(int id)
        { 
            var baseResponse = new BaseResponse<IEnumerable<Department>>();
            try
            {
                var department = await _departmentRepository.Select(id);

                if (department.Count == 0)
                {
                    baseResponse.Description = "Отделы не найдены!";
                    baseResponse.StatusCode = StatusCode.ObjNotFound;
                    return baseResponse;
                }
                baseResponse.Data = department;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Department>>()
                {
                    Description = $"[GetAllDepartmens] : ex.Message",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
