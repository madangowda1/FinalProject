//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MINIMVCPROJECT.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MiniProjectBlogsEntities2 : DbContext
    {
        public MiniProjectBlogsEntities2()
            : base("name=MiniProjectBlogsEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BlogsTable> BlogsTables { get; set; }
        public virtual DbSet<UserTable> UserTables { get; set; }
    }
}
