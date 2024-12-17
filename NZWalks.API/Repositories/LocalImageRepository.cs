using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    //we are uploading images to a local solution or a local path and not to any third party solutions for example Cloudinary
    //we want to store/upload images in 'Images' folder 
    public class LocalImageRepository : IImageRepository
    {
        //Inject IWebHostEnvironment - to work with a local path variable in Upload()
        private readonly IWebHostEnvironment webHostEnvironment;
        //Inject IHttpContextAccessor - to create a variable or path to the image that we upload
        private readonly IHttpContextAccessor httpContextAccessor;
        //Inject NZWalksDbContext - to save changes to the db
        private readonly NZWalksDbContext dbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            NZWalksDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<Image> Upload(Image image)
        {
            //create a local path variable which points to the 'Images' folder
            //first it goes to this location: webHostEnvironment.ContentRootPath,
            //then to "Images" folder, and then it needs the file info (image.FileName)
            //we also need to provide the extension, which isn't part of the name, but it's part of the image domain model (image.FileExtension)
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", 
                $"{image.FileName}{image.FileExtension}");

            //Upload Image to Local Path
            //open a file stream object so that we can copy this file that we are receiving in the form of the IFormFile
            //and upload this to this location
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream); //by this line we should have an image inside 'Images' folder

            //Save the changes to the db, it needs to point to URL of an app that is getting served online
            //For example, we are running our local host
            // https://localhost:1234/images/image.jpg
            //location like this should be able to serve an image
            //httpContextAccessor.HttpContext.Request.Scheme => http or https 
            //httpContextAccessor.HttpContext.Request.Host => application url (host)
            //httpContextAccessor.HttpContext.Request.PathBase => so far this part: https://localhost:1234
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            //this path will be uploaded to the table and returned to the user as well

            //fill the image domain model
            image.FilePath = urlFilePath;

            //save changes to db, add Image to the Images table
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        }
    }
}
