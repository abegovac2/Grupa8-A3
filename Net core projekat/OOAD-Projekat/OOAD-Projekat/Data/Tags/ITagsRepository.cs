using OOAD_Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Data.Tags
{
    public interface ITagsRepository
    {
        public Task<List<Tag>> GetTags(string searchParam);

        public Task AddTags(Tag t);
    }
}
