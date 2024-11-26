using AutoMapper;
using Business.DTO.ContenPage;
using Business.Response;
using Business.Validation;
using Microsoft.AspNetCore.Mvc;
using Repository.ContentRepository;

namespace DTS_ToyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentPageController : ControllerBase
    {
        private readonly IContentRepository _contentRepository;
        private readonly IMapper _mapper;

        public ContentPageController(IContentRepository contentRepository, IMapper mapper)
        {
            _contentRepository = contentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContentPageDTO>>> GetAllContentPage()
        {
            try
            {
                var content = await _contentRepository.GetAllContentPage();
                return Ok(new Response
                {
                    Code = ResponseCode.Success,
                    Message = "Get all content pages successfully",
                    Data = content
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetContentPageById(Guid id)
        {
            try
            {
                var content = await _contentRepository.GetContentPageById(id);
                if (content == null)
                {
                    return NotFound(new Response
                    {
                        Code = ResponseCode.NotFound,
                        Message = "Content page not found"
                    });
                }

                return Ok(new Response
                {
                    Code = ResponseCode.Success,
                    Message = "Get content page successfully",
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = ex.Message
                });
            }
        }

        [HttpPut]
        [Consumes("multipart/form-data")] // Specify that this endpoint accepts form data
        public async Task<ActionResult<Response>> UpdateContentPage([FromForm] UpdateContentFileDTO contentPageDTO)
        {
            try
            {
                // Validate input
                if (contentPageDTO == null)
                {
                    return BadRequest(new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = "Content page data cannot be null"
                    });
                }

                if (contentPageDTO.ID == Guid.Empty)
                {
                    return BadRequest(new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = "Invalid ID"
                    });
                }

                // Validate file uploads
                if (contentPageDTO.Files != null && contentPageDTO.Files.Any())
                {
                    foreach (var file in contentPageDTO.Files)
                    {
                        // Check file size (e.g., max 5MB)
                        if (file.Length > 5 * 1024 * 1024)
                        {
                            return BadRequest(new Response
                            {
                                Code = ResponseCode.BadRequest,
                                Message = $"File {file.FileName} is too large. Maximum size is 5MB"
                            });
                        }

                        // Check file type
                        var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                        if (!allowedTypes.Contains(file.ContentType.ToLower()))
                        {
                            return BadRequest(new Response
                            {
                                Code = ResponseCode.BadRequest,
                                Message = $"File {file.FileName} has invalid format. Allowed formats are JPEG, PNG, and GIF"
                            });
                        }
                    }
                }

                // Validate phone number if provided
                if (!string.IsNullOrEmpty(contentPageDTO.PhoneNumber) &&
                    !ValidationHelper.IsValidPhoneNumber(contentPageDTO.PhoneNumber))
                {
                    return BadRequest(new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = "Invalid phone number format. Must be a valid Vietnamese phone number"
                    });
                }

                // Validate email if provided
                if (!string.IsNullOrEmpty(contentPageDTO.Email) &&
                    !ValidationHelper.IsValidEmail(contentPageDTO.Email))
                {
                    return BadRequest(new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = "Invalid email format"
                    });
                }

                // Validate video URL if provided
                if (!string.IsNullOrEmpty(contentPageDTO.Video) &&
                    !ValidationHelper.IsValidUrl(contentPageDTO.Video))
                {
                    return BadRequest(new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = "Invalid video URL format"
                    });
                }

                // Validate text lengths
                if (!string.IsNullOrEmpty(contentPageDTO.Description) &&
                    !ValidationHelper.IsValidLength(contentPageDTO.Description, 1, 2000))
                {
                    return BadRequest(new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = "Description must be between 1 and 2000 characters"
                    });
                }

                if (!string.IsNullOrEmpty(contentPageDTO.Introduction) &&
                    !ValidationHelper.IsValidLength(contentPageDTO.Introduction, 1, 1000))
                {
                    return BadRequest(new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = "Introduction must be between 1 and 1000 characters"
                    });
                }

                if (!string.IsNullOrEmpty(contentPageDTO.Address) &&
                    !ValidationHelper.IsValidLength(contentPageDTO.Address, 1, 500))
                {
                    return BadRequest(new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = "Address must be between 1 and 500 characters"
                    });
                }

                var response = await _contentRepository.UpdateContentPage(contentPageDTO);
                if (response.Code == ResponseCode.Success)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = ex.Message
                });
            }
        }
    }
}