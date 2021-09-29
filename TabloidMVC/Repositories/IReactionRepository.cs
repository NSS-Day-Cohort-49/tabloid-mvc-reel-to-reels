using System;
using System.Collections.Generic;
using System.Linq;
using TabloidMVC.Models;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace TabloidMVC.Repositories
{
    public interface IReactionRepository
    {
        List<Reaction> GetAllReactions();
       
    }
}
