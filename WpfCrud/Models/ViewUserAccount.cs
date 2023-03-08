using WpfCrud.Models.Enums;

namespace WpfCrud.Models
{
    public class ViewUserAccount
    {
        public ViewUserAccount(int id, int roleId, string roleName, string login, byte[] image)
        {
            Id = id;
            RoleId = roleId;
            RoleName = roleName;
            Login = login;
            Image = image;
        }

        public int Id { get; }
        public int RoleId { get; }
        public UserRoleEnum Role => (UserRoleEnum)RoleId;
        public string RoleName { get; }
        public string Login { get; }
        public byte[] Image { get; }
    }
}
