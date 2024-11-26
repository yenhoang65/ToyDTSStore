using Business.DTO;
using Business.DTO.ContenPage;
using Business.Response;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ContentRepository
{
    public interface IContentRepository
    {
        Task<IEnumerable<ContentPageDTO>> GetAllContentPage();
        Task<Response> UpdateContentPage(UpdateContentFileDTO contentPageDTO);
        Task<ContentPageDTO?> GetContentPageById(Guid id);
    }
}
