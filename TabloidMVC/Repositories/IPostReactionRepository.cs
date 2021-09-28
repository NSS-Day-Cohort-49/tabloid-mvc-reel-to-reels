using System;
using System.Collections.Generic;
using System.Linq;
using TabloidMVC.Models;
using System.Threading.Tasks;

namespace TabloidMVC.Repositories
{
    public interface IPostReactionRepository
    {
        List<PostReaction> GetPostReactionsByPostId(int postId);
    }
}
