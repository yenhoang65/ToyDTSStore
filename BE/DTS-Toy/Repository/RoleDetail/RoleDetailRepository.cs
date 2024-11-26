using Business.DTO.RoleDetail;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Business.Helper;

namespace Repository.RoleDetail
{
    public class RoleDetailRepository : IRoleDetailRepository
    {
        private readonly ECommerceDBContext _dbContext;

        public RoleDetailRepository(ECommerceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedList<RoleDetailResponseDTO>> GetAllRoleDetailsAsync(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var query = _dbContext.RoleDetails.AsNoTracking(); 

                var totalCount = await query.CountAsync();

                var roleDetails = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(rd => new RoleDetailResponseDTO
                    {
                        ID = rd.ID,
                        RoleDetailName = rd.RoleDetailName
                    })
                    .ToListAsync();

                return new PaginatedList<RoleDetailResponseDTO>(roleDetails, totalCount, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception($"Đã xảy ra lỗi khi lấy danh sách phân quyền: {ex.Message}");
            }
        }


        public async Task<RoleDetailResponseDTO?> GetRoleDetailByIdAsync(Guid id)
        {
            return await _dbContext.RoleDetails
                .Where(rd => rd.ID == id)
                .Select(rd => new RoleDetailResponseDTO
                {
                    ID = rd.ID,
                    RoleDetailName = rd.RoleDetailName
                })
                .FirstOrDefaultAsync();
        }

        public async Task<RoleDetailResponseDTO> AddRoleDetailAsync(RoleDetailAddOrUpdateDTO roleDetailDto)
        {
            var newRoleDetail = new Data.Models.RoleDetail
            {
                ID = Guid.NewGuid(),
                RoleDetailName = roleDetailDto.RoleDetailName
            };

            await _dbContext.RoleDetails.AddAsync(newRoleDetail);
            await _dbContext.SaveChangesAsync();

            return new RoleDetailResponseDTO
            {
                ID = newRoleDetail.ID,
                RoleDetailName = newRoleDetail.RoleDetailName
            };
        }

        public async Task<RoleDetailResponseDTO> UpdateRoleDetailAsync(Guid id, RoleDetailAddOrUpdateDTO roleDetailDto)
        {
            var existingRoleDetail = await _dbContext.RoleDetails.FindAsync(id);
            if (existingRoleDetail == null) return null;

            existingRoleDetail.RoleDetailName = roleDetailDto.RoleDetailName;

            _dbContext.RoleDetails.Update(existingRoleDetail);
            await _dbContext.SaveChangesAsync();

            return new RoleDetailResponseDTO
            {
                ID = existingRoleDetail.ID,
                RoleDetailName = existingRoleDetail.RoleDetailName
            };
        }

        public async Task<bool> DeleteRoleDetailAsync(Guid id)
        {
            var roleDetail = await _dbContext.RoleDetails.FindAsync(id);
            if (roleDetail == null)
            {
                return false;
            }

            _dbContext.RoleDetails.Remove(roleDetail);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
