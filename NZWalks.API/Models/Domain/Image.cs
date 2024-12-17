using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }
        [NotMapped] //excluded from db mapping because we do not want to store this file inside the db
                    //comes from System.ComponentModel.DataAnnotations.Schema
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; } //optional property
        public string FileExtension { get; set; } //.jpg, .png etc
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }
    }
}
