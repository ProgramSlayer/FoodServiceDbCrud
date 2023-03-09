namespace WpfCrud.Models.Enums
{
    /// <summary>
    /// Роли пользователей в системе.
    /// </summary>
    public enum UserRoleEnum
    {
        /// <summary>
        /// Неавторизованный пользователь.
        /// </summary>
        Unauthorized = 0,
        /// <summary>
        /// Администратор.
        /// </summary>
        Admin = 1,
        /// <summary>
        /// Шеф-повар.
        /// </summary>
        Chef = 2,
        /// <summary>
        /// Повар.
        /// </summary>
        Cook = 3,
    }
}
