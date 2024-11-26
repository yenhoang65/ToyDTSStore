using Business.DTO.RoleDetail;
using Business.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RoleDetail
{
    public interface IRoleDetailRepository
    {
        Task<PaginatedList<RoleDetailResponseDTO>> GetAllRoleDetailsAsync(int pageIndex = 1, int pageSize = 10);
        Task<RoleDetailResponseDTO?> GetRoleDetailByIdAsync(Guid id);
        Task<RoleDetailResponseDTO> AddRoleDetailAsync(RoleDetailAddOrUpdateDTO roleDetailDto);
        Task<RoleDetailResponseDTO> UpdateRoleDetailAsync(Guid id, RoleDetailAddOrUpdateDTO roleDetailDto);

        Task<bool> DeleteRoleDetailAsync(Guid id);
    }
}
