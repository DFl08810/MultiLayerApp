using App.Core.AuthInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Core.Models
{
    public class ViewConfigModel
    {
        //flags if is user logged in site or not
        public bool IsSignedIn { get; set; }
        //flags role of current user
        public bool IsAdmin { get; set; }
        public bool IsUser { get; set; }

    }
}
