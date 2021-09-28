using Microsoft.Extensions.Configuration;
using System;
using TabloidMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Repositories
{
    public class PostReactionRepository : BaseRepository, IPostReactionRepository
    {
        public PostReactionRepository(IConfiguration config) : base(config) { }


        public List<PostReaction> GetPostReactionsByPostId()
        {
            var postReacations = new List<PostReaction>();


            return postReacations;
        }
    }
}
