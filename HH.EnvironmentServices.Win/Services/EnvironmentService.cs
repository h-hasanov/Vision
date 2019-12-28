using System;
using System.Diagnostics;
using System.Net.Mime;
using System.Security.Principal;
using HH.EnvironmentServices.Interfaces;

namespace HH.EnvironmentServices.Win.Services
{
    [DebuggerNonUserCode]
    public class EnvironmentService : IEnvironmentService
    {
        public string UserDomainName { get { return Environment.UserDomainName; } }
        
        public string UserName { get { return Environment.UserName; } }

        public string CurrentPrincipalName
        {
            get
            {
                return WindowsIdentity.GetCurrent().Name;
            }
        }
    }
}
