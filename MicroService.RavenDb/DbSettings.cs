using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.RavenDb
{
    public class DbSettings
    {
        public string Url { get; set; }
        public string DatabaseName { get; set; }
        public string CertName { get; set; }
        public string CertPassword { get; set; }
    }
}
