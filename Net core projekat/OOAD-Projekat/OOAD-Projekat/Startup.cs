using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OOAD_Projekat.Controllers.Hubs;
using OOAD_Projekat.Data;
using OOAD_Projekat.Data.Answers;
using OOAD_Projekat.Data.ChatData;
using OOAD_Projekat.Data.NotificationData;
using OOAD_Projekat.Data.Questions;
using OOAD_Projekat.Data.ReactionData;
using OOAD_Projekat.Data.Statistics;
using OOAD_Projekat.Data.TagPosts;
using OOAD_Projekat.Data.Tags;
using OOAD_Projekat.Data.Users;
using OOAD_Projekat.Models;
using OOAD_Projekat.Utils;

namespace OOAD_Projekat
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("AzureConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddSignalR();
            services.AddScoped<IQuestionsRepository, QuestionsRepository>();
            services.AddScoped<IStatisticsRepository, StatisticsRepository>();
            services.AddScoped<ITagsRepository, TagsRepository>();
            services.AddScoped<ITagPostRepository, TagPostRepository>();
            services.AddScoped<IQuestionRecommendation, QuestionRecommendation>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddSingleton<IUserConnectionManager, UserConnectionManager>();
            services.AddScoped<IAnswersRepository, AnswersRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IReactionRepository, ReactionRepository>();
            services.Configure<MailInfo>(Configuration.GetSection("MailInfo"));
            services.AddScoped<IMailSender, MailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
            });
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Question}/{action=Index}/{id?}");
                endpoints.MapHub<ChatHub>("/chatHub");
                endpoints.MapHub<NotificationUserHub>("/NotificationUserHub");
                endpoints.MapRazorPages();
            });
        }
    }
}
