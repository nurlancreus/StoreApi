using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Extensions
{
    public static class FormFileExtensions
    {

        public static bool IsImage(this IFormFile formFile)
        {
            return formFile.ContentType.Contains("image");
        }

        public static bool IsSizeOk(this IFormFile formFile, int mb)
        {
            // Convert file length from bytes to megabytes
            double fileSizeInMB = formFile.Length / (1024.0 * 1024.0);
            return fileSizeInMB <= mb;
        }

        public static bool RestrictExtension(this IFormFile formFile, string[]? permittedExtensions = null)
        {
            permittedExtensions ??= [".jpg", ".png", ".gif"];

            string extension = Path.GetExtension(formFile.FileName).ToLowerInvariant();
            return !string.IsNullOrEmpty(extension) && permittedExtensions.Contains(extension);

        }

        public static bool RestrictMimeTypes(this IFormFile formFile, string[]? permittedMimeTypes = null)
        {
            permittedMimeTypes ??= ["image/jpeg", "image/png", "image/gif"];

            string mimeType = formFile.ContentType;
            return permittedMimeTypes.Contains(mimeType);

        }
    }
}
