using AutoMapper;
using Business.DTO.ContenPage;
using Business.Helper;
using Business.Response;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Repository.ContentRepository
{
    public class ContentRepository : IContentRepository
    {
        private readonly ECommerceDBContext _context;
        private readonly IMapper _mapper;
        private readonly FileUpload _fileUpload;
        private readonly LanguageHelper _languageHelper;

        public ContentRepository(ECommerceDBContext context, IMapper mapper, FileUpload fileUpload, LanguageHelper languageHelper)
        {
            _context = context;
            _mapper = mapper;
            _fileUpload = fileUpload;
            _languageHelper = languageHelper;
        }

        public async Task<IEnumerable<ContentPageDTO>> GetAllContentPage()
        {
            var contents = await _context.ContentPages.ToListAsync();
            return contents.Select(c => _mapper.Map<ContentPageDTO>(c));
        }

        public async Task<ContentPageDTO?> GetContentPageById(Guid id)
        {
            var content = await _context.ContentPages.FirstOrDefaultAsync(x => x.ID == id);
            return _mapper.Map<ContentPageDTO>(content);
        }

        public async Task<Response> UpdateContentPage(UpdateContentFileDTO contentPageDTO)
        {
            var language = _languageHelper.GetCurrentLanguage();
            try
            {
                var existingContent = await _context.ContentPages.FirstOrDefaultAsync(x => x.ID == contentPageDTO.ID);
                if (existingContent == null)
                {
                    return new Response
                    {
                        Code = ResponseCode.NotFound,
                        Message = await _languageHelper.GetTranslatedMessage("ERROR_CONTENTPAGE_NOT_FOUND", language)
                    };
                }
                var imageNames = contentPageDTO.ImageNames.Split(',').Select(name => name.Trim()).ToList();

                // Handle file uploads if files are provided
                var imgPath = _fileUpload.UploadFiles(contentPageDTO.Files, "ContentPage",imageNames);

                // Update other properties
                existingContent.PhoneNumber = contentPageDTO.PhoneNumber;
                existingContent.Email = contentPageDTO.Email;
                existingContent.Address = contentPageDTO.Address;
                existingContent.Introduction = contentPageDTO.Introduction;
                existingContent.Description = contentPageDTO.Description;
                existingContent.Video = contentPageDTO.Video;
                existingContent.ImagePaths = imgPath;
                existingContent.ImageNames =contentPageDTO.ImageNames.Split(',').ToList();

                await _context.SaveChangesAsync();

                var updatedDTO = _mapper.Map<UpdateContentFileDTO>(existingContent);

                

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = await _languageHelper.GetTranslatedMessage("UPDATE_CONTENTPAGE_SUCCESS", language)
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = ResponseCode.InternalServerError,
                    Message = await _languageHelper.GetTranslatedMessage("ERROR_UPDATE_CONTENTPAGE", language)

                };
            }
        }
    }
}