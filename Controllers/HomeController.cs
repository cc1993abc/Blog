using Blog.Controllers.Repository;
using Blog.Data;
using Blog.Data.FileManager;
using Blog.Models;
using Blog.Models.Comment;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        public  IRepository _repo { get; set; }

        private IFileManager _fileManager;

        public HomeController(IRepository repo,IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }
        public IActionResult Index(string category) => View(string.IsNullOrWhiteSpace(category) ? _repo.GetAllPosts() : _repo.GetAllPosts(category));

        //public IActionResult Index(string category)
        //{
        //    var posts = string.IsNullOrWhiteSpace(category)? _repo.GetAllPosts():_repo.GetAllPosts(category);
        //    return View(posts);
        //}

        public IActionResult Post(int id) =>  View(_repo.GetPost(id));



        [HttpGet("/Image/{image}")]
        [ResponseCache(CacheProfileName ="monthly")]
        public IActionResult Image(string image) => new FileStreamResult(_fileManager.ImageStream(image), $"image/{image.Substring(image.LastIndexOf('.'))}");


        //[HttpGet("/Image/{image}")]
        //public IActionResult Image(string image)
        //{
        //    var mime = image.Substring(image.LastIndexOf('.'));
        //    return new FileStreamResult(_fileManager.ImageStream(image),$"image/{mime}");
        //}

        public async Task<IActionResult> Comment(CommentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Post",new {id = vm.PostId });
            }
            var post = _repo.GetPost(vm.PostId);
            if (vm.MainCommentId == 0)
            {
                // equal      post.MainComments =  post.MainComments  ?? new List<MainComment>();

                post.MainComments ??= new List<MainComment>();

                post.MainComments.Add(new MainComment
                {
                    Message = vm.Message,
                    Created = DateTime.Now
                });
                _repo.UpdatePost(post);
            }
            else
            {
                var comment = new SubComment
                {
                    MainCommentId = vm.MainCommentId,
                    Message = vm.Message,
                    Created = DateTime.Now
                };
                _repo.AddSubComment(comment);
            }

            await _repo.SaveChangesAsync();

            return RedirectToAction("Post", new { id = vm.PostId });

        }
    }

}
