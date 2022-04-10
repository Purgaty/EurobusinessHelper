using System;

namespace EurobusinessHelper.UI.ASP.Models
{
    public partial class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}