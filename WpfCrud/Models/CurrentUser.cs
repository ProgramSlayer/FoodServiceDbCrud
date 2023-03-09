using WpfCrud.Models.Enums;

namespace WpfCrud.Models
{
    /// <summary>
    /// Информация о текущем пользователе системы.
    /// </summary>
    public class CurrentUser
    {
        /// <summary>
        /// Идентификатор учётной записи.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя (логин) учётной записи.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Роль пользователя в системе.
        /// </summary>
        public UserRoleEnum Role { get; set; }
    }
}
