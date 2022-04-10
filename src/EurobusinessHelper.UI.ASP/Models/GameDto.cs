using System;

namespace EurobusinessHelper.UI.ASP.Models
{
    public partial class GameDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public UserDto CreatedBy { get; set; }
    }
}