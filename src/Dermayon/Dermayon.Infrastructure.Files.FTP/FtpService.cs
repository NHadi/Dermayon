using Dermayon.Common.CrossCutting;
using Dermayon.Infrastructure.Files.FTP.Contracts;
using FluentFTP;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dermayon.Infrastructure.Files.FTP
{
    public class FtpService : IFtpService
    {
        private readonly FtpConfiguration _options;
        private readonly IFtpClient _ftpClient;
        private readonly ILog _log;


        public FtpService(IOptions<FtpConfiguration> options, ILog log)
        {
            _log = log;

            _options = options.Value;

            _ftpClient = new FtpClient(_options.Host, new NetworkCredential(_options.User, _options.Password));

            if (_options.WithSSL)
            {
                _ftpClient.EncryptionMode = FtpEncryptionMode.Explicit;
                _ftpClient.SslProtocols = _options.SslProtocol;
            }

            _ftpClient.Connect();

        }

        private string FtpTemplateMessage(string action = null, string file = null, string effected = null)
            => $"<{action}> File <{file}> on FTP <{_options.Host}> <{effected}>";
        public void Remove(string Path, string Filename)
        {

            try
            {
                if (_ftpClient.DirectoryExists(Path))
                {
                    _ftpClient.RetryAttempts = 10;

                    string fullpath = Path + Filename;

                    _log.Info($"{FtpTemplateMessage("Remove", Filename)}", fullpath);
                    _ftpClient.DeleteFile(fullpath);
                }
                else
                {
                    _log.Info($"{FtpTemplateMessage("Remove", Filename, "not exist")}", Path + Filename);
                    throw new Exception("File Not Exist");
                }
            }
            catch (Exception ex)
            {
                _log.Error($"{FtpTemplateMessage("Remove", Filename, "throw exception")}", ex);
                throw ex;
            }
        }

        public async Task RemoveAsync(string Path, string Filename)
        {

            try
            {
                if (_ftpClient.DirectoryExists(Path))
                {
                    _ftpClient.RetryAttempts = 10;

                    string fullpath = Path + Filename;

                    _log.Info($"{FtpTemplateMessage("Remove", Filename)}", fullpath);
                    await _ftpClient.DeleteFileAsync(fullpath);
                }
                else
                {
                    _log.Info($"{FtpTemplateMessage("Remove", Filename, "not exist")}", Path + Filename);
                    throw new Exception("File Not Exist");
                }
            }
            catch (Exception ex)
            {
                _log.Error($"{FtpTemplateMessage("Remove", Filename, "throw exception")}", ex);
                throw ex;
            }
        }
        public byte[] Download(string Path, string Filename)
        {
            try
            {
                if (_ftpClient.DirectoryExists(Path))
                {
                    _ftpClient.RetryAttempts = 10;

                    string fullpath = Path + Filename;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        _log.Info($"{FtpTemplateMessage("Download", Filename, "Downloading")}", fullpath);
                        _ftpClient.Download(ms, fullpath, 0);

                        return ms.ToArray();
                    }
                }
                else
                {
                    _log.Info($"{FtpTemplateMessage("Download", Filename, "Not Exist")}", Path + Filename);
                    throw new Exception("File Not Exist");
                }
            }
            catch (Exception ex)
            {
                _log.Error($"{FtpTemplateMessage("Download", Filename, "throw exception")}", ex);
                throw ex;
            }
        }

        public async Task<byte[]> DownloadAsync(string Path, string Filename)
        {
            try
            {
                if (await _ftpClient.DirectoryExistsAsync(Path))
                {
                    _ftpClient.RetryAttempts = 10;

                    string fullpath = Path + Filename;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        _log.Info($"{FtpTemplateMessage("Download", Filename, "Downloading")}", fullpath);
                        await _ftpClient.DownloadAsync(ms, fullpath, 0);

                        return ms.ToArray();
                    }
                }
                else
                {
                    _log.Info($"{FtpTemplateMessage("Download", Filename, "Not Exist")}", Path + Filename);
                    throw new Exception("File Not Exist");
                }
            }
            catch (Exception ex)
            {
                _log.Error($"{FtpTemplateMessage("Download", Filename, "throw exception")}", ex);
                throw ex;
            }
        }

        public async Task<bool> DownloadToLocalAsync(string localPath, string Path, string Filename)
        {
            try
            {
                if (await _ftpClient.DirectoryExistsAsync(Path))
                {
                    _ftpClient.RetryAttempts = 10;

                    string fullpath = Path + Filename;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        _log.Info($"{FtpTemplateMessage("DownloadLocal", Filename, "Downloading")}", fullpath);
                        return await _ftpClient.DownloadFileAsync(localPath, fullpath, FtpLocalExists.Overwrite);
                    }
                }
                else
                {
                    _log.Info($"{FtpTemplateMessage("DownloadLocal", Filename, "Not Exist")}", Path + Filename);
                    throw new Exception("File Not Exist");
                }
            }
            catch (Exception ex)
            {
                _log.Error($"{FtpTemplateMessage("DownloadLocal", Filename, "throw exception")}", ex);
                throw ex;
            }
        }

        public bool DownloadToLocal(string localPath, string Path, string Filename)
        {
            try
            {
                if (_ftpClient.DirectoryExists(Path))
                {
                    _ftpClient.RetryAttempts = 10;

                    string fullpath = Path + Filename;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        _log.Info($"{FtpTemplateMessage("DownloadLocal", Filename, "Downloading")}", fullpath);
                        return _ftpClient.DownloadFile(localPath, fullpath, FtpLocalExists.Overwrite);
                    }
                }
                else
                {
                    _log.Info($"{FtpTemplateMessage("DownloadLocal", Filename, "Not Exist")}", Path + Filename);
                    throw new Exception("File Not Exist");
                }
            }
            catch (Exception ex)
            {
                _log.Error($"{FtpTemplateMessage("DownloadLocal", Filename, "throw exception")}", ex);
                throw ex;
            }
        }

        public bool Upload(byte[] uploadedFile, string Path, string Filename, bool withCreateDirectory = false)
        {
            try
            {
                _ftpClient.RetryAttempts = 10;

                string fullpath = Path + Filename;

                if (_ftpClient.DirectoryExists(Path) == false)
                {
                    if (withCreateDirectory == true)
                    {
                        _ftpClient.CreateDirectory(Path);
                    }

                }

                using (MemoryStream ms = new MemoryStream())
                {
                    _log.Info($"Uploading File <{Filename}> on FTP <{_options.Host}>", fullpath);
                    return _ftpClient.Upload(uploadedFile, fullpath, FtpExists.Overwrite, false);
                }
            }
            catch (Exception ex)
            {
                _log.Error($"{FtpTemplateMessage("DownloadLocal", Filename, "throw exception")}", ex);
                throw ex;
            }
        }
        public async Task<bool> UploadAsync(byte[] uploadedFile, string Path, string Filename, bool withCreateDirectory = false)
        {
            try
            {
                _ftpClient.RetryAttempts = 10;

                string fullpath = Path + Filename;

                if (await _ftpClient.DirectoryExistsAsync(Path) == false)
                {
                    if (withCreateDirectory == true)
                    {
                        _ftpClient.CreateDirectory(Path);
                    }

                }

                using (MemoryStream ms = new MemoryStream())
                {
                    _log.Info($"Uploading File <{Filename}> on FTP <{_options.Host}>", fullpath);
                    return await _ftpClient.UploadAsync(uploadedFile, fullpath, FtpExists.Overwrite, false);
                }
            }
            catch (Exception ex)
            {
                _log.Error($"{FtpTemplateMessage("DownloadLocal", Filename, "throw exception")}", ex);
                throw ex;
            }
        }
        public string GetCheckSum(string path, string fileName, FtpHashAlgorithm algorithm)
        {
            try
            {
                string result = string.Empty;

                FtpHash remoteHash = _ftpClient.GetChecksum(path + fileName);

                if (remoteHash.IsValid)
                {
                    result = remoteHash.Value;
                }

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<string> GetCheckSumAsync(string path, string fileName, FtpHashAlgorithm algorithm)
        {
            try
            {
                string result = string.Empty;

                FtpHash remoteHash = await _ftpClient.GetChecksumAsync(path + fileName);

                if (remoteHash.IsValid)
                {
                    result = remoteHash.Value;
                }

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Exist(string Path, string Filename)
        {
            try
            {
                if (_ftpClient.FileExists(Path + Filename))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _log.Error($"{FtpTemplateMessage("Exit", Filename, "throw exception")}", ex);
                return false;
            }
        }
        public async Task<bool> ExistAsync(string Path, string Filename)
        {
            try
            {
                if (await _ftpClient.FileExistsAsync(Path + Filename))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _log.Error($"{FtpTemplateMessage("Exit", Filename, "throw exception")}", ex);
                return false;
            }
        }
    }
}
