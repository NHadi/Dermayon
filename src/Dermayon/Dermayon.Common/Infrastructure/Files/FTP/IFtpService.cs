using FluentFTP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dermayon.Common.Infrastructure.Files.FTP
{
    public interface IFtpService
    {
        Task<bool> ExistAsync(string Path, string Filename);
        bool Exist(string Path, string Filename);
        byte[] Download(string Path, string Filename);
        Task<byte[]> DownloadAsync(string Path, string Filename);
        bool DownloadToLocal(string localPath, string Path, string Filename);
        bool Upload(byte[] uploadedFile, string Path, string Filename, bool withCreateDirectory = false);
        Task<bool> UploadAsync(byte[] uploadedFile, string Path, string Filename, bool withCreateDirectory = false)
        void Remove(string Path, string Filename);
        Task RemoveAsync(string Path, string Filename);
        string GetCheckSum(string path, string fileName, FtpHashAlgorithm algorithm);
        Task<string> GetCheckSumAsync(string path, string fileName, FtpHashAlgorithm algorithm);
    }
}
