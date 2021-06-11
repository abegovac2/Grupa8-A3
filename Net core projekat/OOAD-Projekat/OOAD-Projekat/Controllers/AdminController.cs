using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOAD_Projekat.Data;
using OOAD_Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        //nije potrebno praviti REpository klase jer su jednostavni pozivi

        [HttpGet]
        public async Task<IActionResult> BannedUsers()
        {
            var users = await _context.Users.Where(x => x.Blocked).ToListAsync();
            return View("UserList", new Tuple<string, List<User>>("Banned Users", users));
        }


        public async Task<IActionResult> UnbanUser(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound();

            user.Blocked = false;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("BannedUsers");
        }

        public async Task<IActionResult> BanUser(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound();

            user.Blocked = true;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("BannedUsers");
        }

        [HttpGet]
        public async Task<IActionResult> AllUsers()
        {
            var users = await _context.Users.ToListAsync();

            return View("UserList", new Tuple<string, List<User>>("All Users", users));
        }

        public async Task<IActionResult> FindUsers(string UserName)
        {
            UserName = UserName.ToUpper();
            var users = await _context.Users.Where(x => x.NormalizedUserName.Contains(UserName)).ToListAsync();

            return View("UserList", new Tuple<string, List<User>>(UserName, users));
        }

        public async Task<IActionResult> MarkQuestionAsAnwsered(int questionId)
        {
            var question = await _context.Questions.FindAsync(questionId);

            if (question == null) return NotFound();

            question.Anwsered = true;

            _context.Questions.Update(question);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Question", question.Id);
        }

        public async Task<IActionResult> MarkQuestionAsDuplicate(int questionId)
        {
            var question = await _context.Questions.FindAsync(questionId);

            if (question == null) return NotFound();

            question.Duplicate = true;

            _context.Questions.Update(question);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Question", question.Id);
        }

        public async Task<IActionResult> AcceptAnwser(int anwserId)
        {
            var anwser = await _context.Answers.FindAsync(anwserId);

            if (anwser == null) return NotFound();

            anwser.AcceptedAsAnwser = true;

            _context.Answers.Update(anwser);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Question", anwser.QuestionID);
        }

    }
}
