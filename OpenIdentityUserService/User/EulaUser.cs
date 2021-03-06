﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OpenIdentityCustomService.User
{
   public class EulaUser
    {
        public string Subject { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool AcceptedEula { get; set; }
        public List<Claim> Claims { get; set; }
    }
}
