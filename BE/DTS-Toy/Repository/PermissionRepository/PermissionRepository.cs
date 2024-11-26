using Business.DTO.Permission;
using Business.Response;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.PermissionRepository
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ECommerceDBContext _dbContext;

        public PermissionRepository(ECommerceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Lấy danh sách quyền
        public async Task<Response> GetAllPermissionsAsync()
        {
            try
            {
                var permissions = await _dbContext.Permissions
                    .Select(p => new PermissionDTO
                    {
                        ID = p.ID,
                        PermissionName = p.PermissionName
                    })
                    .ToListAsync();

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = "Lấy danh sách quyền thành công.",
                    Data = permissions
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = ResponseCode.InternalServerError,
                    Message = "Lỗi khi lấy danh sách quyền: " + ex.Message
                };
            }
        }

        // Thêm quyền mới
        public async Task<Response> AddPermissionAsync(PermissionAddDTO addPermissionDto)
        {
            if (string.IsNullOrEmpty(addPermissionDto.PermissionName))
            {
                return new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = "Tên quyền không được để trống."
                };
            }

            var newPermission = new Permission
            {
                ID = Guid.NewGuid(),
                PermissionName = addPermissionDto.PermissionName
            };

            try
            {
                _dbContext.Permissions.Add(newPermission);
                await _dbContext.SaveChangesAsync();

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = "Thêm quyền thành công.",
                    Data = new PermissionDTO
                    {
                        ID = newPermission.ID,
                        PermissionName = newPermission.PermissionName
                    }
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = ResponseCode.InternalServerError,
                    Message = "Lỗi khi thêm quyền: " + ex.Message
                };
            }
        }

        // Sửa quyền
        public async Task<Response> UpdatePermissionAsync(Guid id, PermissionUpdateDTO updatePermissionDto)
        {
            var permission = await _dbContext.Permissions.FindAsync(id);
            if (permission == null)
            {
                return new Response
                {
                    Code = ResponseCode.NotFound,
                    Message = "Quyền không tồn tại."
                };
            }

            if (string.IsNullOrEmpty(updatePermissionDto.PermissionName))
            {
                return new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = "Tên quyền không được để trống."
                };
            }

            permission.PermissionName = updatePermissionDto.PermissionName;

            try
            {
                _dbContext.Permissions.Update(permission);
                await _dbContext.SaveChangesAsync();

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = "Cập nhật quyền thành công.",
                    Data = new PermissionDTO
                    {
                        ID = permission.ID,
                        PermissionName = permission.PermissionName
                    }
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = ResponseCode.InternalServerError,
                    Message = "Lỗi khi cập nhật quyền: " + ex.Message
                };
            }
        }

        // Xóa quyền
        public async Task<Response> DeletePermissionAsync(Guid id)
        {
            var permission = await _dbContext.Permissions.FindAsync(id);
            if (permission == null)
            {
                return new Response
                {
                    Code = ResponseCode.NotFound,
                    Message = "Quyền không tồn tại."
                };
            }

            try
            {
                _dbContext.Permissions.Remove(permission);
                await _dbContext.SaveChangesAsync();

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = "Xóa quyền thành công."
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = ResponseCode.InternalServerError,
                    Message = "Lỗi khi xóa quyền: " + ex.Message
                };
            }
        }
    }
}
