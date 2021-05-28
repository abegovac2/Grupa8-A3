using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OOAD_Projekat.Data;
using OOAD_Projekat.Data.ChatData;
using OOAD_Projekat.Models;
using OOAD_Projekat.Models.ChatModels;

namespace OOAD_Projekat
{
    [Authorize]
    public class ChatsController : Controller
    {
        private readonly IChatRepository _chatRepository;

        public ChatsController(ApplicationDbContext context) => _chatRepository = new ChatRepository(context);

        // GET: Chats
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var user = await _chatRepository.GetUserByName(User.Identity.Name);
            var lista = await _chatRepository.GetChatsForUser(user);

            return View(lista);
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

            if (userRole == null) return NotFound();

            return View(new Tuple<Chat, UserRole>(chat, userRole.Role));
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
            if (users.Length <= 2) return View("Error");

            var chat = new Chat
            {
                ChatName = chatName,
                ChatType = users.Length != 3 ? ChatType.GROUP : ChatType.DIRECT
            };

            chat.Users.Add(new ChatUser
            {
                UserId = (await _chatRepository.GetUserByName(User.Identity.Name)).Id,
                Role = UserRole.ADMIN
            });

            foreach (var userInChat in users)
            {
                if (userInChat.Length == 0) continue;
                var chatUser = await _chatRepository.GetUserByName(userInChat);
                if (chatUser == null) continue;
                chat.Users.Add(
                    new ChatUser
                    {
                        UserId = chatUser.Id,
                        Role = UserRole.USER
                    });
            }

            _chatRepository.CreateNewChat(chat);

            return RedirectToAction("Details", new { id = chat.Id, });
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int ChatId)
        {
            var UserId = (await _chatRepository.GetUserByName(User.Identity.Name)).Id;

            _chatRepository.DeleteChatUser(ChatId, UserId);

            var numOfUsersInChat = _chatRepository.NumberOfUsersInAChat(ChatId);
            if (numOfUsersInChat <= 1) return DeleteChat(ChatId);
            
            return RedirectToAction("Details", new { id = ChatId, });
        }

        [HttpPost]
        public IActionResult DeleteChat(int Id)
        {
            _chatRepository.DeleteChat(Id);
            return RedirectToAction(nameof(Index));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromForm(Name = "chatId")] string chatId, [FromForm(Name = "name")] string name, [FromForm(Name = "text")] string text)
        {
            _chatRepository.SaveMessage(int.Parse(chatId), name, text);
            Console.WriteLine($"UPISANA PORUKA U BAZU {chatId}, {name}, {text}");
            return RedirectToAction("Details", new { id = int.Parse(chatId) });
        }
    }
}
