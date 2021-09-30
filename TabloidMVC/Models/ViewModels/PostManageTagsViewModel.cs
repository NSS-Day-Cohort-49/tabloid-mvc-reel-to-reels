using System;
using System.Collections.Generic;

namespace TabloidMVC.Models.ViewModels
{
    public class PostManageTagsViewModel
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public int TagId { get; set; }
        public List<Tag> PostTags { get; set; }
    }
}
