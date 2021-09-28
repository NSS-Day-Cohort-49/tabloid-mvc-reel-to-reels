using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IPostRepository
    {
        void Add(Post post);
        List<Post> GetAllPublishedPosts();
        Post GetPublishedPostById(int id);
        Post GetUserPostById(int id, int userProfileId);

        void DeletePost(int id);

        void UpdatePost(Post post);

        List<Post> GetUserPosts(int UserProfileId);
    }
}