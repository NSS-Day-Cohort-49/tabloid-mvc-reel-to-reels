using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models
{
    public class PostReaction
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public int ReactionId { get; set; }

        public int UserProfileId { get; set; }


        public Post Post = new Post();

        public Reaction Reaction = new Reaction();

        public UserProfile UserProfile = new UserProfile();
    }
}
