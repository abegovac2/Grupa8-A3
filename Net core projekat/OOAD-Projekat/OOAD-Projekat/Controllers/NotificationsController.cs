using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class NotificationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public NotificationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefaultAsync();
            if (user == null) return NotFound();

            var notifications = await _context.Notifications.Where(x => x.UserId == user.Id && x.Seen == false).ToListAsync();

            return View("Index", new Tuple<string, List<Notification>>(user.Id, notifications));
        }
    }
}
