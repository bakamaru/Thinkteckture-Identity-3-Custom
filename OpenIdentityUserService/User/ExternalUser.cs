﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OpenIdentityCustomService
{
   
    public class ExternalUser
    {
        public string Subject { get; set; }
        public string Provider { get; set; }
        public string ProviderID { get; set; }
        public List<Claim> Claims { get; set; }
    }
}