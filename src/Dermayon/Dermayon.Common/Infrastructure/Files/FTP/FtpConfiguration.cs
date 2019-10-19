using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;

namespace Dermayon.Common.Infrastructure.Files.FTP
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
