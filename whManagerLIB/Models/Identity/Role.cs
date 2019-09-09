using System;
using System.Collections.Generic;
using System.Text;

namespace whManagerLIB.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Permission> Permissions { get; set; }
    }
}
