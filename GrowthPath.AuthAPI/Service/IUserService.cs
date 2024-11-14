using GrowthPath.AuthAPI.Models.DTO;

namespace GrowthPath.AuthAPI.Service
{
    public interface IUserService
    {

        Task<List<EmployeeDto>> GetUsersByRole(string role);


    }
}
