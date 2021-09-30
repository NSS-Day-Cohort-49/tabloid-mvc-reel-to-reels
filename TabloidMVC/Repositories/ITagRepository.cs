using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ITagRepository
    {
        void Add(Tag tag);
        List<Tag> GetAllTags();
        Tag GetTagById(int id);
        void DeleteTag(int id);
        void UpdateTag(Tag tag);
        List<Tag> GetTagsByPost(int postId);
        void AddPostTag(int id, int postId);
        void DeletePostTag(int id, int postId);

    }
}