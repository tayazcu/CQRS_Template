﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Framework
{
    public class SiteSettings
    {
        public string PasswordHasherKey { get; set; }
        public JwtSettings JwtSettings { get; set; }
        public IdentitySettings IdentitySettings { get; set; }
        public ConnectionStringSetting ConnectionStringSettings { get; set; }
        public ApplicationSetting ApplicationSettings { get; set; }
    }
    public class IdentitySettings
    {
        public bool PasswordRequireDigit { get; set; }
        public int PasswordRequiredLength { get; set; }
        public bool PasswordRequireNonAlphanumic { get; set; }
        public bool PasswordRequireUppercase { get; set; }
        public bool PasswordRequireLowercase { get; set; }
        public bool RequireUniqueEmail { get; set; }
    }
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Encryptkey { get; set; }
        public string Issuer { get; set; }
        //public IEnumerable<string> Issuer { get; set; }
        public string Audience { get; set; }
        public int NotBeforeMinutes { get; set; }
        public int ExpirationMinutes { get; set; }
    }
    public class ConnectionStringSetting
    {
        public string WriteDatabaseConnectionString { get; set; }
        public string ReadDatabaseConnectionString { get; set; }
        public string ElmahDatabaseConnectionString { get; set; }
    }
    public class ApplicationSetting
    {
        public bool ActivateElmah { get; set; }
        public string ElmahPath { get; set; }
        public bool ActivateSwagger { get; set; }
        public List<string> CorsEnableUris { get; set; }
    }
}
