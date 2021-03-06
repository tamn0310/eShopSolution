﻿using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace eShopSolution.Api.Application.Handler.Common
{
    public class FileStorageService : IStorageService
    {
        private readonly string _userContentFolder;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
           this._userContentFolder = Path.Combine(webHostEnvironment.WebRootPath, USER_CONTENT_FOLDER_NAME);
        }


        /// <summary>
        /// Lấy đường dẫn file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetFileUrl(string fileName)
        {
            return $"/{USER_CONTENT_FOLDER_NAME}/{fileName}";
        }

        /// <summary>
        /// Lưu file
        /// </summary>
        /// <param name="mediaBinaryStream"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }

        /// <summary>
        /// Xóa file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
    }
}