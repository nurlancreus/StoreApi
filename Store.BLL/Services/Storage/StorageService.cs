using Microsoft.AspNetCore.Http;
using Store.Core.Abstractions.Services.Storage;
using Store.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.Services.Storage
{
    public class StorageService : IStorageService
    {
        private readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public StorageType StorageName
        {
            get
            {
                var name = Enum.Parse<StorageType>(_storage.GetType().Name.Replace("Storage", string.Empty));

                return name;
            }
        }

        public async Task DeleteAllAsync(string path)
        {
            await _storage.DeleteAllAsync(path);
        }

        public async Task DeleteAsync(string path, string fileName)
        {
            await _storage.DeleteAsync(path, fileName);
        }

        public async Task DeleteByUrlAsync(string url)
        {
            await _storage.DeleteByUrlAsync(url);
        }

        public async Task<List<string>> GetFilesAsync(string path)
        {
            return await _storage.GetFilesAsync(path);
        }

        public async Task<string> GetUploadedFileUrlAsync(string path, string fileName)
        {
            return await _storage.GetUploadedFileUrlAsync(path, fileName);
        }

        public async Task<bool> HasFileAsync(string path, string fileName)
        {
            return await _storage.HasFileAsync(path, fileName);
        }

        public Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection formFiles)
        {
            return _storage.UploadAsync(path, formFiles);
        }
    }
}
