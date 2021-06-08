using Microsoft.AspNetCore.Mvc;
using OOAD_Projekat.Data.Answers;
using OOAD_Projekat.Data.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswersRepository answersRepository;
        private readonly IUsersRepository usersRepository;
        public AnswerController(IAnswersRepository answersRepository, IUsersRepository usersRepository)
        {
            this.answersRepository = answersRepository;
            this.usersRepository = usersRepository;
        }

        [HttpPost("AddAnswer")]
        public async Task AddAnswer([FromForm(Name = "questionID")] int questionID, [FromForm(Name = "content")] string content) {
            Console.WriteLine("QuestionID: " + questionID);
            Console.WriteLine("Content: " + content);
            var user = await usersRepository.GetUserByUserName(User.Identity.Name);
            await answersRepository.AddAnswer(questionID, content, user.Id);
        }
    }
}
