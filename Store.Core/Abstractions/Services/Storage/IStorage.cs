using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Abstractions.Services.Storage
{
    public interface IStorage
    {
        Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection formFiles);
        Task DeleteAsync(string path, string fileName);
        Task DeleteAllAsync(string path);
        Task<List<string>> GetFilesAsync(string path);
        Task<bool> HasFileAsync(string path, string fileName);
    }
}
