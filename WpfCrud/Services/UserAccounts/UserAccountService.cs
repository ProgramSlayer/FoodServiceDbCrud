using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WpfCrud.DbModels;
using WpfCrud.Models;

namespace WpfCrud.Services.UserAccounts
{
    public class UserAccountService
    {
        private readonly Func<FoodServiceDbContext> _factory = () => new FoodServiceDbContext();

        public UserAccountService()
        {
        }

        public UserAccountService(Func<FoodServiceDbContext> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task<IEnumerable<ViewUserAccount>> GetViewUserAccountsAsync()
        {
            using (var context = _factory())
            {
                var dbResults = await (
                    from ua in context.UserAccounts
                    join ur in context.UserRoles on ua.UserRoleId equals ur.Id
                    select new
                    {
                        ua.Id,
                        RoleId = ur.Id,
                        RoleName = ur.Name,
                        ua.Login,
                        ua.Image,
                    }).ToListAsync();
                var results = from dbr in dbResults
                              select new ViewUserAccount(
                                  dbr.Id,
                                  dbr.RoleId,
                                  dbr.RoleName,
                                  dbr.Login,
                                  dbr.Image);
                return results;
            }
        }

        public async Task<EditableUserAccount> AddUserAccountAsync(EditableUserAccount userAccount)
        {
            if (userAccount is null)
            {
                throw new ArgumentNullException(nameof(userAccount));
            }

            using (var context = _factory())
            {
                var loginAlreadyInBase = await context.UserAccounts.AnyAsync(ua => ua.Login == userAccount.Login);
                
                if (loginAlreadyInBase)
                {
                    throw new Exception($"Пользователь с логином <{userAccount.Login}> уже есть в базе данных!");
                }

                var dbUserAccount = new UserAccount()
                {
                    Login = userAccount.Login,
                    Password = GetPasswordUtf8Sha512Hash(userAccount.Password),
                    UserRoleId = (int)userAccount.Role,
                    Image = userAccount.Image,
                };

                context.UserAccounts.Add(dbUserAccount);
                await context.SaveChangesAsync();
                userAccount.Id = dbUserAccount.Id;
                return userAccount;
            }
        }

        public async Task UpdateUserAccountAsync(EditableUserAccount userAccount)
        {
            if (userAccount is null)
            {
                throw new ArgumentNullException(nameof(userAccount));
            }

            using (var context = _factory())
            {
                var dbUserAccount = await context.UserAccounts.SingleOrDefaultAsync(a => a.Id == userAccount.Id)
                    ?? throw new Exception($"Пользователь (Id = {userAccount.Id}) не найден в базе данных!");
                dbUserAccount.Login = userAccount.Login;
                dbUserAccount.Password = GetPasswordUtf8Sha512Hash(userAccount.Password);
                dbUserAccount.UserRoleId = (int)userAccount.Role;
                dbUserAccount.Image = userAccount.Image;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteUserAccountAsync(int id)
        {
            using (var context = _factory())
            {
                var dbUserAccount = await context.UserAccounts.SingleOrDefaultAsync(a => a.Id == id)
                    ?? throw new Exception($"Пользователь (Id = {id}) не найден в базе данных!");
                context.UserAccounts.Remove(dbUserAccount);
                await context.SaveChangesAsync();
            }
        }

        public static byte[] GetPasswordUtf8Sha512Hash(string password)
        {
            if (password is null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            var utf8Bytes = Encoding.UTF8.GetBytes(password);
            using (var sha512 = new SHA512Managed())
            {
                var hash = sha512.ComputeHash(utf8Bytes);
                return hash;
            }
        }
    }
}
