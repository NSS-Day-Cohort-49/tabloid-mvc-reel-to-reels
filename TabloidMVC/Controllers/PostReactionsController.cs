using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class PostReactionsController : Controller
    {

        private readonly IPostReactionRepository _postReactionRepository;
        

        public PostReactionsController(IPostReactionRepository postReactionRepository)
        {
            _postReactionRepository = postReactionRepository;
           
        }
        // GET: PostReactionsController

        public void AddReaction (int postId, int reactionId, int userProfileId)
        {
            PostReaction pr = new PostReaction()
            {
                PostId = postId,
                ReactionId = reactionId,
                UserProfileId = userProfileId
            };
            try
            {
                _postReactionRepository.AddNewReaction(pr);
              
            }

            catch
            {
                Console.WriteLine("This did not work");
            }
        }
       
        public ActionResult Index(int postId)
        {
            List<PostReaction> postReactions = _postReactionRepository.GetPostReactionsByPostId(postId);
            return View(postReactions);
        }

        // GET: PostReactionsController/Details/5
        public ActionResult Details(int id)
        {
           
            return View();
        }

        // GET: PostReactionsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostReactionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostReactionsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostReactionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostReactionsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostReactionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
