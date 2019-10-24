using System;
using System.Security.Authentication;

namespace Dermayon.Infrastructure.Files.FTP
{
    public class FtpConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public SslProtocols SslProtocol { get; set; }
        public bool WithSSL { get; set; }
    }
}
