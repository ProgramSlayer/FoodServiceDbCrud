//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfCrud.DbModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserAccount
    {
        public int Id { get; set; }
        public int UserRoleId { get; set; }
        public string Login { get; set; }
        public byte[] Password { get; set; }
        public byte[] Image { get; set; }
    
        public virtual UserRole UserRole { get; set; }
    }
}
