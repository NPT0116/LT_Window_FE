using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Models
{
    public enum MediaType
    {
        Image,
        Audio,
        Document,
        Other
    }
    public class Media
    {
        public string FileKey { get; private set; } = String.Empty;
        public string FileName { get; private set; } = String.Empty;
        public string Url { get; private set; } = String.Empty;
        public MediaType MimeType { get; private set; }
        public long Size { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Media(
            string fileKey, 
            string fileName, 
            string url, 
            MediaType mediaType, 
            long? size)
        {
            FileKey = fileKey;
            FileName = fileName;
            Url = url;
            MimeType = mediaType;
            Size = size ?? 0;
            CreatedAt = DateTime.UtcNow;
        }

        public static string GetFileKey(string? prefix, string fileName)
        {
            string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff"); // Timestamp format: 20240225123045987
            string fileExtension = Path.GetExtension(fileName); // Extract file extension
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName); // Extract file name without extension

            string newFileName = $"{fileNameWithoutExt}_{timestamp}{fileExtension}"; // Append timestamp to filename

            return string.IsNullOrEmpty(prefix) ? newFileName : $"{prefix.TrimEnd('/')}/{newFileName}";
        }

        public static string GetMimeType(string fileName)
        {
            var extension = Path.GetExtension(fileName)?.ToLowerInvariant();

            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".webp" => "image/webp",
                ".svg" => "image/svg+xml",
                ".tiff" => "image/tiff",
                ".ico" => "image/x-icon",
                _ => "application/octet-stream" // Default fallback
            };
        }
    }
}
