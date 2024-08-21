﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Store.BLL.Helpers;
using Store.Core.Abstractions.Services.Storage;
using Store.Core.Abstractions.Services.Storage.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.Services.Storage.Local
{
    public class LocalStorage : ILocalStorage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task DeleteAsync(string path, string fileName)
        {

            if (await HasFileAsync(path, fileName))
                File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, path, fileName));

            await Task.CompletedTask;
        }


        public async Task<List<string>> GetFilesAsync(string path)
        {
            DirectoryInfo directory = new(Path.Combine(_webHostEnvironment.WebRootPath, path));
            return await Task.FromResult(directory.GetFiles().Select(f => f.Name).ToList());
        }

        public async Task<bool> HasFileAsync(string path, string fileName)
        {
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, path, fileName);
            return await Task.FromResult(File.Exists(filePath));
        }

        private static async Task<bool> CopyFileAsync(string path, IFormFile formFile)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: true);
                await formFile.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging
                throw new Exception($"Error copying file: {ex.Message}");
                // return false;
            }
        }

        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection formFiles)
        {
            if (formFiles.Count == 0)
            {
                throw new ArgumentException("No files uploaded.");
            }

            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            await FileHelpers.EnsureDirectoryExists(uploadPath);

            var uploadResults = new List<(string fileName, string path)>();

            foreach (IFormFile formFile in formFiles)
            {
                string newFileName = await FileHelpers.RenameFileAsync(path, formFile.FileName, HasFileAsync);
                string fullPath = Path.Combine(uploadPath, newFileName);

                bool isCopied = await CopyFileAsync(fullPath, formFile);
                if (!isCopied)
                {
                    // Optionally, you could delete all uploaded files in case of failure.
                    await FileHelpers.CleanupFailedUploads(uploadResults);
                    throw new Exception("File upload failed.");
                }

                uploadResults.Add((newFileName, path));
            }

            return uploadResults;
        }

        Task IStorage.DeleteAllAsync(string pathOrContainerName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUploadedFileUrlAsync(string path, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByUrlAsync(string url)
        {
            throw new NotImplementedException();
        }
    }
}
