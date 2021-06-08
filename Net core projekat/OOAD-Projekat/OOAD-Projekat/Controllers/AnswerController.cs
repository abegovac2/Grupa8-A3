using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> AddAnswer([FromForm(Name = "questionID")] int questionID, [FromForm(Name = "content")] string content) {
            try
            {
                var user = await usersRepository.GetUserByUserName(User.Identity.Name);
                if (user == null) throw new Exception();
                await answersRepository.AddAnswer(questionID, content, user.Id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
