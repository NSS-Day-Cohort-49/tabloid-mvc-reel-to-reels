using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TabloidMVC.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [DisplayName("Post")]
        public int PostId { get; set; }
        public Post Post { get; set; }

        [DisplayName("Author")]
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
