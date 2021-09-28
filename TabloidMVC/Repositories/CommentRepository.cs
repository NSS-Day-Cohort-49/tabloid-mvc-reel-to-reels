using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TabloidMVC.Models;
using TabloidMVC.Utils;
namespace TabloidMVC.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IConfiguration config) : base(config) { }
        public List<Comment> GetPostComments()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT c.Id, c.Subject, c.Content, c.PostId, c.UserProfileId, c.CreateDateTime,
                                u.FirstName, u.LastName, u.DisplayName, 
                                u.Email, u.CreateDateTime, u.ImageLocation AS AvatarImage,
                                u.UserTypeId, p.Id, p.Title, p.Content, p.ImageLocation AS HeaderImage,
                                p.CreateDateTime, p.PublishDateTime, p.IsApproved,
                                p.CategoryId, p.UserProfileId 
                        From Comment c
                            LEFT JOIN UserProfile u ON c.UserProfileId = u.Id
                            LEFT JOIN Post p ON c.PostId = p.Id
                        WHERE p.Id =  c.PostId
                        ORDER By c.CreateDateTime DESC";
                    var reader = cmd.ExecuteReader();

                    var comments = new List<Comment>();

                    while(reader.Read())
                    {
                        comments.Add(NewCommentFromReader(reader));
                    }
                    reader.Close();

                    return comments;
                }
            }
        }
        private Comment NewCommentFromReader(SqlDataReader reader)
        {
            return new Comment()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Subject = reader.GetString(reader.GetOrdinal("Subject")),
                Content = reader.GetString(reader.GetOrdinal("Content")),
                PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                Post = new Post()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    Content = reader.GetString(reader.GetOrdinal("Content")),
                    ImageLocation = DbUtils.GetNullableString(reader, "HeaderImage"),
                    CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                    PublishDateTime = DbUtils.GetNullableDateTime(reader, "PublishDateTime"),
                    CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId"))
                },
                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                UserProfile = new UserProfile()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                    DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                    ImageLocation = DbUtils.GetNullableString(reader, "AvatarImage"),
                    UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId"))

                },
                CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"))
            };
        }
    }
}
