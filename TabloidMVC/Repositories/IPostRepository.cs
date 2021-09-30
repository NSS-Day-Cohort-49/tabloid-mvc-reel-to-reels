using System.Collections.Generic;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;


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
        PostManageTagsViewModel GetPostWithTags(int id);


    }
}