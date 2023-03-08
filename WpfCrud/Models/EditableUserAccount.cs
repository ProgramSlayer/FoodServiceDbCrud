using WpfCrud.Models.Enums;

namespace WpfCrud.Models
{
    public class EditableUserAccount
    {
        public EditableUserAccount()
        {
        }

        public EditableUserAccount(int id, string login, string password, UserRoleEnum role, byte[] image)
        {
            Id = id;
            Login = login;
            Password = password;
            Role = role;
            Image = image;
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserRoleEnum Role { get; set; } = UserRoleEnum.Cook;
        public byte[] Image { get; set; }
    }
}
