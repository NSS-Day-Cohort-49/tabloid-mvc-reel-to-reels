using System;
using System.Collections.Generic;
using System.Linq;
using TabloidMVC.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TabloidMVC.Repositories
{
    public class ReactionRepository : BaseRepository, IReactionRepository
    {
        public ReactionRepository(IConfiguration config) : base(config) { }
    
    
        public List<Reaction> GetReactionByPostId()
        {

            var reactions = new List<Reaction>();

            return reactions;
        }
    
    
    
    
    }
}
