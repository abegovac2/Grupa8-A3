using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OOAD_Projekat.Controllers.Hubs;
using OOAD_Projekat.Data;
using OOAD_Projekat.Data.Answers;
using OOAD_Projekat.Data.NotificationData;
using OOAD_Projekat.Data.Users;
using System;
using System.Threading.Tasks;

namespace OOAD_Projekat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswersRepository answersRepository;
        private readonly IUsersRepository usersRepository;
        private readonly INotificationRepository notificationRepository;

        public AnswerController(
            IAnswersRepository answersRepository,
            IUsersRepository usersRepository,
            IUserConnectionManager userConnectionManager,
            ApplicationDbContext context

            )
        {
            this.answersRepository = answersRepository;
            this.usersRepository = usersRepository;
            this.notificationRepository = new NotificationRepository(context, userConnectionManager);
        }


        [HttpPost("AddAnswer")]
        public async Task<IActionResult> AddAnswer([FromForm(Name = "questionID")] int questionID, [FromForm(Name = "content")] string content, [FromServices] IHubContext<NotificationUserHub> notifyUser)
        {
            try
            {
                var user = await usersRepository.GetUserByUserName(User.Identity.Name);
                if (user == null) throw new Exception();
                await answersRepository.AddAnswer(questionID, content, user.Id);

                if (content.Length > 20)
                {
                    content = content.Substring(0, 17);
                    content += "...";
                }

                await notificationRepository.SendNotification(user.Id, questionID, Models.NotificationType.QUESTION, content, notifyUser);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
