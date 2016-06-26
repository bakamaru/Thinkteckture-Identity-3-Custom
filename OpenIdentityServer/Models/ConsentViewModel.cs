using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;

namespace OpenIdentityServer.Models
{
    public class PermissionViewModel
    {
        public string ErrorMessage { get; set; }

        public ClientPermission ClientPermission { get; set; } 

    }

    public class ConsentViewModel
    {

        public string ClientlogoUrl { get; set; }
        public string ClientName { get; set; }
        public string ErrorMessage { get; set; }
        public bool RememberConsent { get; set; }

        public List<Scope> Scopes { get; set; }
        public List<Scope> ApplicationScopes { get; set; }
    }

}
