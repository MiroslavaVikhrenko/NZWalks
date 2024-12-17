using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        //inject IImageRepository
        private readonly IImageRepository imageRepository;        
        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        //POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            //Validate if this request is correct or not
            ValidateFileUpload(request);

            if (ModelState.IsValid)
            {
                //Convert DTO to Domain Model
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription //FilePath will not be here, it will be added in Repository instead
                };

                //Use Repository to upload image
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            //Validate extension and size
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                //pass the key as 'file' and error message
                ModelState.AddModelError("file", "Unsupported file extension");
            }
            //10 MB = 10485760 Bytes
            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10 MB, please upload a smaller size file.");
            }
        }
    }
}
