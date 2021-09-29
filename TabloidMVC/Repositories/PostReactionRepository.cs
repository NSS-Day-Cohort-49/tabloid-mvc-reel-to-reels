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


        public List<PostReaction> GetPostReactionsByPostId(int postId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT pr.Id, pr.PostId, pr.ReactionId, pr.UserProfileId, r.Name AS reactionName,
                                        r.ImageLocation AS reactionImageLocation, r.Id AS reactionId, p.Title AS postTitle, p.Content AS postContent,
                                        p.ImageLocation AS postImageLocation, p.Id AS postId
                                        From PostReaction pr 
                                        LEFT JOIN  Post p ON p.Id = pr.PostId 
                                        LEFT JOIN Reaction r ON r.Id = pr.ReactionId
                                        WHERE pr.PostId = @postId";

                    cmd.Parameters.AddWithValue("@postId", postId);
                    var reader = cmd.ExecuteReader();

                    var postReactions = new List<PostReaction>();

                    while (reader.Read())
                    {
                        postReactions.Add(NewPostReactionFromReader(reader));
                    }

                    reader.Close();

                    return postReactions;
                }
            }
           
        }

        public void AddNewReaction(PostReaction pr)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        INSERT INTO [PostReaction] ([PostId], [UserProfileId], [ReactionId]) VALUES (@postId, @userProfileId, @reactionId)
                                        ";

                    cmd.Parameters.AddWithValue("@postId", pr.PostId);
                    cmd.Parameters.AddWithValue("@userProfileId", pr.UserProfileId);
                    cmd.Parameters.AddWithValue("@reactionId", pr.ReactionId);

                    cmd.ExecuteNonQuery();

                    
                }
            }
        }

        public List<PostReaction> GetTotalReactionsByPost(int id, int reactionId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM PostReaction WHERE @postId = postId AND @reactionId = id";

                    cmd.Parameters.AddWithValue("@postId", id);
                    cmd.Parameters.AddWithValue("@reactionId", reactionId);

                    var reader = cmd.ExecuteReader();

                    List<PostReaction> postReactions = new List<PostReaction>();

                    while (reader.Read())
                    {
                        postReactions.Add(NewPostReactionFromReader(reader));
                    }

                    reader.Close();

                    return postReactions;
                }
            }
        }


        private PostReaction NewPostReactionFromReader(SqlDataReader reader)
        {
            return new PostReaction()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                ReactionId = reader.GetInt32(reader.GetOrdinal("ReactionId")),
                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
               
                Reaction = new Reaction()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("reactionId")),
                    Name = reader.GetString(reader.GetOrdinal("reactionName")),
                    ImageLocation = reader.GetString(reader.GetOrdinal("reactionImageLocation"))
                },
                Post = new Post()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("postId")),
                    Title = reader.GetString(reader.GetOrdinal("postTitle")),
                    Content = reader.GetString(reader.GetOrdinal("postContent")),
                    ImageLocation = reader.GetString(reader.GetOrdinal("postImageLocation"))
                }

            };
        }
    }
}
