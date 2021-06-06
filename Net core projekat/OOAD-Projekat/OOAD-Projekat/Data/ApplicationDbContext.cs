using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OOAD_Projekat.Models;
using OOAD_Projekat.Models.ChatModels;
using OOAD_Projekat.Models.QuestionAndAnwserModels;
using OOAD_Projekat.Models.QuestionAndAnwserModels.RatingModels;
using OOAD_Projekat.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOAD_Projekat.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagPost> TagPosts { get; set; }

        public DbSet<Rating> Ratings { get; set; }
        public DbSet<PostType> PostTypes { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<SearchStatistics> SearchStatistics { get; set; }
        public DbSet<ViewedQuestionsHistory> ViewedQuestionsHistory { get; set; }
        public DbSet<NotifyUser> NotifyUsers{ get; set; }
        public DbSet<Notification> Notifications{ get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Many to many questions and tags

            builder.Entity<TagPost>().HasKey(tp => new { tp.TagId, tp.QuestionId });
            builder.Entity<TagPost>().HasOne(tp => tp.Question).WithMany(t => t.Tags).HasForeignKey(tp => tp.QuestionId);
            builder.Entity<TagPost>().HasOne(tp => tp.Tag).WithMany(t => t.TagPosts).HasForeignKey(tp => tp.TagId);

            // Configure Enum PostType in Rating

            builder.Entity<Rating>().Property(r => r.PostTypeId).HasConversion<int>();
            builder.Entity<PostType>().Property(p => p.PostTypeId).HasConversion<int>();
            builder.Entity<PostType>().HasData(Enum.GetValues(typeof(PostTypeId)).Cast<PostTypeId>().Select(e => new PostType() { PostTypeId= e, name = e.ToString() }));

            // Configure default user properties

            builder.Entity<User>().Property(u => u.Blocked).HasDefaultValue(false);


            // Configure composite key

            builder.Entity<ChatUser>().HasKey(cu => new { cu.ChatId, cu.UserId });

            // Many to many questions and users (history - for recommendation table)

            builder.Entity<ViewedQuestionsHistory>().HasKey(vqh => new { vqh.QuestionId, vqh.UserId });
            builder.Entity<ViewedQuestionsHistory>().HasOne(vqh => vqh.Question).WithMany(q => q.ViewedQuestionsHistory).HasForeignKey(vqh => vqh.QuestionId);
            builder.Entity<ViewedQuestionsHistory>().HasOne(vqh => vqh.User).WithMany(u => u.ViewedQuestionsHistory).HasForeignKey(vqh => vqh.UserId);

        }
    }
}
