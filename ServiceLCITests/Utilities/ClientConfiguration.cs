using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLCITests.Utilities
{
    public partial class ClientConfiguration
    {
        public static ClientConfiguration Default { get { return ClientConfiguration.LciClientConfig; } }



        public static ClientConfiguration LciClientConfig = new ClientConfiguration()

        {

            // You only need to populate this section if you are logging on via a native app. For Service to Service scenarios in which you e.g. use a service principal you don't need that.

            UriString = "https://usnconeboxax1aos.cloud.onebox.dynamics.com/",

            UserName = "0oa34vdqadadfHSV3356",

            // Insert the correct password here for the actual test.

            Password = "EUa-lupZgdmhksGuVOhWh4L7Cjd_ldChikgKoGLU",



            // You need this only if you logon via service principal using a client secret. 


            ActiveDirectoryResource = "https://usnconeboxax1aos.cloud.onebox.dynamics.com", // Don't have a trailing "/". Note: Some of the sample code handles that issue.

            ActiveDirectoryTenant = "https://login.windows-ppe.net/TAEOfficial.ccsctp.net", // Some samples: https://login.windows.net/yourtenant.onmicrosoft.com, https://login.windows.net/microsoft.com

            ActiveDirectoryClientAppId = "d8a9a121-b463-41f6-a86c-041272bdb340",

            ActiveDirectoryClientAppSecret = "",

        };


        public string UriString { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ActiveDirectoryResource { get; set; }

        public string ActiveDirectoryTenant { get; set; }

        public string ActiveDirectoryClientAppId { get; set; }

        public string ActiveDirectoryClientAppSecret { get; set; }
    }
}
