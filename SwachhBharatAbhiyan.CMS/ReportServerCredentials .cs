using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace SwachhBharatAbhiyan.CMS
{
    public class ReportServerCredentials : IReportServerCredentials
    {
        private string _username;
        private string _password;
        private string _domain;

        public ReportServerCredentials(string userName, string password, string domain)
        {
            _username = userName;
            _password = password;
            _domain = domain;
        }

        public bool GetFormsCredentials(out Cookie authCookie, out string userName, out string password, out string authority)
        {
            authCookie = null;
            userName = null;
            password = null;
            authority = null;
            return false;
        }

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get
            {
                return null;
            }
        }

        public System.Net.ICredentials NetworkCredentials
        {
            get
            {
                return new NetworkCredential(_username, _password, _domain);
            }
        }
    }
}