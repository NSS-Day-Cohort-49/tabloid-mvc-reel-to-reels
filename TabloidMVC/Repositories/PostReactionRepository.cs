using Microsoft.Extensions.Configuration;
using System;
using TabloidMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using TabloidMVC.Utils;

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


        private PostReaction NewReactionFromReader(SqlDataReader reader)
        {
            return new PostReaction()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                ReactionId = reader.GetInt32(reader.GetOrdinal("ReactionId")),
                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),

            };
        }
    }
}
