using Microsoft.AspNetCore.Mvc;
using OOAD_Projekat.Data.Questions;
using OOAD_Projekat.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OOAD_Projekat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionController : ControllerBase
    {
        private readonly IQuestionsRepository questionsRepository;
        public SuggestionController(IQuestionsRepository questionsRepository)
        {
            this.questionsRepository = questionsRepository;
        }
        [HttpGet("SearchInputSuggestion")]
        public async Task<List<Question>> SuggestBasedOnSearchInput([FromQuery(Name = "searchParam")] string searchParam)
        {
            return await questionsRepository.Find(searchParam);
        }
    }
}
