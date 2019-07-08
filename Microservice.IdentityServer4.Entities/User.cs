using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Microservice.IdentityServer4.Entities
{
    [Table("MUsers")]
    public class User
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsDelete { get; set; }
    }
}
