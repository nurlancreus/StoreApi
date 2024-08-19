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

        public Task DeleteAllAsync(string path)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string path, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetFilesAsync(string path)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasFileAsync(string path, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection formFiles)
        {
            throw new NotImplementedException();
        }
    }
}
