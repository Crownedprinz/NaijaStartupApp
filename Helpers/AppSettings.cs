using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaijaStartupApp.Helpers
{
    public class AppSettings
    {
        public string PayStackSecret { get; set; }
        public string PayStackPublic { get; set; }
        public string Secret { get; set; }
        public string AdminMessagingDisplayName { get; set; }
        public string AdminMessagingEmail { get; set; }
    }
}
