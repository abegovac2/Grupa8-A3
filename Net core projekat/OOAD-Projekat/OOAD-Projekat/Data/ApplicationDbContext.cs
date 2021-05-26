using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OOAD_Projekat.Models;
using OOAD_Projekat.Models.ChatModels;
using OOAD_Projekat.Models.QuestionAndAnwserModels;
using OOAD_Projekat.Models.QuestionAndAnwserModels.RatingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOAD_Projekat.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {

        public DbSet<Question> Questions;
        public DbSet<Answer> Answers;
        public DbSet<Tag> Tags;
        public DbSet<TagPost> TagPosts;

        public DbSet<Rating> Ratings;
        public DbSet<PostType> PostTypes;
        public DbSet<ChatUser> ChatUsers;
        public DbSet<Chat> Chats;
        // public DbSet<Message> Messages;


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
        }
    }
}
