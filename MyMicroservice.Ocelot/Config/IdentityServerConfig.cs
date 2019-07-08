using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMicroservice.Ocelot.Config
{
    public class IdentityServerConfig
    {
        public string AuthorityIP { get; set; }
        public int Port { get; set; }
        public string IdentityScheme { get; set; }
        public List<APIResource> Resources { get; set; }
    }

    public class APIResource
    {
        public string Key { get; set; }
        public string Name { get; set; }
    }
}
