using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.DTOs.Company;

namespace TaskManagementSystem.Application.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<ResponseDTO<IEnumerable<CompanyDTO>>> GetAllCompaniesAsync();
        Task<ResponseDTO<CompanyDTO>> GetCompanyAsync(int companyId);
        Task<ResponseDTO<CompanyDTO>> CreateCompany(CompanyCreateDTO companyCreateDTO);
        Task<ResponseDTO<CompanyDTO>> UpdateCompany(int companyId, CompanyUpdateDTO companyUpdateDTO);
        Task<ResponseDTO<bool>> DeleteCompanyAsync(CompanyDeleteDTO companyDeleteDTO);
    }
}
