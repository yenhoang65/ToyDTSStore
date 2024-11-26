using Business.DTO.Permission;
using Business.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.PermissionRepository
{
    public interface IPermissionRepository
    {
        Task<Response> GetAllPermissionsAsync(); 
        Task<Response> AddPermissionAsync(PermissionAddDTO addPermissionDto); 
        Task<Response> UpdatePermissionAsync(Guid id, PermissionUpdateDTO updatePermissionDto); 
        Task<Response> DeletePermissionAsync(Guid id); 
    }
}
