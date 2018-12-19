using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBuildMovie.Models
{
    public class AppUser : IdentityUser
    {
        public string AstrologicalSign { get; set; }
        public DateTime Birthday { get; set; }

    }
}