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
        /// https://docs.microsoft.com/zh-cn/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#other-relationship-patterns
        /// </summary>  
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LeeTest>(e =>
            {
                e
                .ToTable("LeeTest")
                .HasKey(k => k.Id);

                e
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
             
            });

            modelBuilder.Entity<Journal>(e =>
            {
                e
                .ToTable("Journal")
                .HasKey(k => k.id);

                e
                .Property(p => p.id)
                .ValueGeneratedOnAdd();

            });

            modelBuilder.Entity<Journal_Member_Likes>(e =>
            {
                e
                .ToTable("Journal_Member_Likes")
                .HasKey(k => k.Id);

                e
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            });

            modelBuilder.Entity<MemberInfo>(e =>
            {
                e
                .ToTable("MemberInfo")
                .HasKey(k => k.MemberId);

                e
                .Property(p => p.MemberId)
                .ValueGeneratedOnAdd();

            });

            modelBuilder.Entity<Book>(e =>
            {
                e
                    .ToTable("Book")
                    .HasKey(k => k.book_id);

                e
                    .Property(p => p.book_id)
                    .ValueGeneratedOnAdd();
                e
                    .HasOne(b => b.book_detail) //added by Lee on May 8,2020
                    .WithOne(i => i.book)
                    .HasForeignKey<book_detail>(d => d.detail_id);

                e
                    .HasMany(r => r.book_comments_list) //一个文章包含多个短评
                    .WithOne(l => l.book) //一个短评只归属于一个文章
                    .HasForeignKey(k => k.book_id);


            });
            //https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key

            modelBuilder.Entity<book_member_like>(e =>
            {
                e
                .ToTable("book_member_like")
                .HasKey(k => k.book_like_id);

                e
                .Property(p => p.book_like_id)
                .ValueGeneratedOnAdd();

            });

            modelBuilder.Entity<book_comments>(e =>
            {
                e
                    .ToTable("book_comments")
                    .HasKey(k => k.comment_id);

                e
                    .Property(p => p.comment_id)
                    .ValueGeneratedOnAdd();
                //e
                //    .Property(t => t.book_id).HasColumnName("book_id");

            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
