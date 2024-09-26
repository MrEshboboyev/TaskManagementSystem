using AutoMapper;
using TaskManagementSystem.Application.Common.Interfaces;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.DTOs.Company;
using TaskManagementSystem.Application.Services.Interfaces;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Infrastructure.Implementations
{
    public class CompanyService(IUnitOfWork unitOfWork, IMapper mapper) : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseDTO<IEnumerable<CompanyDTO>>> GetAllCompaniesAsync()
        {
            try
            {
                var companiesFromDb = await _unitOfWork.Company.GetAllAsync(
                    includeProperties: "Owner");
                
                var mappedCompanies = _mapper.Map<IEnumerable<CompanyDTO>>(companiesFromDb);

                return new ResponseDTO<IEnumerable<CompanyDTO>>(mappedCompanies);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<CompanyDTO>>(ex.Message);
            }
        }

        public async Task<ResponseDTO<CompanyDTO>> GetCompanyAsync(int companyId)
        {
            try
            {
                var companyFromDb = await _unitOfWork.Company.GetAsync(
                    filter: c => c.Id.Equals(companyId),
                    includeProperties: "Owner"
                    ) ?? throw new Exception("Company not found!");

                var mappedCompany = _mapper.Map<CompanyDTO>(companyFromDb);

                return new ResponseDTO<CompanyDTO>(mappedCompany);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<CompanyDTO>(ex.Message);
            }
        }

        public async Task<ResponseDTO<IEnumerable<CompanyDTO>>> GetUserCompaniesAsync(string userId)
        {
            try
            {
                var companiesFromDb = await _unitOfWork.Company.GetAllAsync(
                    filter: c => c.OwnerId.Equals(userId),
                    includeProperties: "Owner");

                var mappedCompanies = _mapper.Map<IEnumerable<CompanyDTO>>(companiesFromDb);

                return new ResponseDTO<IEnumerable<CompanyDTO>>(mappedCompanies);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<CompanyDTO>>(ex.Message);
            }
        }


        public async Task<ResponseDTO<CompanyDTO>> CreateCompany(CompanyCreateDTO companyCreateDTO)
        {
            try
            {
                var companyForDb = _mapper.Map<Company>(companyCreateDTO);

                await _unitOfWork.Company.AddAsync(companyForDb);
                await _unitOfWork.SaveAsync();

                var mappedCompany = _mapper.Map<CompanyDTO>(companyForDb);

                return new ResponseDTO<CompanyDTO>(mappedCompany);
            }
            catch (Exception ex) 
            {
                return new ResponseDTO<CompanyDTO>(ex.Message);
            }
        }

        public async Task<ResponseDTO<CompanyDTO>> UpdateCompany(int companyId, CompanyUpdateDTO companyUpdateDTO)
        {
            try
            {
                var companyFromDb = await _unitOfWork.Company.GetAsync(
                    c => c.Id.Equals(companyId) && 
                    c.OwnerId.Equals(companyUpdateDTO.OwnerId),
                    includeProperties: "Owner"
                    ) ?? throw new Exception("Company not found!");

                _mapper.Map(companyUpdateDTO, companyFromDb);

                await _unitOfWork.Company.Update(companyFromDb);
                await _unitOfWork.SaveAsync();

                var mappedCompany = _mapper.Map<CompanyDTO>(companyFromDb);

                return new ResponseDTO<CompanyDTO>(mappedCompany);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<CompanyDTO>(ex.Message);
            }
        }

        public async Task<ResponseDTO<bool>> DeleteCompanyAsync(CompanyDeleteDTO companyDeleteDTO)
        {
            try
            {
                var companyFromDb = await _unitOfWork.Company.GetAsync(
                    c => c.Id.Equals(companyDeleteDTO.CompanyId) && 
                    c.OwnerId.Equals(companyDeleteDTO.OwnerId)
                    ) ?? throw new Exception("Company not found!");

                await _unitOfWork.Company.RemoveAsync(companyFromDb);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO<bool>(true);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<bool>(ex.Message);
            }
        }
    }
}
