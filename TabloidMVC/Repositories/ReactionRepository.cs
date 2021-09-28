using System;
using System.Collections.Generic;
using System.Linq;
using TabloidMVC.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using TabloidMVC.Utils;

namespace TabloidMVC.Repositories
{
    public class ReactionRepository : BaseRepository, IReactionRepository
    {
        public ReactionRepository(IConfiguration config) : base(config) { }
    
    
        public List<Reaction> GetAllReactions()
        {

            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, [Name], ImageLocation FROM Reaction";
                    var reader = cmd.ExecuteReader();

                    var reactions = new List<Reaction>();

                    while (reader.Read())
                    {
                        reactions.Add(NewReactionFromReader(reader));
                    }

                    reader.Close();

                    return reactions;
                }
            }
        }

        private Reaction NewReactionFromReader(SqlDataReader reader)
        {
            return new Reaction()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                ImageLocation = DbUtils.GetNullableString(reader, "ImageLocation"),

            };
        }
   
    }
}
