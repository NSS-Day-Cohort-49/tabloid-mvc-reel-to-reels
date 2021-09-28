using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models.ViewModels
{
    public class PostDetailswithReactionsViewModel
    {
        public Post Post  { get; set; }

        public List<PostReaction> PostReaction { get; set; }

        public List<Reaction> Reactions { get; set; }
    }
}
