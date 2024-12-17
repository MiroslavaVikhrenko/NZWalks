using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        //POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            //Validate if this request is correct or not
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
