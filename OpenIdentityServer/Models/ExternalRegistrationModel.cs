using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpenIdentityServer.Models
{
    public class ExternalRegistrationModel
    {
        [Required]
        public string First { get; set; }
        [Required]
        public string Last { get; set; }
    }
}