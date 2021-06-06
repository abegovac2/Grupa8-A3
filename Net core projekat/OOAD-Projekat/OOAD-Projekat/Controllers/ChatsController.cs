using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OOAD_Projekat.Controllers.Hubs;
using OOAD_Projekat.Data;
using OOAD_Projekat.Data.ChatData;
using OOAD_Projekat.Data.NotificationData;
using OOAD_Projekat.Models;
using OOAD_Projekat.Models.ChatModels;

namespace OOAD_Projekat
{
    [Authorize]
    public class ChatsController : Controller
    {
        private readonly IChatRepository _chatRepository;

        private readonly INotificationRepository _notificationRepository;

        public ChatsController(ApplicationDbContext context, IChatRepository chatRepository, IUserConnectionManager userConnectionManager)
        {
            _chatRepository = chatRepository;
            _notificationRepository = new NotificationRepository(context, userConnectionManager);
        }

        // GET: Chats
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var user = await _chatRepository.GetUserByName(User.Identity.Name);
            var lista = await _chatRepository.GetChatsForUser(user);
            var notifications = new List<bool>();
            lista.ForEach(x => notifications.Add(_notificationRepository.HasNotifications(user.Id, x.Id, NotificationType.CHAT)));

            return View(new Tuple<string, List<Chat>, List<bool>>(user.Id,lista,notifications));
        }

        // GET 
        [HttpGet]
        public async Task<IActionResult> SearchForChat(string ChatName)
        {
            var lista = await _chatRepository.SearchForChat(ChatName);
            return View("Index", lista);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // GET: Individual Chats
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var chat = await _chatRepository.GetChat(id);

            if (chat == null) return NotFound();

            var current = await _chatRepository.GetUserByName(User.Identity.Name);
            var userRole = chat.Users.Find(x => x.UserId == current.Id);

            await _notificationRepository.MarkAsSeen(current.Id, (int)id ,NotificationType.CHAT);

            if (userRole == null) return NotFound();

            return View(new Tuple<string,Chat, UserRole>(current.Id, chat, userRole.Role));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Chat creation

        //AddedUsers - string of format "::UserName::UserName::"
        //contains currently added users to the chat

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var res = new Tuple<List<User>, string>(
                (await _chatRepository.GetUsers())
                .Where(x => x.UserName != User.Identity.Name).ToList(),
                "::"
                );

            return View(res);
        }

        public async Task<IActionResult> AddUserToAddedUsers(string AddUser, string AddedUsers)
        {
            var split = AddedUsers.Split("::");
            if (!split.Any(x => x.ToUpper() == AddUser.ToUpper())) AddedUsers += AddUser + "::";

            var list = await _chatRepository.GetUsers();
            var result = list.Where(x => x.UserName != User.Identity.Name && !AddedUsers.Contains(x.UserName)).ToList();

            return View("Create", new Tuple<List<User>, string>(result, AddedUsers));
        }

        public async Task<IActionResult> RemoveUserFromAddedUsers(string user, string AddedUsers)
        {
            var users = AddedUsers.Split("::");
            AddedUsers = "::";
            foreach (var item in users)
            {
                if (item.Length == 0) continue;
                if (item.ToLower() != user.ToLower()) AddedUsers += item + "::";
            }
            var result = (await _chatRepository.GetUsers())
                .Where(x => x.UserName != User.Identity.Name && !AddedUsers.Contains(x.UserName))
                .ToList();
            return View("Create", new Tuple<List<User>, string>(result, AddedUsers));
        }
        //only in chat creation
        public async Task<IActionResult> SearchForNewChatUsers(string name, string AddedUsers)
        {
            var result = await _chatRepository.SearchUsers(name);
            var find = result.Find(x => x.UserName == User.Identity.Name);
            if (find != null) result.Remove(find);


            for (int i = 0; i < result.Count; ++i)
            {
                if (AddedUsers.Contains(result[i].UserName))
                {
                    result.Remove(result[i]);
                    --i;
                }
            }

            return View("Create", new Tuple<List<User>, string>(result, AddedUsers));
        }

        // POST: Add a new chat
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNewChat(string chatName, string AddedUsers)
        {
            var users = AddedUsers.Split("::");
            if (users.Length <= 2) return RedirectToAction(nameof(Create));

            if (chatName == null) chatName = "";
            else chatName.Trim();

            if (chatName.Length == 0)
            {
                chatName = User.Identity.Name + ",";
                for(int i = 0; i < users.Length; ++i)
                {
                    if (users[i].Length != 0) chatName += users[i];
                    else continue;
                    if (i != users.Length - 2) chatName += ",";
                }
            }

            var chat = new Chat
            {
                ChatName = chatName,
                ChatType = users.Length != 3 ? ChatType.GROUP : ChatType.DIRECT
            };

            var creator = (await _chatRepository.GetUserByName(User.Identity.Name)).Id;

            chat.Users.Add(new ChatUser
            {
                UserId = creator,
                Role = UserRole.ADMIN
            });

            var listOfUserIds = new List<string>() { creator};

            foreach (var userInChat in users)
            {
                if (userInChat.Length == 0) continue;
                var chatUser = await _chatRepository.GetUserByName(userInChat);

                if (chatUser == null) continue;

                listOfUserIds.Add(chatUser.Id);

                chat.Users.Add(
                    new ChatUser
                    {
                        UserId = chatUser.Id,
                        Role = UserRole.USER
                    });
            }

            await _chatRepository.CreateNewChat(chat);

            foreach(var user in listOfUserIds)
            {
                await _notificationRepository.AddUserToNotificationList(user, chat.Id, NotificationType.CHAT);
            }
            

            return RedirectToAction("Details", new { id = chat.Id, });
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string UserId, int ChatId)
        {
            //var UserId = (await _chatRepository.GetUserByName(User.Identity.Name)).Id;

            await _chatRepository.DeleteChatUser(UserId, ChatId);

            await _notificationRepository.RemoveUserFromNotificationList(UserId, ChatId, NotificationType.CHAT);

            var numOfUsersInChat = _chatRepository.NumberOfUsersInAChat(ChatId);
            if (numOfUsersInChat <= 1) return await DeleteChat(ChatId);
            
            return RedirectToAction("Details", new { id = ChatId, });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteChat(int Id)
        {
            await _chatRepository.DeleteChat(Id);

            await _notificationRepository.RemoveAllUsersFromNotificationList(Id, NotificationType.CHAT);

            return RedirectToAction(nameof(Index));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromForm(Name = "chatId")] string chatId, [FromForm(Name = "name")] string name, [FromForm(Name = "text")] string text, [FromServices] IHubContext<NotificationUserHub> notifyUser)
        {
            await _chatRepository.SaveMessage(int.Parse(chatId), name, text);
            //Console.WriteLine($"UPISANA PORUKA U BAZU {chatId}, {name}, {text}");

            var user = await _chatRepository.GetUserByName(name);

            await _notificationRepository.SendNotification( user.Id, int.Parse(chatId), NotificationType.CHAT, text, notifyUser);
            
            return RedirectToAction("Details", new { id = int.Parse(chatId) });
        }
    }
}
