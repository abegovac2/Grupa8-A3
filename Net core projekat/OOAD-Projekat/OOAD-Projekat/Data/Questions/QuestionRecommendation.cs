using Microsoft.EntityFrameworkCore;
using OOAD_Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Data.Questions
{
    public class QuestionRecommendation : IQuestionRecommendation
    {
        private readonly ApplicationDbContext applicationDbContext;
        public QuestionRecommendation(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<ICollection<Question>> RecommendQuestions(string user)
        {
            // Za nove korisnike ce izabrati sva pitanja :)
            // Pronaci pitanja koja je korisnik otvarao u proslosti
            var userQuestions = await FindUsersQuestions(user);

            // Odrediti scor svakog taga
            Dictionary<string, double> tagScore = DetermineTagScore(userQuestions);

            // Izvuci pitanja iz baze koja korisnik nije otvarao

            var notOpenedQuestions = await FindNotOpenedQuestions(user, userQuestions);

            // Sada cemo za svako pitanje odrediti "ocjenu slicnosti" na osnovu tagova tog pitanja i na osnovu izracunate korisnicke ocjene tagova
            // Sto visocija ocjena bude, to je veca vjerovatnoca da je pitanje interesantno za naseg korisnika
            // Dictionary za Question i konacnu ocjenu pitanja
            Dictionary<Question, double> questionScores = DetermineQuestionScores(tagScore, notOpenedQuestions);
            return TakeRecommendedQuestions(questionScores);
        }


        // Uzima sva pitanja iz historije korisnika
        private async Task<ICollection<Question>> FindUsersQuestions(string user)
        {

            return await applicationDbContext.ViewedQuestionsHistory
                .Where(vqh => vqh.User.UserName == user)
                .Include(q => q.Question)
                .ThenInclude(q => q.Tags)
                .ThenInclude(t => t.Tag)
                .Select(vqh => vqh.Question)
                .ToListAsync();
        }
        private Dictionary<string, double> DetermineTagScore(ICollection<Question> questions)
        {
            // U sustini dictionary <Tag, double>
            var dict = new Dictionary<string, double>();
            foreach (var question in questions)
            {

                foreach (var tag in question.Tags)
                {
                    var tagContent = tag.Tag.TagContent;
                    // Prvo pojavljivanje taga, postavi defaultnu ocjenu na 1
                    if (!dict.ContainsKey(tag.Tag.TagContent))
                    {
                        dict[tagContent] = 1.0;
                    }
                    // Uvecaj ocjenu za 1
                    else
                    {
                        dict[tagContent] += 1.0;
                    }
                }

            }
            // Nakon sto su pocetne ocjene (tj. ucestalost pojavljivanja taga u historiji pitanja) definirane,
            // sada cemo ocjenu svakog taga podijeliti sa brojem pitanja, time opseg ocjene je u [0,1]
            var numberOfQuestions = questions.Count;

            foreach (KeyValuePair<string, double> tagScore in dict)
            {
                if (numberOfQuestions != 0)
                {
                    dict[tagScore.Key] /= numberOfQuestions;
                }
            }
            return dict;
        }
        private async Task<ICollection<Question>> FindNotOpenedQuestions(string user, ICollection<Question> questions)
        {
            // Koristimo vec nadjena pitanja
            return await applicationDbContext.Questions
                .Where(q => !questions.Contains(q))
                .Include(q => q.Tags)
                .ThenInclude(q => q.Tag)
                .ToListAsync();
        }

        private Dictionary<Question, double> DetermineQuestionScores(Dictionary<string, double> tagScore, ICollection<Question> notOpenedQuestions)
        {
            Dictionary<Question, double> questionScores = new Dictionary<Question, double>();
            foreach (var question in notOpenedQuestions)
            {
                double score = 0.0;
                if (question.Tags == null)
                {
                    questionScores[question] = 0.0;
                    continue;
                }
                foreach (var tag in question.Tags)
                {
                    var tagContent = tag.Tag.TagContent;
                    if (tagScore.ContainsKey(tagContent))
                    {
                        score += tagScore[tagContent];
                    }
                }
                questionScores[question] = score;
            }
            return questionScores;
        }
        private ICollection<Question> TakeRecommendedQuestions(Dictionary<Question, double> questionScores)
        {
            List<Question> recommendedQuestions = new List<Question>();
            // Sortiraj u opadajucem poretku
            var orderedQuestionScores = questionScores.OrderBy(qs => qs.Value).Reverse();
            foreach (var pair in orderedQuestionScores)
            {
                recommendedQuestions.Add(pair.Key);
            }
            return recommendedQuestions;
        }
    }
}
