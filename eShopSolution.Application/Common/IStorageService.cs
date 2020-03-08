using System.IO;
using System.Threading.Tasks;

namespace eShopSolution.Application.Common
{
    /// <summary>
    /// Làm việc với file
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// Get đường dẫn file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        string GetFileUrl(string fileName);

        /// <summary>
        /// Lưu File
        /// </summary>
        /// <param name="mediaBinaryStream"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Task SaveFileAsync(Stream mediaBinaryStream, string fileName);

        /// <summary>
        /// Xóa file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Task DeleteFileAsync(string fileName);
    }
}