﻿using OOAD_Projekat.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OOAD_Projekat.Data.Tags
{
    public interface ITagsRepository
    {
        public Task<List<Tag>> GetTags(string searchParam);

        public Task AddTags(Tag t);
        public Task DeleteTags(Tag t);

        public Task<Tag> GetTagByName(string name);
        public Task<List<Tag>> GetPopular();
    }
}
