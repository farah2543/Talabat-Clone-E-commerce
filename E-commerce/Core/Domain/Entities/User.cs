using Microsoft.AspNetCore.Identity;
using System.Net.Sockets;

namespace Domain.Entities
{
    public class User : IdentityUser
    { 
        public string DisplayName  { get; set; }

        public Address Address { get; set; }

    }
}
