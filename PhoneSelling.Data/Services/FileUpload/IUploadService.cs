using PhoneSelling.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Services.FileUpload
{
    public record MediaUploadRequest(
        string Prefix,
        byte[] FileData,
        string FileName,
        string ContentType
    );
    public interface IUploadService
    {
        Task<bool> DeleteFileAsync(string fileKey, CancellationToken cancellationToken);
        Task<Media> UploadFileAsync(MediaUploadRequest request, CancellationToken cancellationToken);

    }
}
