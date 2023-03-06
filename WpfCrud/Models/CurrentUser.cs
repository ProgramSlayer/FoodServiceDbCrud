using WpfCrud.Models.Enums;

namespace WpfCrud.Models
{
    public class CurrentUser
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public UserRoleEnum Role { get; set; }
    }
}
