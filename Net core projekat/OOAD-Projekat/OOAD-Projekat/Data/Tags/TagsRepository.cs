using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOAD_Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Data.Tags
{
    public class TagsRepository : ITagsRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        public TagsRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public async Task<List<Tag>> GetTags(string searchParam)
        {
            // Defaultni search, kad se tek otvori stranica
            if(searchParam == "")
            {
                return await applicationDbContext.Tags.ToListAsync();
            }
            else return await applicationDbContext.Tags.Where(t => t.TagContent.ToUpper().Contains(searchParam)).ToListAsync();
        }
    }
}
