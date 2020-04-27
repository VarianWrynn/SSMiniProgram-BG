using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Model.POCOs;

namespace DAL.Repository
{
    /// <summary>
    /// This class will be responsible for mirroring the database tables into Class objects.
    /// https://dev.to/lucianopereira86/net-core-3-efcore-4jg5
    /// </summary>
    public class DBContext:DbContext
    {
        /// <summary>
        /// This object will be used to manipulate the data from the table.
        /// </summary>
        public DbSet<LeeTest> leeTest { get; set; }

        public DbSet<Journal> journal { get; set; }

        /* 注意： 构造函数 DBContext 传递进去的类型必须是 DBContext，而不是小写的DbContext，否则就会报错
         InvalidOperationException: Unable to resolve service for type 'Microsoft.EntityFrameworkCore.
         DbContextOptions`1[Microsoft.EntityFrameworkCore.DbContext]' while attempting to activate 
         'DAL.Repository.DBContext'. 2020-4-11 22:14:59 
       
          参考： https://github.com/dotnet/efcore/issues/15145
         */
        //public DBContext(DbContextOptions<DbContext> options):base(options)
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        /// <summary>
        /// Inside the OnModelCreating, you need to mirror each model with its respective table.
        /// </summary>  
        /// <param name="modelBilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBilder)
        {
            modelBilder.Entity<LeeTest>(e =>
            {
                e
                .ToTable("LeeTest")
                .HasKey(k => k.Id);

                e
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
             
            });

            modelBilder.Entity<Journal>(e =>
            {
                e
                .ToTable("Journal")
                .HasKey(k => k.id);

                e
                .Property(p => p.id)
                .ValueGeneratedOnAdd();

            });

            modelBilder.Entity<Journal_Member_Likes>(e =>
            {
                e
                .ToTable("Journal_Member_Likes")
                .HasKey(k => k.Id);

                e
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            });

            modelBilder.Entity<MemberInfo>(e =>
            {
                e
                .ToTable("MemberInfo")
                .HasKey(k => k.MemberId);

                e
                .Property(p => p.MemberId)
                .ValueGeneratedOnAdd();

            });

            modelBilder.Entity<Book>(e =>
            {
                e
                    .ToTable("Book")
                    .HasKey(k => k.book_id);

                e
                    .Property(p => p.book_id)
                    .ValueGeneratedOnAdd();

            });

            modelBilder.Entity<book_member_like>(e =>
            {
                e
                .ToTable("book_member_like")
                .HasKey(k => k.book_like_id);

                e
                .Property(p => p.book_like_id)
                .ValueGeneratedOnAdd();

            });



            base.OnModelCreating(modelBilder);
        }
    }
}
