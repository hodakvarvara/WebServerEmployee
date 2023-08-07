using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServerEmployee.BL.Interfaces;
using WebServerEmployee.DAL.Interfaces;
using WebServerEmployee.DAL.Repositories;
using WebServerEmployee.Domain.Entity;
using WebServerEmployee.Domain.Enum;
using WebServerEmployee.Domain.Response;

namespace WebServerEmployee.BL.Implementations
{
    public class CompanyBL : ICompanyBL
    {
        private readonly ICompanyRepository<Company> _companyRepository;
        public CompanyBL(ICompanyRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
        }

        /// <summary>
        /// Получение всех company, которые есть в бд
        /// </summary>
        /// <returns></returns>
        public async Task<IBaseResponse<IEnumerable<Company>>> GetAllCompanies()
        {
            var baseResponse = new BaseResponse<IEnumerable<Company>>();
            try
            {
                var company = await _companyRepository.SelectCompany();
                if (company.Count == 0)
                {
                    baseResponse.Description = "Компании не найдены!";
                    baseResponse.StatusCode = StatusCode.ObjNotFound;
                    return baseResponse;
                }
                baseResponse.Data = company;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Company>>()
                {
                    Description = $"[GetAllCompanies] : ex.Message",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

       

        /// <summary>
        /// Получение сотрудника по id
        /// </summary>
        /// <returns></returns>
        //public async Task<IBaseResponse<IEnumerable<Employee>>> GetEmployeeByCompany(int id)
        //{
        //    var baseResponse = new BaseResponse<Employee>();
        //    try
        //    {
        //        var employee = await _companyRepository.SelectEmployee(id);
        //        if (employee == null)
        //        {
        //            baseResponse.Description = "Сотрудник не найден!";
        //            baseResponse.StatusCode = StatusCode.ObjNotFound;
        //            return baseResponse;
        //        }
        //        baseResponse.StatusCode = StatusCode.OK;
        //        return baseResponse;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BaseResponse<Employee>()
        //        {
        //            Description = $"[GetEmployeeByCompany] : ex.Message",
        //            StatusCode = StatusCode.InternalServerError
        //        };
        //    }
        //}
    }
}
